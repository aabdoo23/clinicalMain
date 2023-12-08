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

        public TreatmentPlan(int planID, string planName, int planTimeInWeeks, double price, string notes)
        {
            PlanID = planID;
            PlanName = planName;
            PlanTimeInWeeks = planTimeInWeeks;
            Price = price;
            Notes = notes;
        }
    }
}
