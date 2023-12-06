using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class MedicalRecord
    {
        public int RecordID { get; set; }
        public string Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Report { get; set; }
        public List<string> Images { get; set; }
        public int VisitID { get; set; }
        public int PatientID { get; set; }
        public string PhysicianNotes { get; set; }

        // Constructor
        public MedicalRecord(int recordID, string type, DateTime timeStamp, string report, List<string> images, int visitID, int patientID, string physicianNotes)
        {
            RecordID = recordID;
            Type = type;
            TimeStamp = timeStamp;
            Report = report;
            Images = images;
            VisitID = visitID;
            PatientID = patientID;
            PhysicianNotes = physicianNotes;
        }

    }
}
