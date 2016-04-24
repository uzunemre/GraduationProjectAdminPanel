using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminPanel.Models;

namespace AdminPanel.Controllers
{
    public class CategoriesController : Controller
    {
        private GraduationProjectContext db = new GraduationProjectContext();

        private int getSessionInfo()
        {
            string test = Session["UserID"].ToString();
            int x = Int32.Parse(test);
            return x;
        }

        // GET: Categories
        public ActionResult Index()
        {
            int a = getSessionInfo();
            var categories = db.Categories.Include(c => c.Restaurant).Where(c => c.RestaurantId == a);
            return View(categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
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
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
           // ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PicturePath")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.RestaurantId = getSessionInfo();
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(category);
        }

        // GET: Categories/Edit/5
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
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", category.RestaurantId);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PicturePath")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.RestaurantId = getSessionInfo();
                db.Categories.Add(category);
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
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
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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


        public ActionResult UrunListele(int id)
        {
            // Kullanıcı sekmeden kafasına göre id girerse başka restorana ait engelle
            int a = getSessionInfo();
            var category_id_for_restaurant = from category in db.Categories
                                             where category.RestaurantId == a
                                             select category.Id;

            foreach (int cat_id in category_id_for_restaurant)
            {
                if(cat_id == id)
                {
                    var food = db.Foods.Where(x => x.CategoryId == id).ToList();
                    return View("ListFood", food);
                }
            }


            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);



        }


    }
}
