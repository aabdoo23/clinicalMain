using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class TreatmentPlan{
        TreatmentPlan(int patient_Id, int plan_Id, string description, string progress){
            Patient_Id = patient_Id;
            Plan_Id = plan_Id;
            Description = description;
            Progress = progress;
        }

        public int Patient_Id { get; set; }
        public int Plan_Id { get; set; }
        public string Description { get; set; }
        public string Progress { get; set; }
    }
}
