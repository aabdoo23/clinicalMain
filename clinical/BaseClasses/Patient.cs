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
        public int PhysicianID { get; set; }
        public string PhysicianName { get; set; }
        public bool PreviouslyTreated { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double DueAmount { get; set; }
        public string referringName { get; set; }
        public string referringPN { get; set; }
        public int age { get; set; }

        // Constructor
        public Patient(int patientID, string firstName, string lastname, 
            DateTime birthdate, string gender, string phoneNumber, 
            string email, string address, 
            List<int> chronicDiseasesIDs, 
            List<int> previousInjuriesIDs, 
            int physicianId,
            bool referred, bool previouslyTreated, double height, 
            double weight, double dueAmount, string refN,string refPN)
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
            PhysicianID = physicianId;
            Referred = referred;
            PreviouslyTreated = previouslyTreated;
            Height = height;
            Weight = weight;
            DueAmount = dueAmount;
            referringName = refN;
            referringPN=refPN;
            this.age = Age();
        }

        // calculate age based on birthdate
        public int Age()
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - Birthdate.Year;
            if (currentDate.Month < Birthdate.Month || (currentDate.Month == Birthdate.Month && currentDate.Day < Birthdate.Day))
            {
                age--;
            }
            this.age = age;
            return age;
        }
        

        public string chronics()
        {
            string s="";
            foreach(int i in ChronicDiseasesIDs)
            {
                s += i.ToString();
                s += ", ";
            }
            return s;
        }

        public string injuries()
        {
            string s = "";
            foreach (int i in PreviousInjuriesIDs)
            {
                s += i.ToString();
                s += ", ";
            }
            return s;
        }
    }
}
