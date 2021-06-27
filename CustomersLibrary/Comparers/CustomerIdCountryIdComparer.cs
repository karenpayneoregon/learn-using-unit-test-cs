using System;
using System.Collections.Generic;
using CustomersLibrary.Classes;

namespace CustomersLibrary.Comparers
{
    /// <summary>
    /// Comparer on Customer identifier and Customer CountryNavigation
    /// </summary>
    public class CustomerIdCountryNavigationComparer : IEqualityComparer<Customers>
    {
        
        public bool Equals(Customers x, Customers y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x is null)
            {
                return false;
            }

            if (y is null)
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.CustomerIdentifier == y.CustomerIdentifier && Equals(x.CountryNavigation, y.CountryNavigation);
        }

        public int GetHashCode(Customers customers) => 
            HashCode.Combine(customers.CustomerIdentifier, customers.CountryNavigation);
    }
}
