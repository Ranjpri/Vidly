using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Vidly_Auth.Models;
using AutoMapper;
using Vidly_Auth.Models.Dto;
using System;
using System.Data.Entity;

namespace Vidly_Auth.Controllers.Api
{
    public class CustomersController : ApiController
    {
        ApplicationDbContext _context;
        IMapper iMapper;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Customer, CustomerDto>();
                cfg.CreateMap<CustomerDto, Customer>();
                cfg.CreateMap<MembershipType, MembershipTypeDto>();

            });
            iMapper = config.CreateMapper();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        //GET /api/Customers
        public IHttpActionResult GetCustomers(String query=null)
        {
            var customersQuery = _context.Customers.Include(c => c.MembershipType);
            if (!String.IsNullOrEmpty(query))
            {
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));
            }
             var customersDto = customersQuery.ToList()
                .Select(iMapper.Map<Customer, CustomerDto>);

            return Ok(customersDto);
        }

        //Get /api/Customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.ID == id);
            if (customer == null)
                return NotFound();
            CustomerDto customerDto = iMapper.Map<Customer, CustomerDto>(customer);
            return Ok(customerDto);
        }

        //POST /api/Customers
        [HttpPost]
        public IHttpActionResult  CreateCustomer(CustomerDto customerDto)
        {
            Customer customer = iMapper.Map<CustomerDto, Customer>(customerDto);
            if (!ModelState.IsValid)
                return BadRequest();
            
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.ID = customer.ID;
            return Created(new Uri(Request.RequestUri+"/"+customerDto.ID),customerDto);
        }

        //PUT /api/Customer/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var customerInDb = _context.Customers.SingleOrDefault(c => c.ID == id);
            if (customerInDb == null)
                return NotFound();
            iMapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
           
            _context.SaveChanges();
            return Ok(customerDto);
        }

        //Delete /api/Customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.ID == id);
            if (customer == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return Ok();
        }
    }

}
