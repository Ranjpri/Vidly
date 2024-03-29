﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Vidly_Auth.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public String Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }
        
        public MembershipType MembershipType { get; set; }

        [Display(Name ="Membership Type")]
        [Required]
        public byte MembershipTypeId { get; set; }

        [Display(Name = "Date Of Birth")]
        [Min18YrsIfMember]        
        public DateTime? BirthDate { get; set; }
    }
}