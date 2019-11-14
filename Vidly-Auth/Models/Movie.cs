using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Auth.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        public DateTime AddedDate { get; set; }

        [Display(Name="Number In Stock")]
        public short NumInStock { get; set; }
        public Genre Genre { get; set; }
        public short GenreId { get; set; }
    }
}