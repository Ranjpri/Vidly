using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly_Auth.Models;
using System.Data.Entity;

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

        // GET: Customer
        public ActionResult Index()
        {

            var customers = _context.Customers.Include(c=>c.MembershipType);
            return View(customers);
        }
        public ActionResult Details(int Id)
        {
            var customer = _context.Customers.SingleOrDefault(n => n.ID == Id);
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