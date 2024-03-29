﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly_Auth.Models;

namespace Vidly_Auth.ViewModel
{
    public class CustomerFormViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
    }
}