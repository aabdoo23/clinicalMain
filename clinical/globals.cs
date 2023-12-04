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
        }

    }
}
