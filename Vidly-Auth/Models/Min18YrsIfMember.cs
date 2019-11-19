using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Auth.Models
{
    public class Min18YrsIfMember:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Customer customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.Unknown||customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }
            if (customer.BirthDate != null)
            {
                int age = DateTime.Today.Year - customer.BirthDate.Value.Year;
                if (age >= 18)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Customer needs to be 18 or more for this subscription");
                }
            }
            else
            {
                return new ValidationResult("Birthdate is a mandatory field");
            }

        }
    }
}