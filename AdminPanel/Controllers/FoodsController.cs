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
    public class FoodsController : Controller
    {
        private GraduationProjectContext db = new GraduationProjectContext();
        

        private int getSessionInfo()
        {
            string test = Session["UserID"].ToString();
            int x = Int32.Parse(test);
            return x;
        }

        // GET: Foods
        public ActionResult Index()
        {
            var foods = db.Foods.Include(f => f.Category).Include(f => f.Restaurant);
            return View(foods.ToList());
        }

        // GET: Foods/Details/5
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

        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }
            // restauranta ait kategorileri çektim CiewBag ile yolladım
            int restaurant_id = getSessionInfo();
            var restaurant_categories = db.Categories.Where(x => x.RestaurantId == restaurant_id);
            ViewBag.CategoryId = new SelectList(restaurant_categories, "Id", "Name");
            //    ViewBag.RestaurantId = new SelectList(context.Restaurants, "Id", "Name");
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,CategoryId")] Food food, HttpPostedFileBase uploadfile)
        {
            if (ModelState.IsValid)
            {
                
                string folder_name = "Images";
                string file_name = Guid.NewGuid().ToString() + "_" + Path.GetFileName(uploadfile.FileName);

                if (uploadfile.ContentLength > 0)
                {
                    string file_path = Server.MapPath("~/" + folder_name + "/" + file_name);
                    //filePath = Path.Combine(Server.MapPath("~/Images"), Guid.NewGuid().ToString() + "_" + Path.GetFileName(uploadfile.FileName));
                    uploadfile.SaveAs(file_path);
                }

                string database_path = folder_name + "/" + file_name;

                int restaurant_id = getSessionInfo();
                food.RestaurantId = restaurant_id;
                food.PicturePath = database_path;
                db.Foods.Add(food);
                db.SaveChanges();
                return RedirectToAction("KategoriListele","Home");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", food.CategoryId);
            //  ViewBag.RestaurantId = new SelectList(context.Restaurants, "Id", "Name", food.RestaurantId);
            return View(food);
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int? id)
        {

            int a = getSessionInfo();
            var product_id_for_restaurant = from product in db.Foods
                                            where product.RestaurantId == a
                                            select product.Id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            foreach (int product_id in product_id_for_restaurant)
            {
                if (product_id == id)
                {
                    Food food = db.Foods.Find(id);
                    Session["category_id"] = food.CategoryId;
                    if (food == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", food.CategoryId);
                    ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", food.RestaurantId);
                    return View(food);
                }
            }


            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);






        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price")] Food food, HttpPostedFileBase uploadfile)
        {
            // sessiona category_id bilgisi atadım ürün değişikliği o şekilde yapıldı
            if (ModelState.IsValid)
            {

                string folder_name = "";
                string file_name = "";
                string test = Session["category_id"].ToString();
                int x = Int32.Parse(test);
                int restaurant_id = getSessionInfo();

                if (uploadfile!=null)
                {
                    folder_name = "Images";
                    file_name = Guid.NewGuid().ToString() + "_" + Path.GetFileName(uploadfile.FileName);
                    string file_path = Server.MapPath("~/" + folder_name + "/" + file_name);
                    uploadfile.SaveAs(file_path);

                    string database_path = folder_name + "/" + file_name;
                    food.CategoryId = x;
                    food.RestaurantId = restaurant_id;
                    food.PicturePath = database_path;
                    db.Entry(food).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("KategoriListele", "Home");
                }

                else
                {

                    // id ye göre resim pathini bul

                    var food_picture_path = (from product in db.Foods
                                             where product.Id == food.Id
                                             select product.PicturePath).SingleOrDefault();



                    // restaurant id bilgisine göre veritabanına ekleyecek. Resim yüklü değilse veritabanında picture path dokunma
                    food.CategoryId = x;
                    food.RestaurantId = restaurant_id;
                    food.PicturePath = food_picture_path;
                    
                    db.Entry(food).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("KategoriListele", "Home");
                }

               
               // return RedirectToAction("Index");
            }
           // ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", food.CategoryId);
           // ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", food.RestaurantId);
            return View(food);
        }

        // GET: Foods/Delete/5
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

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
            db.SaveChanges();
            return RedirectToAction("KategoriListele", "Home");
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
