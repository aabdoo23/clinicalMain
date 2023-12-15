using System;

namespace clinical.BaseClasses
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string NationalID { get; set; }

        public User(int userID, string firstName, string lastName, string gender, DateTime hireDate, DateTime birthdate, string address, string phoneNumber, string email, string nationalID)
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
        override public string ToString()
        {
            return UserID.ToString() + " " + FirstName;
        }


    }
}
