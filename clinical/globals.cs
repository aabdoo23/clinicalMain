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
        public static string generateNewPhysicianID(string fn,string nid )
        {
            DateTime dateTime = DateTime.Now;
            string s = "P";
            s += fn[0];
            s += fn[1];
            s += nid[10];
            s += nid[11];
            s += nid[12];
            s += nid[13];
            return s;
            
        }
        public static string generateNewEmployeeID(string fn, string nid)
        {
            DateTime dateTime = DateTime.Now;
            string s = "E";
            s += fn[0];
            s += fn[1];
            s += nid[10];
            s += nid[11];
            s += nid[12];
            s += nid[13];
            return s;
            
        }
        public static string generateNewAdminID(string fn)
        {
            string s = "A";
            s += fn[0];
            s += fn[1];
            return s;

        }
    }
}
