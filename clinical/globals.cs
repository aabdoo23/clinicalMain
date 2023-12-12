﻿using clinical.BaseClasses;
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
        public static int generateNewChatRoomID(int fID,int sID)
        {

            string s = fID.ToString().Substring(0, 3) + sID.ToString().Substring(0, 3);
            return Convert.ToInt32(s);
        }

       
    }
}
