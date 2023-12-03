using System;

namespace clinical.BaseClasses
{
    public class Patient
    {
        public Patient(int patientID, string name, string address, DateTime birthDate, string phoneNumber)
        {
            PatientID = patientID;
            Name = name;
            Address = address;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
        }
        public int Age => CalculateAge(BirthDate);

        private int CalculateAge(DateTime birthdate)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthdate.Year;
            if (birthdate > currentDate.AddYears(-age))
            {
                age--;
            }

            return age;
        }
        public int PatientID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
    }
}
