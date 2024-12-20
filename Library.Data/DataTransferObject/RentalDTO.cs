using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.DataTransferObject
{
    public class RentalDTO
    {
        public int RentalID { get; set; }
        public int BookID { get; set; }
        public int CustomerID { get; set; }
        public DateTime? DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}
