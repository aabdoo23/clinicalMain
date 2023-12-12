using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public DateTime TimeStamp { get; set; }
        public int PatientID { get; set; }
        public int UserID { get; set; }
        public int VisitID { get; set; }
        public List<IssueDrug> IssuedDrugsIDs { get; set; }
        public List<IssueExercise> IssuedExercisesIDs { get; set; }

        // Constructor
        public Prescription(int prescriptionID, DateTime timeStamp, int patientID, int physiotherapistID, int visitID, List<IssueDrug> issuedDrugsIDs, List<IssueExercise> issuedExercisesIDs)
        {
            PrescriptionID = prescriptionID;
            TimeStamp = timeStamp;
            PatientID = patientID;
            UserID = physiotherapistID;
            VisitID = visitID;
            IssuedDrugsIDs = issuedDrugsIDs;
            IssuedExercisesIDs = issuedExercisesIDs;
        }
    }
}
