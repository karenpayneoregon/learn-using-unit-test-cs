using System;
using System.Linq;
using EntityFrameworkLibrary.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesUnitTestProject.Base;

namespace SalesUnitTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        /// <summary>
        /// Working with queries on dates, in this case looking for SaleDate greater than seven days ago
        /// and a specific country identifier.
        /// </summary>
        [TestMethod]
        [Timeout(2500)]
        public void TestMethod1()
        {
            var saleDate = new DateTime(2021, 6, 25);
            var shipCountry = 1;
            var expectedCount = 2;
            
            saleDate = saleDate.AddDays(-7);
            
            using var context = new DatabaseContext();

            var results = context
                .Sales
                .Where(sales => sales.SaleDate.Value.Date > saleDate.Date && 
                                sales.ShipCountry == shipCountry)
                .ToList();
            
            Assert.AreEqual(results.Count, expectedCount);
        }
        
    }
}
