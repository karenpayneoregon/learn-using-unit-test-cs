using System.Collections.Generic;

namespace CustomersLibrary.Classes
{
    public class CountryGrouped
    {
        /// <summary>
        /// Count in group
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// List of Customers in the group
        /// </summary>
        public List<Customers> List { get; set; }
        /// <summary>
        /// Country name from country identifier
        /// </summary>
        public string CountryName { get; set; }

        public int CountryIdentifier { get; set; }
    }
}