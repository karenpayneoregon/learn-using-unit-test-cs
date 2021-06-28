using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Base
{
    public class SqlOperations
    {
        protected static string _connectionString => "Server=.\\SQLEXPRESS;Database=NorthWind2020;Integrated Security=true";

        public static Dictionary<string, int> CountryCountDictionary()
        {
            Dictionary<string, int> results = new ();

            using var cn = new SqlConnection() { ConnectionString = _connectionString };
            using var cmd = new SqlCommand
            {
                Connection = cn, 
                CommandText = "SELECT C.[Name], COUNT(C.Name) AS CountryCount FROM Customers AS Cust INNER JOIN Countries AS C ON Cust.CountryIdentifier = C.CountryIdentifier GROUP BY C.[Name] ORDER BY C.[Name];"
            };

            cn.Open();

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                results.Add(reader.GetString(0), reader.GetInt32(1));
            }


            return results;
        }
    }
}
