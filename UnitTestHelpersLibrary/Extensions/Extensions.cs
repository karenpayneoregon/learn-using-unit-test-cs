using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestHelpersLibrary.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Compare two dictionaries by keys and values
        /// </summary>
        /// <typeparam name="TKey">Generic type for key</typeparam>
        /// <typeparam name="TValue">Generic type for value</typeparam>
        /// <param name="d1">dictionary to compare to <see cref="d2"/></param>
        /// <param name="d2">dictionary to compare to <see cref="d1"/></param>
        /// <returns></returns>
        public static bool DictionaryEquals<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> d1, IReadOnlyDictionary<TKey, TValue> d2)
        {
            if (ReferenceEquals(d1, d2)) return true;
            if (d2 is null || d1.Count != d2.Count) return false;
            
            foreach (var (d1key, d1value) in d1)
            {
                if (!d2.TryGetValue(d1key, out TValue d2value)) return false;
                if (!d1value.Equals(d2value)) return false;
            }
            
            return true;
        }
    }
}
