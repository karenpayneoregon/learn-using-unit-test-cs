using System;
using System.Collections.Generic;
using CustomersLibrary.Classes;

namespace CustomersLibrary.Comparers
{
    public class CustomerIdContactIdCompare : IEqualityComparer<Customers>
    {
        public bool Equals(Customers x, Customers y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.CustomerIdentifier == y.CustomerIdentifier && x.ContactId == y.ContactId;
        }

        public int GetHashCode(Customers obj)
        {
            return HashCode.Combine(obj.CustomerIdentifier, obj.ContactId);
        }
    }
}