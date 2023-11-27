using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class Employee
    {
        public Employee(int employeeID, string name, DateTime dateOfBirth, string contactInfo, DateTime hireDate)
        {
            EmployeeID = employeeID;
            Name = name;
            DateOfBirth = dateOfBirth;
            ContactInfo = contactInfo;
            HireDate = hireDate;
        }

        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactInfo { get; set; }
        public DateTime HireDate { get; set; }
    }
}
