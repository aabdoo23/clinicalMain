using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class Equipment
    {
        public int EquipmentID { get; set; }
        public string EquipmentName { get; set; }
        public string Function { get; set; }
        public DateTime LatestMaintenanceDate { get; set; }
        public bool ToCheck { get; set; }

        // Constructor
        public Equipment(int equipmentID, string equipmentName, string function, DateTime latestMaintenanceDate, bool toCheck)
        {
            EquipmentID = equipmentID;
            EquipmentName = equipmentName;
            Function = function;
            LatestMaintenanceDate = latestMaintenanceDate;
            ToCheck = toCheck;
        }
    }
}
