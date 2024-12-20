using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.DataTransferObject;
using Library.Data.TableDataGateWay;

namespace Library.Domain.CustomerTransactionScripts
{
    public class GetAllCustomersTransactionScript : ITransactionScript<List<CustomerDTO>>
    {
        public List<CustomerDTO> Output { get; private set; }

        public void Execute()
        {
            var customerTDGW = new CustomerTDGW();
            Output = customerTDGW.GetAllCustomers();
        }
    }
}
