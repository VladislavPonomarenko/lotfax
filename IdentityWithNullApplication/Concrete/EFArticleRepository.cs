using IdentityWithNullApplication.Abstract;
using IdentityWithNullApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityWithNullApplication.Concrete
{
    public class EFArticleRepository : IArticleRepository
    {
        ApplicationContext context = new ApplicationContext();
        public IEnumerable<Article> Articles
        {
            get { return context.Articles; }
        }

        public Article DeleteArticle(int articleId)
        {
            Article dbEntry = context.Articles.Find(articleId);
            if (dbEntry != null)
            {
                context.Articles.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }



        public void SaveArticle(Article article)
        {
            if (article.ArticleId == 0)
            {
                article.Date = DateTime.Now;
                context.Articles.Add(article);
            }
            else
            {
                Article dbEntry = context.Articles.Find(article.ArticleId);
                if (dbEntry != null)
                {
                    dbEntry.TitlePost = article.TitlePost;
                    dbEntry.Message = article.Message;
                    dbEntry.Author = article.Author;
                    dbEntry.Category = article.Category;
                    dbEntry.Date = DateTime.Now;
                    dbEntry.ImageData = article.ImageData;
                    dbEntry.ImageMimeType = article.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
    }
}