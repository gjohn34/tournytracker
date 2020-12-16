using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Model
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string CellPhoneNumber { get; set; }
        public PersonModel()
        {

        }
        public PersonModel(string firstName, string lastName, string email, string cellphone)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = email;
            CellPhoneNumber = cellphone;
        }

    }
}
