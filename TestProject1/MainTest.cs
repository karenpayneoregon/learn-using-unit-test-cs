using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CustomersLibrary.Classes;
using CustomersLibrary.ComparerHelpers;
using CustomersLibrary.Comparers;
using DataLibrary.Classes;
using DeepEqual.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Base;

namespace TestProject1
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        /// <summary>
        /// Returns distinct elements from a sequence of Customers for <see cref="Wrappers.CompanyNameEqualityComparer"/>
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.QueryOperators)]
        public void Distinct_By_CompanyName()
        {
            var customerList = CustomerOperations.ReadCustomers().Take(5).ToList();

            customerList[1].CompanyName = customerList[0].CompanyName;

            var noDuplicateCustomers = customerList.Distinct(
                Wrappers.CompanyNameEqualityComparer);

            Assert.IsTrue(noDuplicateCustomers.Count() == 4);

        }

        /// <summary>
        /// <see cref="CustomerIdCountryNavigationComparer"/> test
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.QueryOperators)]
        public void Distinct_Identifier_CountryProperties()
        {
            var customerList = CustomerOperations.ReadCustomers().Take(5).ToList();

            customerList[1].CustomerIdentifier = customerList[0].CustomerIdentifier;
            customerList[1].CountryNavigation = customerList[0].CountryNavigation;


            var noDuplicateCustomers = customerList.Distinct(
                new CustomerIdCountryNavigationComparer());

            Assert.IsTrue(noDuplicateCustomers.Count() == 4);

        }

        /// <summary>
        /// Empty: returns an empty IEnumerable&lt;T&gt; that has the specified type argument.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.QueryOperators)]
        public void EmptyAggregate_CompanyName()
        {
            var customerList = CustomerOperations.ReadCustomers();

            string[] names1 = customerList.Take(2).Select(customer => customer.CompanyName).ToArray();
            string[] names2 = customerList.Skip(2).Take(7).Select(customer => customer.CompanyName).ToArray();
            string[] names3 = customerList.Skip(9).Take(9).Select(customer => customer.CompanyName).ToArray();

            List<string[]> customerNamesList = new List<string[]> { names1, names2, names3 };

            // Only include arrays that have four or more elements
            IEnumerable<string> allNames = customerNamesList
                .Aggregate(Enumerable.Empty<string>(), (current, next) => next.Length > 3 ?
                    current.Union(next) :
                    current);

            Assert.AreEqual(allNames.Count(), 16);
        }

        /// <summary>
        /// Except: produces the set difference of two sequences.
        /// </summary>
        /// <remarks>
        /// Uses NuGet package https://www.nuget.org/packages/DeepEqual/
        /// </remarks>
        [TestMethod]
        [TestTraits(Trait.QueryOperators)]
        public void ExceptExample()
        {
            List<Customers> customersList1 = CustomerOperations.ReadCustomers().Take(3).ToList();
            List<Customers> customersList2 = CustomerOperations.ReadCustomers().Skip(2).Take(1).ToList();
            List<Customers> expected = CustomerOperations.ReadCustomers().Take(2).ToList();

            var results = customersList1.Except(customersList2, new CustomerIdContactIdCompare()).ToList();

            Assert.IsTrue(expected.IsDeepEqual(results));

        }

        /// <summary>
        /// Enumerable.GroupBy: Groups the elements of a sequence.
        /// Group Customers by Country
        /// </summary>
        /// <remarks>
        /// Overloads
        /// https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.groupby?view=net-5.0
        /// </remarks>
        [TestMethod]
        [TestTraits(Trait.QueryOperators)]
        public void GroupByExample1()
        {
            var customers = CustomerOperations.ReadCustomers();
            var stronglyGrouped = customers
                .GroupBy((customer) => customer.CountryIdentifier)
                .Select((@group) => new CountryGrouped
                {
                    Count = @group.Count(),
                    List = @group.ToList(),
                    CountryName = CustomerOperations.CountryList.FirstOrDefault(country => country.CountryIdentifier == @group.Key).Name
                })
                .OrderBy(countryGrouped => countryGrouped.CountryName)
                .ToList();

            /*
             * Not part of a typical unit test method, here to show that
             * a developer can inspect results after a test runs in the test
             * output/result window
             */
            foreach (var countryGroup in stronglyGrouped)
            {
                Debug.WriteLine(countryGroup.CountryName);
                foreach (var customer in countryGroup.List)
                {
                    Debug.WriteLine($"\t{customer.CompanyName}");
                }
            }

            Assert.IsTrue(stronglyGrouped.Count == 20);
        }

        /// <summary>
        /// Enumerable.GroupBy: Groups the elements of a sequence.
        /// Group Customers by Country
        /// </summary>
        /// <remarks>
        /// Overloads
        /// https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.groupby?view=net-5.0
        /// </remarks>
        [TestMethod]
        [TestTraits(Trait.QueryOperators)]
        public void CustomersGroupBy_CountryIdentifier()
        {
            var customers = CustomerOperations.ReadCustomers();
            var anonymousGrouped = customers
                .GroupBy((customer) => customer.CountryIdentifier)
                .Select((@group) => new
                {
                    Count = @group.Count(),
                    List = @group.ToList(),
                    CountryName = CustomerOperations.CountryList.FirstOrDefault(country => 
                        country.CountryIdentifier == @group.Key).Name
                })
                .OrderBy(countryGrouped => countryGrouped.CountryName)
                .ToList();

            /*
             * Not part of a typical unit test method, here to show that
             * a developer can inspect results after a test runs in the test
             * output/result window
             */
            foreach (var countryGroup in anonymousGrouped)
            {
                Debug.WriteLine(countryGroup.CountryName);
                
                foreach (var customer in countryGroup.List)
                {
                    Debug.WriteLine($"\t{customer.CompanyName}");
                }
            }

            Assert.IsTrue(anonymousGrouped.Count == 20);


        }

        /// <summary>
        /// Working with OfType
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.QueryOperators)]
        public void OfTypeExample()
        {
            List<object> objects = new()
            {
                new CustomerEntity(),
                new Customers(),
                new ContactType(),
                new Customers()
            };

            Assert.IsTrue(objects.OfType<Customers>().Count() == 2);

        }

    }
}
