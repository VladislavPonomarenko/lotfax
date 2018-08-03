using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityWithNullApplication.Models
{
    public class ArticlesListViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}