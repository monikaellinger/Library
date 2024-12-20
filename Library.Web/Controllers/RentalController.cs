using Library.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.RentalTransactionScripts;
using Library.Domain.CustomerTransactionScripts;
using Library.Domain.BookTransactionSctipts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Library.Web.Controllers
{
    public class RentalController : Controller
    {
        // GET: RentalController
        public ActionResult Index()
        {
            Dictionary<int, CustomerModel> customers = new Dictionary<int, CustomerModel>();
            Dictionary<int, BookModel> books = new Dictionary<int, BookModel>();
            var rentalsScript = new GetAllRentalsTransactionScript();
            rentalsScript.Execute();
            List<RentalModel> rentals = new List<RentalModel>();

            foreach (var rental in rentalsScript.Output)
            {
                if (!customers.ContainsKey(rental.CustomerID))
                {   
                    var customerScript = new GetCustomerTransactionScript();
                    customerScript.CustomerID = rental.CustomerID;
                    customerScript.Execute();
                    customers.Add(rental.CustomerID, new CustomerModel(customerScript.Output.CustomerID, customerScript.Output.FirstName, customerScript.Output.LastName, customerScript.Output.Email));
                }
                if (!books.ContainsKey(rental.BookID))
                {
                    var bookScript = new GetBookTransactionScript();
                    bookScript.BookID = rental.BookID;
                    bookScript.Execute();
                    books.Add(rental.BookID, new BookModel(bookScript.Output.BookID, bookScript.Output.Title, bookScript.Output.Author, bookScript.Output.Year, bookScript.Output.Pages));
                }
                rentals.Add(new RentalModel(rental.RentalID, books[rental.BookID], customers[rental.CustomerID], rental.DateRented, rental.DateReturned));

            }
            return View(rentals);
        }

        // GET: RentalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RentalController/Create
        public ActionResult Create()
        {
            var customersScript = new GetAllCustomersTransactionScript();
            customersScript.Execute();
            var booksScript = new GetAllBooksTransactionScript();
            booksScript.Execute();
            List<CustomerModel> customers = new List<CustomerModel>();
            List<BookModel> books = new List<BookModel>();
            foreach (var customer in customersScript.Output)
            {
                customers.Add(new CustomerModel(customer.CustomerID, customer.FirstName, customer.LastName, customer.Email));
            }
            foreach (var book in booksScript.Output)
            {
                if (!book.CurrentlyRented)
                    books.Add(new BookModel(book.BookID, book.Title, book.Author, book.Year, book.Pages));
            }

            ViewBag.Customers = new SelectList(customers, "CustomerID", "FullName");
            ViewBag.Books = new SelectList(books, "BookID", "Title");
            return View();
        }

        // POST: RentalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var rental = new RentalModel();
                rental.Book.BookID = int.Parse(collection["Book"]);
                rental.Customer.CustomerID = int.Parse(collection["Customer"]);
                rental.RentalDate = DateTime.Parse(collection["RentalDate"]);
                //rental.ReturnDate = null;
                var rentalScript = new CreateRentalTransactionScript();
                rentalScript.BookID = rental.Book.BookID;
                rentalScript.CustomerID = rental.Customer.CustomerID;
                rentalScript.RentalDate = rental.RentalDate;
                //rentalScript.ReturnDate = null;
                rentalScript.Execute();
                TempData["Message"] = "Výpůjčka byla úspěšně uložena";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RentalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RentalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RentalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RentalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var rentalScript = new DeleteRentalTransactionScript();
                rentalScript.RentalID = id;
                rentalScript.Execute();
                TempData["Message"] = "Výpůjčka byla úspěšně smazána";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST RentalController/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(int id)
        {
            try
            {
                var rentalScript = new ReturnRentalTransactionScript();
                rentalScript.RentalID = id;
                rentalScript.ReturnDate = DateTime.Now;
                rentalScript.Execute();
                TempData["Message"] = "Výpůjčka byla úspěšně vrácena";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Message"] = "Výpůjčku se nepodařilo vrátit";
                TempData["MessageType"] = "danger";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: RentalControrller/Export
        public ActionResult Export()
        {
            var rentalsScript = new GetAllRentalsTransactionScript();
            rentalsScript.Execute();
            var csv = new StringBuilder();
            csv.AppendLine("RentalID,BookID,CustomerID,DateRented,DateReturned");
            foreach (var rental in rentalsScript.Output)
            {
                var newLine = string.Format("{0};{1};{2};{3};{4}", rental.RentalID, rental.BookID, rental.CustomerID, rental.DateRented, rental.DateReturned);
                csv.AppendLine(newLine);
            }
            return File(new System.Text.UTF8Encoding().GetBytes(csv.ToString()), "text/csv", "rentals.csv");
        }
    }
}
