﻿using SpringBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpringBlog.Areas.Admin.Controllers
{
    public class CategoriesController : AdminBaseController
    {
        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult New()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                TempData["SuccessMessage"] = "The Category has been added successfuly.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryName = category.CategoryName;
            return View(category);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "The Category has been changed successfuly.";
                return RedirectToAction("Index");
            }

            return View(db.Categories.Find(category.Id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);

            if (category.Posts.Count > 0)
            {
                TempData["ErrorMessage"] = "A Category must contains no post in order to be deleted";
                return RedirectToAction("Index");
            }

            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
                TempData["SuccessMessage"] = "The Category has been deleted successfuly.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "The Category is not exist or already deleted.";
            return RedirectToAction("Index");
        }

    }
}