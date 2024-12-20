using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.DataTransferObject;
using Library.Data.TableDataGateWay;

namespace Library.Domain.RentalTransactionScripts
{
    public class GetAllRentalsTransactionScript : ITransactionScript<List<RentalDTO>>
    {
        public List<RentalDTO> Output { get; private set; }
        public void Execute()
        {
            var rentalTableDataGateway = new RentalTDGW();
            Output = rentalTableDataGateway.GetAllRentals();
        }
    }
}
