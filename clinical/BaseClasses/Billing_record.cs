using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class Billing_record
    {
        public Billing_record(int billingID, DateTime date, float amount, string description)
        {
            BillingID = billingID;
            Date = date;
            Amount = amount;
            Description = description;
        }

        public int BillingID { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }

    }
}
