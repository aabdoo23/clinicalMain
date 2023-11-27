using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class Patient
    {
        Patient(int patientID, string name, string address, int medicalRecordID, DateTime birthDate, string phoneNumber){
            PatientID = patientID;
            Name = name;
            Address = address;
            MedicalRecordID = medicalRecordID;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
        }

        public int PatientID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int MedicalRecordID { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
    }
}
