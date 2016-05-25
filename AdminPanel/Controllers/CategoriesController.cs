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
                return RedirectToAction("KategoriListele", "Home");
            }

            
            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            int restaurant_id = getSessionInfo();

            var id_categories = from cat in db.Categories
                                where cat.RestaurantId == restaurant_id
                                select cat.Id;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            foreach(int category_id in id_categories)
            {
                if(category_id == id)
                {
                    Category category = db.Categories.Find(id);
                    if (category == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                  //  ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", category.RestaurantId);
                    return View(category);
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


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
                return RedirectToAction("KategoriListele","Home");
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
            //Delete View ismi post işlemi onu belirtiyor
            // Kategoriye ait ürünleri buldum teker teker silinecek
            var products_of_category = from product in db.Foods
                                       where product.CategoryId == id
                                       select product.Id;

            foreach (int product_id in products_of_category)
            {
                Food food = db.Foods.Find(product_id);
                db.Foods.Remove(food);
            }

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
