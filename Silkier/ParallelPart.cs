using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Silkier.Extensions
{
    /// <summary>
    /// 并行分区执行
    /// </summary>
    public static class ParallelPart
    {
        /// <summary>
        ///  并行处理<paramref name="source"/>,一个并行任务中分配<paramref name="rangeSize"/>个元素给 <paramref name="action"/>
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源数据</param>
        /// <param name="rangeSize">一个并行任务的最大分配数量</param>
        /// <param name="parallelOptions"></param>
        /// <param name="action"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 并行处理<paramref name="source"/>，按照最多<paramref name="_maxDegreeOfParallelism"/>个个数分配元素给<paramref name="action"/>处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="_maxDegreeOfParallelism">最大任务量</param>
        /// <param name="action"></param>
        /// <returns></returns>
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
