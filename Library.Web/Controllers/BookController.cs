using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.BookTransactionSctipts;
using Library.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        // GET: BookController
        [Authorize]
        public ActionResult Index()
        {
            var script = new GetAllBooksTransactionScript();
            script.Execute();
            var books = script.Output;
            var bookModels = new List<BookModel>();
            foreach (var book in books)
            {
                bookModels.Add(new BookModel
                {
                    BookID = book.BookID,
                    Title = book.Title,
                    Author = book.Author,
                    Year = book.Year,
                    Pages = book.Pages,
                    CurrentlyRented = book.CurrentlyRented
                });
            }

            return View(bookModels);
        }

        // GET: BookController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(IFormCollection collection)
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

        // GET: BookController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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

        // GET: BookController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
