using IdentityWithNullApplication.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityWithNullApplication.Controllers
{
    public class NavController : Controller
    {

        private IArticleRepository repository;

        public NavController(IArticleRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult NavMenu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Articles
                .Select(a => a.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);
        }

        public PartialViewResult Menu()
        {
           return PartialView();
        }

        public PartialViewResult Contact()
        {
            return PartialView();
        }
    }
}