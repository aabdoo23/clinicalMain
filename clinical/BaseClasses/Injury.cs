using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class Injury
    {
        public int InjuryID { get; set; }
        public string InjuryName { get; set; }
        public string InjuryLocation { get; set; }
        public int Severity { get; set; }

        public Injury(int injuryID, string injuryName, string injuryLocation, int severity)
        {
            InjuryID = injuryID;
            InjuryName = injuryName;
            InjuryLocation = injuryLocation;
            Severity = severity;
        }
    }
}
