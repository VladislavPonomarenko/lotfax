using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityWithNullApplication.Models
{
    public class Article
    {
        [HiddenInput(DisplayValue = false)]
        public int ArticleId { get; set; }

        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Будь-ласка, введіть заголовок")]
        public string TitlePost { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Новина")]
        [Required(ErrorMessage = "Будь-ласка, введіть новину")]
        public string Message { get; set; }

        [Display(Name = "Автор")]
        [Required(ErrorMessage = "Будь-ласка, введіть автора")]
        public string Author { get; set; }

        [Display(Name = "Категорія")]
        [Required(ErrorMessage = "Будь-ласка, введіть категорію")]
        public string Category { get; set; }

        [Display(Name = "Дата публікації")]
        [HiddenInput(DisplayValue = false)]
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public Article()
        {
            Comments = new List<Comment>();
        }
    }
}