using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Vidly_Auth.Models;

namespace Vidly_Auth.Controllers.Api
{
    public class CustomersController : ApiController
    {
        ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        //GET /api/Customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers;
        }

        //Get /api/Customers/1
        public Customer GetCustomer(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.ID == id);
            if (customer == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            return customer;
        }

        //POST /api/Customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        //PUT /api/Customer/1
        [HttpPut]
        public Customer UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            var customerInDb = _context.Customers.SingleOrDefault(c => c.ID == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
            customerInDb.Name = customer.Name;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.BirthDate = customer.BirthDate;

            _context.SaveChanges();
            return customer;
        }

        //Delete /api/Customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.ID == id);
            if (customer == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }

}
