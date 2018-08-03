using IdentityWithNullApplication.Abstract;
using IdentityWithNullApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityWithNullApplication.Controllers
{
    public class ArticleController : Controller
    {
        private IArticleRepository repository;
        public int pageSize = 3;
        ApplicationContext db = new ApplicationContext();

        public ArticleController(IArticleRepository repository)
        {
            this.repository = repository;
        }

        // /Article/ListArticles
        public ViewResult ListArticles(string category, string searchString, int page = 1)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                ArticlesListViewModel model1 = new ArticlesListViewModel
                {
                    Articles = repository.Articles
              .Where(a => a.TitlePost.ToLower().Contains(searchString.ToLower()))
              .OrderByDescending(a => a.Date)
              .Skip((page - 1) * pageSize)
              .Take(pageSize),

                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
      repository.Articles.Count() :
      repository.Articles.Where(article => article.Category == category).Count()
                    },
                    CurrentCategory = category
                };
                return View(model1);

            }

            ArticlesListViewModel model = new ArticlesListViewModel
            {
                Articles = repository.Articles
                .Where(a => category == null || a.Category == category)
                .OrderByDescending(a => a.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
        repository.Articles.Count() :
        repository.Articles.Where(article => article.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }

        // /Article/FullVersionArticle
        public ActionResult FullVersionArticle(int fullId, string slug)
        {
            ArticlesListViewModel article = new ArticlesListViewModel
            {
                Articles = repository.Articles.Where(a => a.ArticleId == fullId)
            };

            if (article != null)
            {
                return View(article);
            }
            else
            {
                return View("Error");
            }


        }

        [HttpGet]
        public ActionResult CommentSearch(int articleId)
        {
            var comments = db.Comments.Where(c => c.ArticleId == articleId).OrderByDescending(c => c.CommentId).ToList();
            return PartialView(comments);
        }

        [HttpPost]
        public ActionResult CommentSearch(Comment comment, int articleId)
        {
            if (comment.Comm != null)
            {
                db.Comments.Add(comment);
                db.SaveChanges();

            }
            var comments = db.Comments.Where(c => c.ArticleId == articleId).OrderByDescending(c => c.CommentId).ToList();

            return PartialView(comments);
        }

        public FileContentResult GetImage(int articleId)
        {
            Article articles = repository.Articles
                .FirstOrDefault(a => a.ArticleId == articleId);

            if (articles != null)
            {
                return File(articles.ImageData, articles.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}