using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    internal class Report
    {
        public int ReportId { get; set; }
        public int Physio_Therapist_Id { get; set; } // hena fe kemala bardo t2rebn id
        public int PatientId { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public int Visit_Id { get; set; }
        public DateTime Date { get; set; }
    }
}
