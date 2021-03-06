using System;
using System.Collections.Generic;

namespace CustomersLibrary.Comparers
{
    /// <summary>
    /// Provides code to create a generic IEqualityComparer
    /// https://gist.github.com/DForshner/5688879
    /// </summary>
    /// <typeparam name="T">Type to create comparer</typeparam>
    public static class Equality<T>
    {
        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector)
        {
            return CreateComparer(keySelector, null);
        }

        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return new KeyEqualityComparer<V>(keySelector, comparer);
        }

        class KeyEqualityComparer<V> : IEqualityComparer<T>
        {
            readonly Func<T, V> keySelector;
            readonly IEqualityComparer<V> comparer;

            public KeyEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
            {
                
                // ReSharper disable once JoinNullCheckWithUsage
                if (keySelector is null)
                {
                    throw new ArgumentNullException("keySelector");
                }

                this.keySelector = keySelector;
                this.comparer = comparer ?? EqualityComparer<V>.Default;
            }

            public bool Equals(T x, T y) => comparer.Equals(keySelector(x), keySelector(y));

            public int GetHashCode(T obj) => comparer.GetHashCode(keySelector(obj));
        }
    }
}
