using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomersLibrary.Classes;
using Newtonsoft.Json;

namespace DataLibrary.Classes
{
    public class JsonOperations
    {
        /// <summary>
        /// Serialize list of <see cref="CountryGrouped"/>
        /// </summary>
        /// <param name="countryGrouped">Populated list</param>
        /// <param name="fileName">File name to save serialized items too</param>
        public static void Save(List<CountryGrouped> countryGrouped, string fileName)
        {
            using var file = File.CreateText(fileName);
            var serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented
            };

            serializer.Serialize(file, countryGrouped);
        }
    }
}
