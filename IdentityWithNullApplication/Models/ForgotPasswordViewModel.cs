using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdentityWithNullApplication.Models
{
    public class ForgotPasswordViewModel
    {
       
            [Required]
            [EmailAddress]
            [Display(Name = "Почта")]
            public string Email { get; set; }
        
    }
}