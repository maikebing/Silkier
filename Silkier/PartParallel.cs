using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Silkier.Extensions
{
    public static class PartParallel
    {
        public static ParallelLoopResult ForEach<T>(IEnumerable<T> source, int rangeSize, ParallelOptions parallelOptions, Action<T> action)
        {
            return Parallel.ForEach(Partitioner.Create(0, source.Count(), Math.Min(source.Count(), rangeSize)), parallelOptions ?? new ParallelOptions(), (range, loopState) =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    action?.Invoke(source.ElementAt(i));
                }
            });
        }

        
        public static ParallelLoopResult ForEach<T>(IEnumerable<T> source, int rangeSize, int _maxDegreeOfParallelism, Action<T> action) =>
                                    ForEach(source, rangeSize, new ParallelOptions() { MaxDegreeOfParallelism = _maxDegreeOfParallelism }, action);

        public static ParallelLoopResult ForEach<T>(IEnumerable<T> source, int _maxDegreeOfParallelism, Action<T> action) =>
                                   ForEach(source, (source.Count() + _maxDegreeOfParallelism - 1) / _maxDegreeOfParallelism, new ParallelOptions() { MaxDegreeOfParallelism = _maxDegreeOfParallelism }, action);

        public static ParallelLoopResult ForEach<T>(IEnumerable<T> source, Action<T> action)
        {
            ParallelOptions parallelOptions = new ParallelOptions();
            return ForEach(source, (source.Count() + parallelOptions.MaxDegreeOfParallelism - 1) / parallelOptions.MaxDegreeOfParallelism, parallelOptions, action);
        }
        public static ParallelLoopResult ForEach<T>(IEnumerable<T> source, ParallelOptions parallelOptions, Action<T> action) =>
                                    ForEach(source, (source.Count() + parallelOptions.MaxDegreeOfParallelism - 1) / parallelOptions.MaxDegreeOfParallelism, parallelOptions, action);

    }
}
