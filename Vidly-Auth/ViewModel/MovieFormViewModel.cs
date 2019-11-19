using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly_Auth.Models;
namespace Vidly_Auth.ViewModel
{
    public class MovieFormViewModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }


        [Display(Name = "Number In Stock")]
        [Range(1, 20)]
        public short? NumInStock { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public short? GenreId { get; set; }

        public List<Genre> GenreDetails { get; set; }

        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumInStock = movie.NumInStock;
            GenreId = movie.GenreId;

        }

        public String Title { get
            {
               return (Id == 0) ? "New" : "Edit";
            }            
        }
    }
}