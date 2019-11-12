using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly_Auth.Models;
using Vidly_Auth.ViewModel;

namespace Vidly_Auth.Controllers
{
    public class MoviesController : Controller
    {
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
        public ActionResult Edit(int id)
        {
            return Content("id = " + id);
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
            var movies = GetMovies();
            return View(movies);
        }

        public List<Movie> GetMovies()
        {

            List<Movie> movies = new List<Movie>
            {
                new Movie{ Id = 1, Name = "Shrek"},
                new Movie{ Id = 2, Name = "Madagascar"}
            };
            return movies;
        }
    }
}