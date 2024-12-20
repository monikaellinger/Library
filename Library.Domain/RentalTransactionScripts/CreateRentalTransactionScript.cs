using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.DataTransferObject;
using Library.Data.TableDataGateWay;

namespace Library.Domain.RentalTransactionScripts
{
    public class CreateRentalTransactionScript : ITransactionScript<bool>
    {
        public int BookID { get; set; }
        public int CustomerID { get; set; }
        public DateTime? RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool Output { get; private set; }

        public void Execute()
        {
            var rental = new RentalDTO
            {
                BookID = BookID,
                CustomerID = CustomerID,
                DateRented = RentalDate,
                DateReturned = ReturnDate
            };
            var rentalTDGW = new RentalTDGW();
            rentalTDGW.InsertRental(rental);

            var bookTDGW = new BookTDGW();
            var book = bookTDGW.GetBook(BookID);
            book.CurrentlyRented = true;
            bookTDGW.UpdateBook(book);


            Output = true;
        }
    }
}
