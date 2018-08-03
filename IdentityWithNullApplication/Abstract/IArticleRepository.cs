using IdentityWithNullApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityWithNullApplication.Abstract
{
    public interface IArticleRepository
    {
        IEnumerable<Article> Articles { get; }
        void SaveArticle(Article article);
        Article DeleteArticle(int articleId);
    }
}