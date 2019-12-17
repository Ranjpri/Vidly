using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly_Auth.Models
{
    public class ReturnRental
    {
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}