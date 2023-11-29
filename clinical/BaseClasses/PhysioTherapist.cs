using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class PhysioTherapist{
        public PhysioTherapist(int employeeID, string name, DateTime dateOfBirth, string contactInfo, string nationalID, DateTime hireDate)
        {
            PhysioTherapistID = employeeID;
            Name = name;
            DateOfBirth = dateOfBirth;
            ContactInfo = contactInfo;
            NationalID = nationalID;
            HireDate = hireDate;
        }

        public int PhysioTherapistID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactInfo { get; set; }
        public string NationalID { get; set; }
        public DateTime HireDate { get; set; }
    }
}
