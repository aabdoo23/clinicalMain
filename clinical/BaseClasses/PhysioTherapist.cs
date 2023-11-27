using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class PhysioTherapist{
        public PhysioTherapist(int physioTherapistID, string name, string phoneNumber)
        {
            PhysioTherapistID = physioTherapistID;
            Name = name;
            PhoneNumber = phoneNumber;
        }

        public int PhysioTherapistID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
