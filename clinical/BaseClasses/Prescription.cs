using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class Prescription
    {
        public int PrescriptionID { get; set; }
        public int PhysioTherapistID {  get; set; }    
        public int PatientID {  get; set; }
        public int VisitID {  get; set; }
        public ObservableCollection<Drug> DrugList {  get; set; }
        public Prescription(int prescriptionID, int physioTherapistID, int patientID, int visitID, ObservableCollection<Drug> drugList)
        {
            PrescriptionID = prescriptionID;
            PhysioTherapistID = physioTherapistID;
            PatientID = patientID;
            VisitID = visitID;
            DrugList = drugList;
        }
    }
}
