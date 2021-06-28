using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// ReSharper disable once CheckNamespace - do not change
namespace TestProject1
{
    public partial class MainTest
    {
        /// <summary>
        /// File name for test <see cref="CreateJsonHardCoded_CustomersGroupByCountryIdentifier"/>
        /// </summary>
        public string CountryGroupFileName => "CustomerCountryGroup.json";
        
        /// <summary>
        /// Perform initialization before test runs using assertion on current test name.
        /// </summary>
        [TestInitialize]
        public void Initialization()
        {
            if (TestContext.TestName == nameof(CreateJsonHardCoded_CustomersGroupByCountryIdentifier) || TestContext.TestName == nameof(CreateJsonGeneric_CustomersGroupByCountryIdentifier))
            {
                if (File.Exists(CountryGroupFileName))
                {
                    File.Delete(CountryGroupFileName);
                }
            }
        }

        /// <summary>
        /// Perform cleanup after test runs using assertion on current test name.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {

        }
        /// <summary>
        /// Perform any initialize for the class
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }
    }
}
