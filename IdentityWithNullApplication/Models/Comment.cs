using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityWithNullApplication.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Author { get; set; }
        public string Comm { get; set; }
        public DateTime DateTimeComm { get; set; }

        public int? ArticleId { get; set; }
        public Article Article { get; set; }
    }
}