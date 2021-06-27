using System.Collections.Generic;
using CustomersLibrary.Classes;
using CustomersLibrary.Comparers;

namespace CustomersLibrary.ComparerHelpers
{
    public class Wrappers
    {
        /// <summary>
        /// <see cref="Customers"/> comparer for CompanyName
        /// </summary>
        public static IEqualityComparer<Customers> CompanyNameEqualityComparer =>
            Equality<Customers>.CreateComparer(customer => customer.CompanyName);

    }
}
