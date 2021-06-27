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
    public class CustomerOperations
    {
        /// <summary>
        /// Read json data to create a list of customers
        /// </summary>
        /// <param name="contactTypeList"><see cref="ContactType"/> list</param>
        /// <param name="contactList"><see cref="Contacts"/> list</param>
        /// <param name="countriesList"><see cref="Countries"/>list</param>
        /// <returns>Populated list of <see cref="Customers"/></returns>
        private static List<Customers> GetModelDataFromJson(out List<ContactType> contactTypeList, out List<Contacts> contactList, out List<Countries> countriesList)
        {
            var customerJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Customers.json"));
            var customersList = JsonConvert.DeserializeObject<List<Customers>>(customerJson);

            var contactTypeJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "ContactType.json"));
            contactTypeList = JsonConvert.DeserializeObject<List<ContactType>>(contactTypeJson);

            var contactJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Contacts.json"));
            contactList = JsonConvert.DeserializeObject<List<Contacts>>(contactJson);

            var countriesJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Countries.json"));
            countriesList = JsonConvert.DeserializeObject<List<Countries>>(countriesJson);

            return customersList;

        }

        public static List<CustomerEntity> ReadCustomersWithJoins()
        {
            var customerJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Customers.json"));
            var customersList = JsonConvert.DeserializeObject<List<Customers>>(customerJson);

            var contactTypeJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "ContactType.json"));
            var contactTypeList = JsonConvert.DeserializeObject<List<ContactType>>(contactTypeJson);

            var contactJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Contacts.json"));
            var contactList = JsonConvert.DeserializeObject<List<Contacts>>(contactJson);

            var countriesJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Countries.json"));
            var countriesList = JsonConvert.DeserializeObject<List<Countries>>(countriesJson);

            Console.WriteLine();

            List<CustomerEntity> customerData = (

                from customer in customersList

                join contact in contactList on customer.ContactId equals contact.ContactId
                join contactType in contactTypeList on customer.ContactTypeIdentifier equals contactType.ContactTypeIdentifier
                join country in countriesList on customer.CountryIdentifier equals country.CountryIdentifier

                select new CustomerEntity
                {
                    CustomerIdentifier = customer.CustomerIdentifier,
                    CompanyName = customer.CompanyName,
                    ContactIdentifier = customer.ContactId,
                    ContactTitle = contactType.ContactTitle,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Address = customer.Street,
                    City = customer.City,
                    PostalCode = customer.PostalCode,
                    CountryIdentifier = customer.CountryIdentifier,
                    CountyName = customer.CountryName,
                    ContactTypeNavigation = contactType,
                    Contact = contact
                }).ToList();

            return customerData;

        }
        public static List<Customers> ReadCustomers()
        {
            var customersList = GetModelDataFromJson(out var contactTypeList, out var contactList, out var countriesList);

            foreach (var customer in customersList)
            {

                var contactType = contactTypeList!.FirstOrDefault(x => x.ContactTypeIdentifier == customer.ContactTypeIdentifier);
                if (contactType is not null)
                {
                    customer.ContactTypeNavigation.ContactTitle = contactType.ContactTitle ?? "???";
                }

                var contact = contactList!.FirstOrDefault(x => x.ContactId == customer.ContactIdentifier);
                if (contact is not null)
                {
                    customer.Contact = contact;
                    customer.ContactId = contact.ContactId;
                }

                var country = countriesList!.FirstOrDefault(x => x.CountryIdentifier == customer.CountryIdentifier);
                if (country is not null)
                {
                    customer.CountryName = country.Name;
                    customer.CountryNavigation = country;
                }

            }

            return customersList;

        }

        protected static string JsonFolder => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json");

        private static List<Contacts> Contacts() =>
            JsonConvert.DeserializeObject<List<Contacts>>(File.ReadAllText(
                Path.Combine(JsonFolder, "Contacts.json")));
        private static List<ContactType> ContactTypes() =>
            JsonConvert.DeserializeObject<List<ContactType>>(File.ReadAllText(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "ContactType.json")));

        public static List<Countries> CountryList =>
            JsonConvert.DeserializeObject<List<Countries>>(File.ReadAllText(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Countries.json")));


    }
}
