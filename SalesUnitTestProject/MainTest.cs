using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkLibrary.Data;
using EntityFrameworkLibrary.Models;
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
        /// <remarks>
        /// Timeout attribute can be unpredictable thus commented out
        /// * An average may be 1.5 seconds while other times over 2.5 dependent on the environment
        /// </remarks>
        [TestMethod]
        //[Timeout(2500)]
        [TestTraits(Trait.SalesQueries)]
        public void SalesLastWeekByCountryCode()
        {
            var saleDate = new DateTime(2021, 6, 25);
            var shipCountry = 1;
            var expectedCount = 2;
            
            saleDate = saleDate.AddDays(-7);
            
            using var context = new DatabaseContext();

            List<Sales> results = context
                .Sales
                .Where(sales => sales.SaleDate.Value.Date > saleDate.Date && 
                                sales.ShipCountry == shipCountry)
                .ToList();
            
            Assert.AreEqual(results.Count, expectedCount);
        }
        
    }
}
