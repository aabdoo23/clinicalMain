using clinical.BaseClasses;
using System;

namespace clinical
{
    class globals
    {
        public globals()
        {
        }

        public static DateTime CalculateBirthdate(int age)
        {
            DateTime currentDate = DateTime.Now;

            int birthYear = currentDate.Year - age;

            DateTime birthdate = new DateTime(birthYear, currentDate.Month, currentDate.Day);

            return birthdate;
        }
        public static int generateNewPatientID()
        {
            DateTime dateTime = DateTime.Now;
            string s = dateTime.Day.ToString();
            s += dateTime.Month.ToString();
            s += dateTime.Year.ToString()[2];
            s += dateTime.Year.ToString()[3];
            s+= dateTime.Hour.ToString();
            s += dateTime.Minute.ToString();
            return int.Parse(s);
            
        }
        public static string generateNewPhysicianID(User ph )
        {
            DateTime dateTime = DateTime.Now;
            string s = "P";
            s += ph.FirstName[0];
            s += ph.FirstName[1];
            s += ph.NationalID[10];
            s += ph.NationalID[11];
            s += ph.NationalID[12];
            s += ph.NationalID[13];
            return s;
            
        }
        public static string generateNewEmployeeID(User ph)
        {
            DateTime dateTime = DateTime.Now;
            string s = "E";
            s += ph.FirstName[0];
            s += ph.FirstName[1];
            s += ph.NationalID[10];
            s += ph.NationalID[11];
            s += ph.NationalID[12];
            s += ph.NationalID[13];
            return s;
            
        }
        public static string generateNewAdminID(User ph)
        {
            string s = "A";
            s += ph.FirstName[0];
            s += ph.FirstName[1];
            return s;

        }
    }
}
