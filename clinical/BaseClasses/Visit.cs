using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class Visit
    {
        public int VisitID { get; set; }
        public int PhysiotherapistID { get; set; }
        public int PatientID { get; set; }
        public int PackageID { get; set; }
        public DateTime TimeStamp { get; set; }
        
        public int RoomID { get; set; }
        public string Type { get; set; }
        public string TherapistNotes { get; set; }
        public string PatientName { get; set; }
        public string PhysioTherapistName { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        // Constructor
        public Visit(int visitID, int physiotherapistID, int patientID, int packageID, DateTime timeStamp, int roomID, string type, string therapistNotes, double height, double weight)
        {
            VisitID = visitID;
            PhysiotherapistID = physiotherapistID;
            PatientID = patientID;
            PackageID = packageID;
            TimeStamp = timeStamp;
            RoomID = roomID;
            Type = type;
            TherapistNotes = therapistNotes;
            this.Weight=weight; 
            this.Height=height;
        }
    }
}
