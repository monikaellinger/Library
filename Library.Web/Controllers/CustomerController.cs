using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Web.Models;
using Library.Domain.CustomerTransactionScripts;
using Library.Data.DataTransferObject;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Library.Web.Controllers
{
    public class CustomerController : Controller
    {
        // GET: CustomerController
        [Authorize]
        public ActionResult Index()
        {
            var script = new GetAllCustomersTransactionScript();
            script.Execute();
            var customers = script.Output;
            List<CustomerModel> result = new List<CustomerModel>();
            foreach (var customer in customers)
            {
                result.Add(new CustomerModel
                {
                    CustomerID = customer.CustomerID,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email
                });
            }

            return View(result);
        }

        // GET: CustomerController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            var script = new GetCustomerTransactionScript();
            script.CustomerID = id;
            script.Execute();
            CustomerModel customer = new CustomerModel
            {
                CustomerID = script.Output.CustomerID,
                FirstName = script.Output.FirstName,
                LastName = script.Output.LastName,
                Email = script.Output.Email
            };
            return View(customer);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
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

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
