using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical.BaseClasses
{
    class Admin
    {
        public Admin(int userID, string userName, string password, int role)
        {
            UserID = userID;
            UserName = userName;
            Password = password;
            Role = role;
        }

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role {get; set; }
    }
}
