using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    public class Package
    {
        public int PackageID {  get; set; }    
        public string PackageName { get; set; }
        public int NumberOfSessions {  get; set; }
        public double Price {  get; set; }
        public string Description { get; set; }
        public Package(int packageID, string packageName, int numberOfSessions, double price, string description)
        {
            PackageID = packageID;
            PackageName = packageName;
            NumberOfSessions = numberOfSessions;
            Price = price;
            Description = description;
        }

        public override string ToString()
        {
            return $"{PackageName}, {NumberOfSessions} sessions, {Price}";
        }
    }
}
