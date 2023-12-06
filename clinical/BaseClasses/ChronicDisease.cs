using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class ChronicDisease
    {
        public int ChronicDiseaseID { get; set; }
        public string ChronicDiseaseName { get; set; }
        public string Description { get; set; }
        public List<int> AllergicToDrugsIDs { get; set; }

        public ChronicDisease(int chronicDiseaseID, string chronicDiseaseName, string description, List<int> allergicToDrugs)
        {
            ChronicDiseaseID = chronicDiseaseID;
            ChronicDiseaseName = chronicDiseaseName;
            Description = description;
            AllergicToDrugsIDs = allergicToDrugs;
        }
    }
}
