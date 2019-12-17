using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly_Auth.Models;
using Vidly_Auth.Models.Dto;
using System.Data.Entity;

namespace Vidly_Auth.Controllers.Api
{
    public class ReturnMoviesController : ApiController
    {
        ApplicationDbContext _context;
        IMapper iMapper;


        public ReturnMoviesController()
        {
            _context = new ApplicationDbContext();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ReturnRental, ReturnRentalDto>();
                cfg.CreateMap<Movie, MovieDto>();
            });
            iMapper = config.CreateMapper();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET /api/ReturnMovies/CustomerId
        public IHttpActionResult GetReturnMovies()
        {
            var rentalsForCustomerQuery = _context.Rentals.Include(c => c.Movie);

            return Ok(rentalsForCustomerQuery);

        }


        //GET /api/ReturnMovies/1
        public IHttpActionResult GetReturnMovies(String custName)
        {
            //var rental = _context.Rentals.Include(c => c.Movie).Where(c => c.Id == customerId);
            //if (rental == null)
            //{
            //    return NotFound();
            //}
            //var moviesDto = rental.ToList().Select(c=>c.Movie).Select(iMapper.Map<Movie, MovieDto>);


            Customer cst = _context.Customers.Single(c => c.Name == custName);
            List<int> movieIds = _context.Rentals.Where(c => c.CustomerId == cst.ID && c.DateReturned == null).Select(c => c.MovieId).ToList();
            IEnumerable<MovieDto> moviesDto = _context.Movies.Where(c => movieIds.Contains(c.Id)).Select(iMapper.Map<Movie, MovieDto>); ;
            return Ok(moviesDto);
        }



        [HttpPost]
        public IHttpActionResult ReturnMovie(ReturnRentalDto rental)
        {
            //var customer = _context.Customers.Single(c => c.ID == rental.CustomerId);
            //List<Movie> movies = _context.Movies.Where(c => rental.MovieIds.Contains(c.Id)).ToList();

            int customerId = rental.CustomerId;

            List<int> movieIds = rental.MovieIds;

            foreach (int movieId in movieIds)
            {
                Rental r = _context.Rentals.SingleOrDefault(c => c.CustomerId == rental.CustomerId && c.MovieId == movieId && c.DateReturned == null);
                if (r == null) 
                {
                    return BadRequest("Rental Not found");
                }
                r.DateReturned = DateTime.Now;
                Movie m = _context.Movies.SingleOrDefault(c => c.Id == movieId);
                if (m == null)
                {
                    return BadRequest("Movie Not found");
                }
                m.NumAvailability++;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
