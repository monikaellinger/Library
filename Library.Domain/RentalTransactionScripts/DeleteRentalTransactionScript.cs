using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.DataTransferObject;
using Library.Data.TableDataGateWay;

namespace Library.Domain.RentalTransactionScripts
{
    public class DeleteRentalTransactionScript : ITransactionScript<bool>
    {
        public bool Output { get; private set; }
        public int RentalID { get; set; }

        public void Execute()
        {
            var rentalTDG = new RentalTDGW();
            rentalTDG.DeleteRental(RentalID);
            Output = true;
        }
    }
}
