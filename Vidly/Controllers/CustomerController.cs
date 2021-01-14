using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            var customers = new List<Customer>
            {
                new Customer { Name ="Amin" , Id=1},
                new Customer { Name="Happy",  Id=2},
                new Customer { Name="Kratos", Id=3}
            };

            return View(customers);
        }

        // GET : Customer/Details/id
        [Route("customer/details/{id}")]
        public ActionResult Details(int id)
        {
            var customers = new List<Customer>
            {
                new Customer { Name ="Amin" , Id=1},
                new Customer { Name="Happy",  Id=2},
                new Customer { Name="Kratos", Id=3}
            };

            Customer customer;

            try
            {
                customer = customers.SingleOrDefault(c => c.Id == id);
                if (customer != null)
                {
                    return View(customer);
                }
            }
            catch (Exception exp)
            {

                Console.WriteLine($"exp occured : {exp}");
            }

            return HttpNotFound();
        }
    }
}