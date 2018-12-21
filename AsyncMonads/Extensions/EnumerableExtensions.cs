using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncMonads.Extensions
{
    public static class EnumerableExtensions
    {
        public static async Task<IEnumerable<B>> Traverse<A, B>(this IEnumerable<Task<A>> tasks, Func<A, B> selector)
        {
            var taskResults = await Task.WhenAll(tasks);
            var mappedTaskResults = taskResults.Select(selector);

            return mappedTaskResults;
        }
    }
}