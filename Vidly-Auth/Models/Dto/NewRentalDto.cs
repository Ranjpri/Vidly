﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly_Auth.Models.Dto
{
    public class NewRentalDto
    {
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}