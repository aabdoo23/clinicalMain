using clinical.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class EvaluationTestFeedBack
    {
        public int TestFeedBackID { get; set; }
        public int Severity {  get; set; }
        public int VisitID {  get; set; }
        public int PatientID {  get; set; }
        public int TestID { get; set; }

        public EvaluationTestFeedBack(int testFeedBackID, int severity, int visitID, int patientID, int testID)
        {
            TestFeedBackID = testFeedBackID;
            Severity = severity;
            VisitID = visitID;
            PatientID = patientID;
            TestID = testID;
        }
    }
}
