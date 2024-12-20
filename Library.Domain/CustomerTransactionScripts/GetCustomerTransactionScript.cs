using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.DataTransferObject;
using Library.Data.TableDataGateWay;

namespace Library.Domain.CustomerTransactionScripts
{
    public class GetCustomerTransactionScript : ITransactionScript<CustomerDTO>
    {
        public CustomerDTO Output { get; private set; }
        public int CustomerID { get; set; }

        public void Execute()
        {
            var customerTDGW = new CustomerTDGW();
            Output = customerTDGW.GetCustomer(CustomerID);
        }
    }
}
