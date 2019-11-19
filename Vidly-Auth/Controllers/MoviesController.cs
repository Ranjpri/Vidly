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
            var movie = _context.Movies.Include(c => c.Genre).Single(n => n.Id == Id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                var movieViewModel = new MovieFormViewModel(movie)
                {                    
                    GenreDetails = _context.Genres.ToList()
                };
                return View("MovieForm", movieViewModel);
            }
        }
        public ActionResult New()
        {
            var genres = _context.Genres;
            var movieFormViewModel = new MovieFormViewModel()
            {
                GenreDetails = genres.ToList(),
            };
            return View("MovieForm", movieFormViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                MovieFormViewModel mvModel = new MovieFormViewModel(movie)
                {
                    GenreDetails = _context.Genres.ToList()
                };
                return View("MovieForm", mvModel);
            }
            if (movie.Id == 0)
            {
                //New Movie
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDB = _context.Movies.Single(c=>c.Id == movie.Id);
                movieInDB.Name = movie.Name;
                movieInDB.ReleaseDate = movie.ReleaseDate;
                movieInDB.GenreId = movie.GenreId;
                movieInDB.NumInStock = movie.NumInStock;
            }
           
           _context.SaveChanges();           
            return RedirectToAction("Index", "Movies");
        }
       
        

        public ActionResult Index()
        {
            var movies = _context.Movies.Include(c=>c.Genre);
            return View(movies);
        }

        
    }
}