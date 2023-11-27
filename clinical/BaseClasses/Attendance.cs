using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class Attendance
    {
        Attendance(int iD, int physioTherapistID, bool isPresent)
        {
            ID = iD;
            PhysioTherapistID = physioTherapistID;
            IsPresent = isPresent;
        }

        public int ID { get; set; }
        public int PhysioTherapistID { get; set; }
        public bool IsPresent { get; set; }
    }
}
