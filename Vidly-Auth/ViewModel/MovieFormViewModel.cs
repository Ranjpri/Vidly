using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly_Auth.Models;
namespace Vidly_Auth.ViewModel
{
    public class MovieFormViewModel
    {
        public Movie Movie { get; set; }
        public List<Genre> GenreDetails { get; set; }
    }
}