using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CustomersLibrary.Classes;
using CustomersLibrary.ComparerHelpers;
using CustomersLibrary.Comparers;
using DataLibrary.Classes;
using DataLibrary.Extensions;
using DeepEqual.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.Base;
using UnitTestHelpersLibrary.Extensions;


namespace TestProject1
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        /// <summary>
        /// Returns distinct elements from a sequence of Customers for <see cref="Wrappers.CompanyNameEqualityComparer"/>
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Distinct)]
        public void Distinct_By_CompanyName()
        {
            List<Customers> customerList = CustomerOperations.ReadCustomers().Take(5).ToList();

            customerList[1].CompanyName = customerList[0].CompanyName;

            IEnumerable<Customers> noDuplicateCustomers = customerList.Distinct(
                Wrappers.CompanyNameEqualityComparer);

            Assert.IsTrue(noDuplicateCustomers.Count() == 4);

        }

        /// <summary>
        /// <see cref="CustomerIdCountryNavigationComparer"/> test
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Distinct)]
        public void Distinct_Identifier_CountryProperties()
        {
            List<Customers> customerList = CustomerOperations.ReadCustomers().Take(5).ToList();

            customerList[1].CustomerIdentifier = customerList[0].CustomerIdentifier;
            customerList[1].CountryNavigation = customerList[0].CountryNavigation;


            IEnumerable<Customers> noDuplicateCustomers = customerList.Distinct(
                new CustomerIdCountryNavigationComparer());

            Assert.IsTrue(noDuplicateCustomers.Count() == 4);

        }

        /// <summary>
        /// Empty: returns an empty IEnumerable&lt;T&gt; that has the specified type argument.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Aggregate)]
        public void EmptyAggregate_CompanyName()
        {
            List<Customers> customerList = CustomerOperations.ReadCustomers();

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
        [TestTraits(Trait.Except)]
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
        [TestTraits(Trait.GroupBy)]
        public void CustomersGroupByCountryIdentifierStrongTyped()
        {
            List<Customers> customers = CustomerOperations.ReadCustomers();
            List<CountryGrouped> stronglyGrouped = customers
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
        ///
        /// Note, there are several asserts within in this method, <see cref="Extensions.DictionaryEquals()"/>
        /// is the best option as it compares by count, keys and values
        /// </summary>
        /// <remarks>
        /// Overloads
        /// https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.groupby?view=net-5.0
        /// </remarks>
        [TestMethod]
        [TestTraits(Trait.GroupBy)]
        public void CustomersGroupByCountryIdentifierAnonymous()
        {
            List<Customers> customers = CustomerOperations.ReadCustomers();
            
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

            Dictionary<string, int> results = anonymousGrouped.ToDictionary(countryGroup => countryGroup.CountryName, countryGroup => countryGroup.Count);


            //CollectionAssert.AreEqual(
            //    results.OrderBy(kv => kv.Key).ToList(),
            //    SqlOperations.CountryCountDictionary().OrderBy(kv => kv.Key).ToList()
            //);

            var expected = SqlOperations.CountryCountDictionary();
            //Assert.IsTrue(results.Count == expected.Count && !results.Except(expected).Any());
            
            Assert.IsTrue(results.DictionaryEquals(expected));

        }

        /// <summary>
        /// Validate <see cref="SqlOperations.CountryCountDictionary"/> works correctly for use
        /// with confirmation of test methods above work correctly
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.SqlRead)]
        public void TempTest()
        {
            SqlOperations.CountryCountDictionary();
        }


        /// <summary>
        /// Working with OfType
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Generics)]
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
        #region Two different ways to serialize a list

        [TestMethod]
        [TestTraits(Trait.ToJson)]
        public void CreateJsonHardCoded_CustomersGroupByCountryIdentifier()
        {
            List<Customers> customers = CustomerOperations.ReadCustomers();
            List<CountryGrouped> stronglyGrouped = customers
                .GroupBy((customer) => customer.CountryIdentifier)
                .Select((@group) => new CountryGrouped
                {
                    Count = @group.Count(),
                    List = @group.ToList(),
                    CountryName = CustomerOperations.CountryList.FirstOrDefault(country => country.CountryIdentifier == @group.Key).Name
                })
                .OrderBy(countryGrouped => countryGrouped.CountryName)
                .ToList();

            JsonOperations.Save(stronglyGrouped, CountryGroupFileName);

        }

        [TestMethod]
        [TestTraits(Trait.ToJson)]
        public void CreateJsonGeneric_CustomersGroupByCountryIdentifier()
        {
            List<Customers> customers = CustomerOperations.ReadCustomers();
            List<CountryGrouped> stronglyGrouped = customers
                .GroupBy((customer) => customer.CountryIdentifier)
                .Select((@group) => new CountryGrouped
                {
                    Count = @group.Count(),
                    List = @group.ToList(),
                    CountryName = CustomerOperations.CountryList.FirstOrDefault(country => country.CountryIdentifier == @group.Key).Name
                })
                .OrderBy(countryGrouped => countryGrouped.CountryName)
                .ToList();


            stronglyGrouped.ModeListToJson(CountryGroupFileName);

        }

        #endregion

    }
}
