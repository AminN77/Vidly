using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private readonly VidlyDataBaseContext _context;

        public CustomerController()
        {
            _context = new VidlyDataBaseContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customer
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        // GET : Customer/Details/id
        [Route("customer/details/{id}")]
        public ActionResult Details(int id)
        {
           
            Customer customer;

            try
            {
                customer = _context.Customers.Include(c=> c.MembershipType).SingleOrDefault(c => c.Id == id);
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

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel); 
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var exCustomer = _context.Customers.Single(c => c.Id == customer.Id);

                exCustomer.Name = customer.Name;
                exCustomer.BirthDate= customer.BirthDate;
                exCustomer.MembershipTypeId = customer.MembershipTypeId;
                exCustomer.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}