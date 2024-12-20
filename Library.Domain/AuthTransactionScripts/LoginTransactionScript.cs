using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.DataTransferObject;
using Library.Data.TableDataGateWay;

namespace Library.Domain.AuthTransactionScripts
{
    public class LoginTransactionScript : ITransactionScript<bool>
    {
        public bool Output { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public StaffDTO Staff { get; private set; }
        public void Execute()
        {
            StaffTDGW staffTDGW = new StaffTDGW();
            StaffDTO staff = staffTDGW.GetStaffByUsername(Username);
            if (staff.Username == Username && staff.Password == Password)
            {
                Output = true;
                Staff = staff;
            }

            else
                Output = false;
        }
    }
}
