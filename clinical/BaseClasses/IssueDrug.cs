using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class IssueDrug
    {
        public int IssueID { get; set; }
        public int DrugID { get; set; }
        public int PrescriptionID { get; set; }
        public int PatientID { get; set; }
        public string Frequency { get; set; }
        public string Notes { get; set; }

        public IssueDrug(int issueID, int drugID, int prescriptionID, int patientID, string frequency, string notes)
        {
            IssueID = issueID;
            DrugID = drugID;
            PrescriptionID = prescriptionID;
            PatientID = patientID;
            Frequency = frequency;
            Notes = notes;
        }
    }
}
