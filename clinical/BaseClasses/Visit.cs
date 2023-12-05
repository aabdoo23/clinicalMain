using System;

namespace clinical.BaseClasses
{
    class Visit
    {
        public Visit(int visitID, int patientID, int physioTherapistID, DateTime date, string reason, string doctorNotes, int roomID)
        {
            VisitID = visitID;
            PatientID = patientID;
            Date = date;
            Reason = reason;
            DoctorNotes = doctorNotes;
            PhysioTherapistID = physioTherapistID;
            RoomID = roomID;
        }

        public int VisitID { get; set; }
        public int PatientID { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public string DoctorNotes { get; set; }
        public int PhysioTherapistID { get; set; }
        public int RoomID { get; set; }
        
    }
}
