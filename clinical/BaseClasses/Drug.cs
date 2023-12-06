using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class Drug
    {
        public int DrugID { get; set; }
        public string DrugName { get; set; }
        public string NormalDosage { get; set; }
        public string Notes { get; set; }

        public Drug(int drugID, string drugName, string normalDosage, string notes)
        {
            DrugID = drugID;
            DrugName = drugName;
            NormalDosage = normalDosage;
            Notes = notes;
        }
    }
}
