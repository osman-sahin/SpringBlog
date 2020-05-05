using Microsoft.AspNet.Identity;
using SpringBlog.Areas.Admin.ViewModels;
using SpringBlog.Helpers;
using SpringBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpringBlog.Areas.Admin.Controllers
{
    public class PostsController : AdminBaseController
    {
        // GET: Admin/Posts
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }
        public ActionResult New()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(x => x.CategoryName).ToList(), "Id", "CategoryName");
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(NewPostViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    CategoryId = vm.CategoryId,
                    Title = vm.Title,
                    Content = vm.Content,
                    AuthorId = User.Identity.GetUserId(),
                    Slug = UrlService.URLFriendly(vm.Slug),
                    CreationTime = DateTime.Now,
                    ModificationTime = DateTime.Now,
                    FeaturedImagePath = this.SaveImage(vm.FeaturedImage)
                };

                db.Posts.Add(post);
                db.SaveChanges();
                TempData["SuccessMessage"] = "The Post has been added successfuly.";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(x => x.CategoryName).ToList(), "Id", "CategoryName");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = db.Posts.Find(id);

            if (post != null)
            {
                db.Posts.Remove(post);
                db.SaveChanges();
                TempData["SuccessMessage"] = "The Post has been deleted successfuly.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "The Post is not exist or already deleted.";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            ViewBag.PostName = post.Title;
            return View(post);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "The Post has been changed successfuly.";
                return RedirectToAction("Index");
            }

            return View(db.Posts.Find(post.Id));
        }

        [HttpPost]
        public string ConvertToSlug(string title) 
        {
            return UrlService.URLFriendly(title);
        }
    }
}