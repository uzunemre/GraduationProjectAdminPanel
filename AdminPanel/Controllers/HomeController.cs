
using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        GraduationProjectContext context = new GraduationProjectContext();



        public ActionResult Index()
        {


            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Restaurant restaurant_user)
        {
            var user = context.Restaurants.Where(u => u.UserName == restaurant_user.UserName && u.Password == restaurant_user.Password).FirstOrDefault();
            if (user != null)
            {
                Session["UserID"] = restaurant_user.Id.ToString();
                Session["Username"] = restaurant_user.UserName.ToString();
                return RedirectToAction("LoggedIn");

            }

            else
            {
                ModelState.AddModelError("", "Username or Password is Wrong");
            }

            return View();
        }



        public ActionResult LoggedIn()
        {
            


            if (Session["UserID"] != null)
            {
                string user_id = (Session["UserId"].ToString());
                int x = Int32.Parse(user_id);
                var categories = from cat in context.Categories
                                 where cat.RestaurantId == 1000
                                 select cat;
                                    
                return View(categories);
            }

            else

            {
                return RedirectToAction("Login");
            }


        }













    }
}