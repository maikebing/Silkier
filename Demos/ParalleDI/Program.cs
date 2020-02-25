
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Silkier.Extensions;

namespace Demos
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
    public class BlogX
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }


    public class BloggingContext : DbContext
    {
        public static long InstanceCount;

        public BloggingContext(DbContextOptions options)
            : base(options)
            => Interlocked.Increment(ref InstanceCount);

        public DbSet<Blog> Blogs { get; set; }
    }

    public class BlogController
    {
        private readonly BloggingContext _context;

        public BlogController(BloggingContext context) => _context = context;

        public async Task ActionAsync()
        {
            var x = await _context.Blogs.FirstAsync();
            Console.WriteLine($" {x.Name} {_context.ContextId.InstanceId.ToString()} {Task.CurrentId}");
        }
    }

    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            var f = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder();
            f.DataSource = "test.db";
            services.AddDbContextPool<BloggingContext>(c => c.UseSqlite(f.ToString()));
            services.AddSingleton(b =>
            {
                return new Blog() { BlogId = 1, Name = "Blog", Url = "http://www.cn.cn" };
            });
            services.AddTransient(b =>
            {
                return new BlogX() { BlogId = 1, Name = "BlogX", Url = "http://www.cn.cn" };
            });
        }
    }

    public class Program
    {
        private const int Threads = 32;
        private const int Seconds = 10;

        private static long _requestsProcessed;

        private static async Task Main()
        {
            var serviceCollection = new ServiceCollection();
            new Startup().ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            SetupDatabase(serviceProvider);

            var stopwatch = new Stopwatch();

            MonitorResults(TimeSpan.FromSeconds(Seconds), stopwatch);

            await Task.WhenAll(
                Enumerable
                    .Range(0, Threads).ToArray()
                    .Select(async _ => await SimulateRequestsAsync(serviceProvider, stopwatch)).ToArray());
        }

        private static void SetupDatabase(IServiceProvider serviceProvider)
        {

            int x = 0;
            List<int> list = Enumerable.Range(1, 1000).ToList();
            ParallelPart.ForEach(list, 10, serviceProvider, (int i, Blog b, BlogX x, BloggingContext bc) =>
           {
               bc.Blogs.Add(new Blog() { Name = i.ToString(), Url = b.Url });
               Console.WriteLine($"{Thread.CurrentThread} {i}{b?.Name}{x?.Name}");
               bc.SaveChanges();
           });
        }

        private static async Task SimulateRequestsAsync(IServiceProvider serviceProvider, Stopwatch stopwatch)
        {
            while (stopwatch.IsRunning)
            {
                try
                {
                    using (var serviceScope = serviceProvider.CreateScope())
                    {
                        await new BlogController(serviceScope.ServiceProvider.GetService<BloggingContext>()).ActionAsync();
                    }
                    Interlocked.Increment(ref _requestsProcessed);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
            await Task.CompletedTask;
        }

        private static async void MonitorResults(TimeSpan duration, Stopwatch stopwatch)
        {
            var lastInstanceCount = 0L;
            var lastRequestCount = 0L;
            var lastElapsed = TimeSpan.Zero;

            stopwatch.Start();

            while (stopwatch.Elapsed < duration)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));

                var instanceCount = BloggingContext.InstanceCount;
                var requestCount = _requestsProcessed;
                var elapsed = stopwatch.Elapsed;
                var currentElapsed = elapsed - lastElapsed;
                var currentRequests = requestCount - lastRequestCount;

                Console.WriteLine(
                    $"[{DateTime.Now:HH:mm:ss.fff}] "
                    + $"Context creations/second: {instanceCount - lastInstanceCount} | "
                    + $"Requests/second: {Math.Round(currentRequests / currentElapsed.TotalSeconds)}");

                lastInstanceCount = instanceCount;
                lastRequestCount = requestCount;
                lastElapsed = elapsed;
            }

            Console.WriteLine();
            Console.WriteLine($"Total context creations: {BloggingContext.InstanceCount}");
            Console.WriteLine(
                $"Requests per second:     {Math.Round(_requestsProcessed / stopwatch.Elapsed.TotalSeconds)}");

            stopwatch.Stop();
        }
    }
}