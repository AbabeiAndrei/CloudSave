using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSave.GeneralLibrary.Extensions
{
    public static class EnumerableEx
    {
        public static IEnumerable<TSource> Foreach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null) 
                throw new ArgumentNullException(nameof(source));

            if (action == null) 
                throw new ArgumentNullException(nameof(action));

            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<TSource> Insert<TSource>(this IEnumerable<TSource> source, TSource item, int index = 0)
        {
            var idx = 0;

            foreach (var itemS in source)
            {
                if (idx == index)
                    yield return item;

                yield return itemS;
                idx++;
            }
        }
    }
}
