using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityWithNullApplication.Models
{
    public class EditModel
    {
        public int Year { get; set; }
        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}