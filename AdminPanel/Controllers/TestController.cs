using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminPanel.Models;
using System.IO;

namespace AdminPanel.Controllers
{
    public class TestController : Controller
    {
        private GraduationProjectContext db = new GraduationProjectContext();
        

        public ActionResult ImageUpload()
        {
           

            return View();
        }


        [HttpPost]
        public ActionResult ImageUpload(HttpPostedFileBase uploadfile)
        {
            if (uploadfile.ContentLength > 0)
            {
                string filePath = Path.Combine(Server.MapPath("~/Images"), Guid.NewGuid().ToString() + "_" + Path.GetFileName(uploadfile.FileName));
                uploadfile.SaveAs(filePath);
            }

            return View();
        }

        // GET: Test
        public ActionResult Index()
        {
            var foods = db.Foods.Include(f => f.Category).Include(f => f.Restaurant);
            return View(foods.ToList());
        }

        // GET: Test/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name");
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,CategoryId,PicturePath,RestaurantId")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Foods.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", food.CategoryId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", food.RestaurantId);
            return View(food);
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", food.CategoryId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", food.RestaurantId);
            return View(food);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,CategoryId,PicturePath,RestaurantId")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", food.CategoryId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", food.RestaurantId);
            return View(food);
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
