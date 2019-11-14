using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly_Auth.Models;
using Vidly_Auth.ViewModel;
using System.Data.Entity;

namespace Vidly_Auth.Controllers
{
    public class MoviesController : Controller
    {
        ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shawshank" };
            List<Customer> c = new List<Customer>
            {
                new Customer{ Name = "Ravi"},
                new Customer{ Name = "Pete"}
            };
            var viewModel = new RandomMovieViewModel();
            viewModel.Movie = movie;
            viewModel.Customers = c;

            return View(viewModel);
        }
        public ActionResult Edit(int Id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(n => n.Id == Id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(movie);
            }
        }
        public ActionResult Save(Movie movie)
        {          
           _context.Movies.Add(movie);
           _context.SaveChanges();
           
            return RedirectToAction("Index", "Movies");
        }
        public ActionResult New()
        {
            var genres = _context.Genres;
            var movieFormViewModel = new MovieFormViewModel()
            {
                GenreDetails = genres.ToList(),
                Movie = new Movie()
                
            };
            return View(movieFormViewModel);

        }
        //public ActionResult Index(int? pageIndex, string sortField)
        //{
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 2;
        //    }
        //    if (String.IsNullOrEmpty(sortField))
        //    {
        //        sortField = "Test Name";
        //    }
        //    return Content(String.Format("Here {0} {1}", pageIndex, sortField));
        //}
        //[Route("movies/released/{year}/{month:regex(\\d{2}):range(1,3)}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content("Year = " + year + "& Month = " + month);
        //}

        public ActionResult Index()
        {
            var movies = _context.Movies.Include(c=>c.Genre);
            return View(movies);
        }

        
    }
}