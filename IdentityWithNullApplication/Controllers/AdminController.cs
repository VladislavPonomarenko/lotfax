using IdentityWithNullApplication.Abstract;
using IdentityWithNullApplication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;

namespace IdentityWithNullApplication.Controllers
{
    [Authorize(Roles = "admin, moderator")]
    public class AdminController : Controller
    {

        IArticleRepository repository;
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public AdminController(IArticleRepository repository)
        {
            this.repository = repository;

        }

        // GET: /Admin/Index
        public ViewResult Index()
        {
          
            return View(repository.Articles);
        }

        // GET: /Admin/Edit
        [HttpGet]
        public ViewResult Edit(int articleId)
        {
            Article article = repository.Articles
                .FirstOrDefault(a => a.ArticleId == articleId);
            return View(article);
        }

        // POST: /Admin/Edit
        [HttpPost]
        public ActionResult Edit(Article article, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    article.ImageMimeType = image.ContentType;
                    article.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(article.ImageData, 0, image.ContentLength);
                }
                repository.SaveArticle(article);
                TempData["message"] = string.Format("Изменения в статьи \"{0}\" были сохранены", article.TitlePost);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(article);
            }
        }

        // GET: /Admin/Create
        public ViewResult Create()
        {
            return View("Edit", new Article());
        }

        // POST: /Admin/Delete
        [HttpPost]
        public ActionResult Delete(int articleId)
        {
            Article deleteArticle = repository.DeleteArticle(articleId);
            if (deleteArticle != null)
            {
                TempData["message"] = string.Format("Стаття \"{0}\" успешно удалена",
                    deleteArticle.TitlePost);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles ="admin")]
        // GET: /Admin/ViewUsers
        public ViewResult ViewUsers(string roleName)
        {
            using (var context = new ApplicationContext())
            {

                if (roleName != "all")
                {
                    var emp = (from role in context.Roles
                               where role.Name == roleName
                               from userRoles in role.Users
                               join user in context.Users
                               on userRoles.UserId equals user.Id
                               select user).ToList();
                    return View(emp);
                }
                else
                {
                    var emp = (from role in context.Roles
                               from userRoles in role.Users
                               join user in context.Users
                               on userRoles.UserId equals user.Id
                               select user).ToList();
                    return View(emp);
                }
            }
        }


        // GET: /Admin/EditUser
        [HttpGet]
        public async Task<ActionResult> EditUser(string userId)
        {
            ApplicationUser applicationUser = new ApplicationUser();

            var user = await UserManager.FindByIdAsync(userId);
            return View(user);
        }

        // POST: /Admin/EditUser
        [HttpPost]
        public async Task<ActionResult> EditUser(string userId,string email, string role)
        {
            ApplicationUser user = new ApplicationUser ();
            if (userId != null && role != null)
            {
                if (role.Equals("moderator"))
                {
                    await UserManager.RemoveFromRoleAsync(userId, "user");
                    await UserManager.RemoveFromRoleAsync(userId, "admin");
                    await UserManager.AddToRoleAsync(userId, role);
                }
                else if (role.Equals("user"))
                {
                    await UserManager.RemoveFromRoleAsync(userId, "moderator");
                    await UserManager.RemoveFromRoleAsync(userId, "admin");
                    await UserManager.AddToRoleAsync(userId, role);
                }
                else if (role.Equals("admin")) {
                    await UserManager.RemoveFromRoleAsync(userId, "moderator");
                    await UserManager.RemoveFromRoleAsync(userId, "user");
                    await UserManager.AddToRoleAsync(userId, role);
                }

                TempData["message"] = string.Format("Изменения пользователя \"{0}\" были сохранены", email);
                return RedirectToAction("ViewUsers");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(user);
            }
        }


        [Authorize(Roles = "admin")]
        // POST: /Admin/DeleteUser
        [HttpPost]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            if (userId != null)
            {
                var user = await UserManager.FindByIdAsync(userId);
                var deleteUser = await UserManager.DeleteAsync(user);
                if (deleteUser.Succeeded)
                    TempData["message"] = string.Format("Пользователь \"{0}\" успешно удален",
                        user.Email);
            }
            return RedirectToAction("ViewUsers");
        }
    }
}