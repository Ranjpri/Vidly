using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Auth.Models.Dto
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
       
        [Required]
        public DateTime? ReleaseDate { get; set; }

        public DateTime? AddedDate { get; set; }

        [Range(1, 20)]
        public short NumInStock { get; set; }

        [Required]        
        public short GenreId { get; set; }
    }
}