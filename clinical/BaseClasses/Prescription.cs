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
        public int PhysiotherapistID { get; set; }
        public int VisitID { get; set; }
        public List<int> IssuedDrugsIDs { get; set; }
        public List<int> IssuedExercisesIDs { get; set; }

        // Constructor
        public Prescription(int prescriptionID, DateTime timeStamp, int patientID, int physiotherapistID, int visitID, List<int> issuedDrugsIDs, List<int> issuedExercisesIDs)
        {
            PrescriptionID = prescriptionID;
            TimeStamp = timeStamp;
            PatientID = patientID;
            PhysiotherapistID = physiotherapistID;
            VisitID = visitID;
            IssuedDrugsIDs = issuedDrugsIDs;
            IssuedExercisesIDs = issuedExercisesIDs;
        }
    }
}
