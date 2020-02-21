using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Silkier
{
    public static class ReTry
    {
        public static T Invoke<T>(int times, Func<T> action)
        {
            return Invoke(times, a =>
            {
                return action.Invoke();
            }, ef =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(ef.current * 5));
            });
        }

        public static T Invoke<T>(int times, Func<int, T> action, Action<(int current, Exception ex)> efunc)
        {
            Exception exception = null;
            for (int i = 0; i < times; i++)
            {
                try
                {
                    return action.Invoke(i + 1);
                }
                catch (Exception ex)
                {
                    exception = ex;
                    efunc?.Invoke((i + 1, ex));
                }
            }
            throw exception;
        }
    }
}
