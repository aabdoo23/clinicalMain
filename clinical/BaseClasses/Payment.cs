﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    internal class Payment
    {
        public int PaymentID { get; set; }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public int PhysicianID {  get; set; }
        public int PatientID {  get; set; }
        public string PatientName { get { return DB.GetPatientById(PatientID).FullName; } }
        public Payment(int paymentID, double amount, DateTime timeStamp, int physicianID, int patientID)
        {
            PaymentID = paymentID;
            Amount = amount;
            TimeStamp = timeStamp;
            PhysicianID = physicianID;
            PatientID = patientID;
        }

    }
}
