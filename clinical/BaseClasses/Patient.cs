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
        public bool Referred { get; set; }
        public int PhysicianID { get; set; }
        public string PhysicianName { get; set; }
        public bool PreviouslyTreated { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double DueAmount { get; set; }
        public string referringName { get; set; }
        public string referringPN { get; set; }
        public int Age { get {
                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - Birthdate.Year;
                if (currentDate.Month < Birthdate.Month || (currentDate.Month == Birthdate.Month && currentDate.Day < Birthdate.Day))
                {
                    age--;
                }
                return age;
            }
        }
        public int ActivePackageID {  get; set; }
        public int RemainingSessions {  get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }

        public Patient(int patientID, string firstName, string lastname, 
            DateTime birthdate, string gender, string phoneNumber, 
            string email, string address, 
            int physicianId,
            bool referred, bool previouslyTreated, double height, 
            double weight, double dueAmount, string refN,string refPN, int activePackageID, int remainingSessions)
        {
            PatientID = patientID;
            FirstName = firstName;
            LastName = lastname;
            Birthdate = birthdate;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            PhysicianID = physicianId;
            Referred = referred;
            PreviouslyTreated = previouslyTreated;
            Height = height;
            Weight = weight;
            DueAmount = dueAmount;
            referringName = refN;
            referringPN=refPN;
            ActivePackageID = activePackageID;
            RemainingSessions=remainingSessions;
        }

        // calculate age based on birthdate
        
        
    }
}
