using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.TableDataGateWay;
using Library.Data.DataTransferObject;

namespace Library.Domain.StaffTransactionScripts
{
    public class GetAllStaffTransactionScript : ITransactionScript<List<StaffDTO>>
    {
        public List<StaffDTO> Output { get; private set; }

        public void Execute()
        {
            var staffTDGW = new StaffTDGW();
            Output = staffTDGW.GetAllStaff();
        }
    }
}
