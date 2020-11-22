﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Shop___DAW.Models;

namespace Online_Shop___DAW.Controllers
{
    public class CategoriesController : Controller
    {
        private Models.AppContext db = new Models.AppContext();
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(int id)
        {
            var category = db.Categories
                .Include("Products")
                .FirstOrDefault(p => p.CategoryId == id);
            ViewBag.Category = category;
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Category category)
        {
            try
            {
                category.CreatedAt = DateTime.UtcNow;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("Error");
            }
        }

        public ActionResult Edit(int id)
        {
            var category = db.Categories
                .Include("Products")
                .FirstOrDefault(p => p.CategoryId == id);
            ViewBag.Category = category;
            return View();
        }

        [HttpPut]
        public ActionResult Edit(int id, Category requestCategory)
        {
            try
            {
                Category category = db.Categories.Find(id);
                if (TryUpdateModel(category))
                {
                    category.Name = requestCategory.Name;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("Error");
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}