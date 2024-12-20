using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.DataTransferObject;
using Library.Data.TableDataGateWay;

namespace Library.Domain.RentalTransactionScripts
{
    public class ReturnRentalTransactionScript : ITransactionScript<bool>
    {
        public int RentalID { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool Output { get; private set; }

        public void Execute()
        {
            var rentalTDGW = new RentalTDGW();
            rentalTDGW.UpdateReturnedDate(RentalID, ReturnDate);
            Output = true;
        }
    }
    
}
