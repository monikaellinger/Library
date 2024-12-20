namespace Library.Web.Models
{
    public class RentalModel
    {
        public int Id { get; set; }
        public BookModel Book { get; set; } = new BookModel();
        public CustomerModel Customer { get; set; } = new CustomerModel();
        public DateTime? RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public RentalModel() { }

        public RentalModel(int id, BookModel book, CustomerModel customer, DateTime? rentalDate, DateTime? returnDate)
        {
            Id = id;
            Book = book;
            Customer = customer;
            RentalDate = rentalDate;
            ReturnDate = returnDate;
        }
    }
}
