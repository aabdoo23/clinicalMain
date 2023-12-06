using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class User
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string NationalID { get; set; }

        public User(string userID, string firstName, string lastName, string gender, DateTime hireDate, DateTime birthdate, string address, string phoneNumber, string email, string nationalID)
        {
            UserID = userID;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            HireDate = hireDate;
            Birthdate = birthdate;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            NationalID = nationalID;
        }



    }
}
