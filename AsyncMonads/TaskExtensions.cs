using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncMonads
{
    public static class TaskExtensions
    {
        public static async Task<TResult> Select<T, TResult>(this Task<T[]> items, Func<T[], TResult> selector)
        {
            return selector(await items);
        }
    }
}