using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly_Auth.Models;
using System.Data.Entity;
using Vidly_Auth.ViewModel;

namespace Vidly_Auth.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }
        //To Dispose the Applicaiton Context
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //Add New Customer
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            CustomerFormViewModel customerViewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypes, 
                Customer = new Customer()
            };
            
            return View("CustomerForm", customerViewModel);
        }
        //Adding Create Customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!(ModelState.IsValid))
            {
                var customerViewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes
                };
                return View("CustomerForm", customerViewModel);
            }
            
            if (customer.ID == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                Customer customerInDB = _context.Customers.Single(c => c.ID == customer.ID);
                customerInDB.Name = customer.Name;
                customerInDB.BirthDate = customer.BirthDate;
                customerInDB.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerInDB.MembershipTypeId = customer.MembershipTypeId;
            }
            
            _context.SaveChanges();
            return RedirectToAction("Index","Customer");
        }
        // GET: Customer
        public ActionResult Index()
        {

            var customers = _context.Customers.Include(c=>c.MembershipType);
            return View(customers);
        }
        public ActionResult Edit(int Id)
        {
            var customers = _context.Customers.SingleOrDefault(c => c.ID == Id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            var customerViewModel = new CustomerFormViewModel {
                Customer = customers,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", customerViewModel);
        }
        public ActionResult Details(int Id)
        {
            var customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(n => n.ID == Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }

        }
       
    }
}