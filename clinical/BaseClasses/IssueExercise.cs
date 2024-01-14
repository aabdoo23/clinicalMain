using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class IssueExercise
    {
        public int IssueID { get; set; }
        public int ExerciseID { get; set; }
        public int PatientID { get; set; }
        public int TreatmentPlanID { get; set; }
        public string Frequency { get; set; }
        public string Notes { get; set; }

        // Constructor
        public IssueExercise(int issueID, int exerciseID, int patientID, int planID, string frequency, string notes)
        {
            IssueID = issueID;
            ExerciseID = exerciseID;
            PatientID = patientID;
            TreatmentPlanID = planID;
            Frequency = frequency;
            Notes = notes;
        }
    }
}
