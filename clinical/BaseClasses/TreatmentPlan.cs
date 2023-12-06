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
        public int InjuryID { get; set; }
        public decimal Price { get; set; }
        public string Notes { get; set; }

        // Constructor
        public TreatmentPlan(int planID, string planName, int planTimeInWeeks, int injuryID, decimal price, string notes)
        {
            PlanID = planID;
            PlanName = planName;
            PlanTimeInWeeks = planTimeInWeeks;
            InjuryID = injuryID;
            Price = price;
            Notes = notes;
        }
    }
}
