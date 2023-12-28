using clinical.BaseClasses;
using System;
using System.Security.Cryptography;

namespace clinical
{
    class globals
    {
        public globals()
        {
        }

        public static User signedIn=null;
        public static DateTime CalculateBirthdate(int age)
        {
            DateTime currentDate = DateTime.Now;

            int birthYear = currentDate.Year - age;

            DateTime birthdate = new DateTime(birthYear, currentDate.Month, currentDate.Day);

            return birthdate;
        }

        public static int generateNewRecordID(int patientID)
        {
            DateTime dateTime = DateTime.Now;
            string s="";
            s += patientID.ToString().Substring(0,4);
            s += dateTime.DayOfYear.ToString();
            
            return int.Parse(s);

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
        public static int generateNewPhysicianID(string nid )
        {
            DateTime dateTime = DateTime.Now;
            string s = "2";
            s += nid[10];
            s += nid[11];
            s += nid[12];
            s += nid[13];
            return Convert.ToInt32(s);
            
        }
        public static int generateNewEmployeeID(string nid)
        {
            DateTime dateTime = DateTime.Now;
            string s = "3";
            s += nid[10];
            s += nid[11];
            s += nid[12];
            s += nid[13];
            return Convert.ToInt32(s);
            
        }
        public static string generateNewAdminID()
        {
            string s = "1";
            Random rand = new Random();
            s+=rand.Next(100).ToString();
            return s;

        }

        public static int generateNewVisitID(int patID, DateTime time) {
            string s = patID.ToString().Substring(0, 3) + time.Month+time.Day;
            return Convert.ToInt32(s);
        }

        public static int generateNewPrescriptionID(int visitID, DateTime time)
        {
            string s = visitID.ToString().Substring(0, 3) + time.Day+time.Second;
            return Convert.ToInt32(s);
        }

        public static int generateNewIssueExerciseID(int prescriptionID,int patientID)
        {
            string s = prescriptionID.ToString().Substring(0, 2)+new Random().Next(99).ToString()+ patientID.ToString().Substring(0, 2)+ new Random().Next(99).ToString();
            return Convert.ToInt32(s);
        }

        public static int generateNewExerciseID()
        {
            string s = new Random().Next(99).ToString() + new Random().Next(99).ToString()+ new Random().Next(81).ToString();
            return Convert.ToInt32(s);
        }
        public static int generateNewChatRoomID(int fID,int sID)
        {

            string s = fID.ToString().Substring(0, 3) + sID.ToString().Substring(0, 3);
            return Convert.ToInt32(s);
        }



        public static int generateNewChatMessageID(int senderID)
        {

            string s = senderID.ToString().Substring(0, 1) + DateTime.Now.Second.ToString()+ DateTime.Now.Millisecond.ToString();
            return Convert.ToInt32(s);
        }



    }
}
