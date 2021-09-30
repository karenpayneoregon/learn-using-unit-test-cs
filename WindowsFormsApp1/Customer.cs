using System;

namespace WindowsFormsApp1
{
    public class Customer
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public DateTime BirthDate { get; internal set; }
    }
}