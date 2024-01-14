using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class TreatmentPlan
    {
        public int PlanID { get; set; }
        public string PlanName { get; set; }
        public int PlanTimeInWeeks { get; set; }
        public double Price { get; set; }
        public string Notes { get; set; }
        public int InjuryID { get;set; }
        public int PatientID { get;set; }
        public int VisitID { get;set; }
        public string Keywords { get; set; }
        public DateTime Timestamp {  get; set; }

        public TreatmentPlan(int planID, string planName, int planTimeInWeeks, double price, string notes, string keywords, int injuryID, int patientId, int visitId, DateTime timestamp)
        {
            PlanID = planID;
            PlanName = planName;
            PlanTimeInWeeks = planTimeInWeeks;
            Price = price;
            Notes = notes;
            InjuryID = injuryID;
            PatientID = patientId;
            VisitID = visitId;
            Keywords = keywords;
            Timestamp = timestamp;
        }
        override public string ToString()
        {
            return PlanName;
        }

    }
}
