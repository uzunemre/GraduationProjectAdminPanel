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

        public ActionResult Test()
        {
            var restaurants = context.Restaurants.ToList();
            return View(restaurants);
        }

        public ActionResult Index()
        {
            

            if(Session["UserID"]==null)
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
            var user = context.Restaurants.Where(u => u.User_Name == restaurant_user.User_Name && u.Password == restaurant_user.Password).FirstOrDefault();
            if(user!=null)
            {
                Session["UserID"] = restaurant_user.Code.ToString();
                Session["Username"] = restaurant_user.User_Name.ToString();
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
            if(Session["UserID"]!=null)
            {
                return View();
            }

            else

            {
                return RedirectToAction("Login");
            }

            
        }


     






    }
}