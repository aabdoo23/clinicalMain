using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<int> ChronicDiseasesIDs { get; set; }
        public List<int> PreviousInjuriesIDs { get; set; }
        public bool Referred { get; set; }
        public User Physician { get; set; }
        public bool PreviouslyTreated { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double DueAmount { get; set; }

        // Constructor
        public Patient(int patientID, string firstName, string lastname, 
            DateTime birthdate, string gender, string phoneNumber, 
            string email, string address, 
            List<int> chronicDiseasesIDs, 
            List<int> previousInjuriesIDs, 
            User physician,
            bool referred, bool previouslyTreated, double height, 
            double weight, double dueAmount)
        {
            PatientID = patientID;
            FirstName = firstName;
            LastName = lastname;
            Birthdate = birthdate;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            ChronicDiseasesIDs = chronicDiseasesIDs;
            PreviousInjuriesIDs = previousInjuriesIDs;
            Physician = physician;
            Referred = referred;
            PreviouslyTreated = previouslyTreated;
            Height = height;
            Weight = weight;
            DueAmount = dueAmount;
        }

        // Method to calculate age based on birthdate
        public int Age()
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - Birthdate.Year;
            if (currentDate.Month < Birthdate.Month || (currentDate.Month == Birthdate.Month && currentDate.Day < Birthdate.Day))
            {
                age--;
            }
            return age;
        }
    }
}
