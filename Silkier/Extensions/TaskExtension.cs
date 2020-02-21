using System;
using System.Threading.Tasks;

namespace Silkier.Extensions
{
    public static class TaskExtension
    {
        public static Task Forget(this Task task)
        {
            return Task.CompletedTask;
        }
    }
}
