using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class Drug
    {
        int DrugID { set; get; }    
        String Name {  set; get; }
        int Frequency {set; get; }
        Drug(int drugID, string name, int frequency)
        {
            DrugID = drugID;
            Name = name;
            Frequency = frequency;
        }   
    }
}
