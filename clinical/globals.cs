using clinical.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical
{
    class globals
    {
        public static ObservableCollection<PhysioTherapist> sampleTherapists = new ObservableCollection<PhysioTherapist>();
        public static ObservableCollection<Employee> sampleEmployees = new ObservableCollection<Employee>();
        public static ObservableCollection<Patient> samplePatients= new ObservableCollection<Patient>();
        public static ObservableCollection<Visit> sampleAppointments = new ObservableCollection<Visit>();
        public globals()
        {
            sampleTherapists.Add(new PhysioTherapist(123,"DR. Ahmed Mohamed",DateTime.Now,"01002342252","14141414141414141",DateTime.Today));
            sampleTherapists.Add(new PhysioTherapist(456,"DR. Mohamed Ahmed",DateTime.Now,"02342252","14141414141",DateTime.Today));
            sampleTherapists.Add(new PhysioTherapist(789,"DR. Ahmed Ahmed",DateTime.Now,"0100234123123","1414141231224141",DateTime.Today));
            sampleEmployees.Add(new Employee(12, "Ahmed Mohamed", DateTime.Now, "01002342252","Reciptionist", "1414221414144141", DateTime.Today));
            sampleEmployees.Add(new Employee(13, "Ibrahim Mohamed", DateTime.Now, "01002222252","Reciptionist", "14142221141414141", DateTime.Today));
            sampleEmployees.Add(new Employee(23, "Abdelrahman Mohamed", DateTime.Now, "022221002342252","Reciptionist", "14141222414141414141", DateTime.Today));
            samplePatients.Add(new Patient(111, "Ahdam", "30 share3 elbasateen", DateTime.MinValue, "20202020200"));
            samplePatients.Add(new Patient(222, "Agmad", "40 share3 elbasateen", DateTime.MinValue, "2020200"));
            samplePatients.Add(new Patient(333, "Aswa2", "50 share3 elbasateen", DateTime.MinValue, "20020200"));
            sampleAppointments.Add(new Visit(1,111,123,DateTime.Now,"Ankle Visit","Good patient, great Patient",9));
            sampleAppointments.Add(new Visit(2,222,456,DateTime.Now,"Ankle Visit","Good patient, great Patient",8));
            sampleAppointments.Add(new Visit(3,333,456,DateTime.Now,"Ankle Visit","Good patient, great Patient",7));
        }

    }
}
