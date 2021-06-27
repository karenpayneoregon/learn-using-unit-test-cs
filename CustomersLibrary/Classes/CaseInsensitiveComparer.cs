using System;
using System.Collections.Generic;

namespace CustomersLibrary.Classes
{
    public class CaseInsensitiveComparer : IComparer<string>
    {
        public int Compare(string x, string y) => string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
    }
}
