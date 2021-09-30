using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormApp2Lib.Classes
{
    public class Friend
    {
        public Friend(string firstName, string middleName, string lastName) => (FirstName, MiddleName, LastName) = (firstName, middleName, lastName);

        public void Deconstruct(out string firstName, out string middleName, out string lastName) => 
            (firstName, middleName, lastName) = (FirstName, MiddleName, LastName);

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
