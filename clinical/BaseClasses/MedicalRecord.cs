using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class MedicalRecord
    {
        MedicalRecord(int recordID, int patientID, DateTime recordDate, string description)
        {
            RecordID = recordID;
            PatientID = patientID;
            RecordDate = recordDate;
            Description = description;
        }

        public int RecordID { get; set; }
        public int PatientID { get; set; }
        public DateTime RecordDate { get; set; }
        public string Description { get; set; }

       
    }
}