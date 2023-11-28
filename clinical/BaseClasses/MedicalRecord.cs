using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class MedicalRecord
    {
        public MedicalRecord(int recordID, int patientID, DateTime recordDate, string description, string type)
        {
            RecordID = recordID;
            PatientID = patientID;
            RecordDate = recordDate;
            Description = description;
            Type=type;
        }

        public int RecordID { get; set; }
        public int PatientID { get; set; }
        public DateTime RecordDate { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

       
    }
}