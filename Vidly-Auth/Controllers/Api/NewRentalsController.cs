using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly_Auth.Models;
using Vidly_Auth.Models.Dto;

namespace Vidly_Auth.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto rental)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.ID == rental.CustomerId);
            if (customer == null)
            {
                return BadRequest("Customer not Found");
            }
            if (rental.MovieIds.Count == 0)
            {
                return BadRequest("No Movies were added");
            }
            List<Movie> movies = _context.Movies.Where(c => rental.MovieIds.Contains(c.Id)).ToList();
            if (movies.Count != rental.MovieIds.Count)
            {
                return BadRequest("Movie Ids not found");
            }
            foreach (Movie movie in movies)
            {
                if (movie.NumAvailability == 0)
                {
                    return BadRequest("Movie is out of stock");
                }

                Rental rentalDbo = new Rental();
                rentalDbo.Customer = customer;
                rentalDbo.Movie = movie;
                rentalDbo.DateRented = DateTime.Now;
                _context.Rentals.Add(rentalDbo);

                //Calculating the movie availability
                movie.NumAvailability--;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
