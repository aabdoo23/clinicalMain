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
        public int AppointmentTypeID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Type { get { return DB.GetAppointmentTypeByID(AppointmentTypeID).Name; } }
        public int RoomID { get; set; }
        public string TherapistNotes { get; set; }
        public string PatientName { get { return DB.GetPatientById(PatientID).FullName;} }
        public string PhysioTherapistName { get {return DB.GetUserById(PhysiotherapistID).FullName;} }
        public double Height { get; set; }
        public double Weight { get; set; }
        public bool IsDone { get; set; }

        // Constructor
        public Visit(int visitID, int physiotherapistID, int patientID, int packageID, DateTime timeStamp, int roomID, string therapistNotes, double height, double weight, bool isDone, int appointmentTypeId)
        {
            this.VisitID = visitID;
            this.PhysiotherapistID = physiotherapistID;
            this.PatientID = patientID;
            this.PackageID = packageID;
            this.TimeStamp = timeStamp;
            this.RoomID = roomID;
            this.TherapistNotes = therapistNotes;
            this.Weight=weight; 
            this.Height=height;
            this.IsDone = isDone;
            this.AppointmentTypeID = appointmentTypeId;
        }
    }
}
