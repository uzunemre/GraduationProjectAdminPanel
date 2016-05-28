
using AdminPanel.Models;
using AdminPanel.SignalR;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class HomeController : Controller
    {

        private GraduationProjectContext context = new GraduationProjectContext();
        
        private int getSessionInfo()
        {
            string test = Session["UserID"].ToString();
            int x = Int32.Parse(test);
            return x;
        }


        public ActionResult GetMessages()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            return PartialView("OrderList", _messageRepository.GetAllMessages(1000));
        }

        public ActionResult Index()
        {
             
           
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }

            return View();
            
            
            
            
            /*  else

            {
                using (GraduationProjectContext db = new GraduationProjectContext())
                {
                    int user_id = getSessionInfo();
                    List<OrderDTO> data = (from orders in db.Orders
                                           where user_id == orders.RestaurantId

                                           select new OrderDTO()
                                           {
                                               table_number = orders.TableNumber,
                                               description = orders.OrderDetail,



                                           }).ToList();

                    // Order DTO Listesi siparişlerde gösterilecek
                                        
                    return View(data);
                }
            }*/

            /*    else
                {
                    int a = getSessionInfo();
                    var foods = context.Foods.Include(f => f.Category).Include(f => f.Restaurant).Where(f => f.RestaurantId == a);
                    return View(foods.ToList());
                }*/

            
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

                Session["UserID"] = user.Id.ToString();
                Session["Username"] = user.UserName.ToString();
                return RedirectToAction("KategoriListele");

            }

            else
            {
                ModelState.AddModelError("", "Username or Password is Wrong");
            }

            return View();
        }


        


        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }



        public ActionResult KategoriListele()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }
         //   int a = getSessionInfo();
         //   var categories = context.Categories.Include(c => c.Restaurant).Where(c => c.RestaurantId == a);
         //   return View(categories.ToList());

            // restauranta ait kategorileri çektim ViewBag ile yolladım
            int restaurant_id = getSessionInfo();
            var restaurant_categories = context.Categories.Where(x => x.RestaurantId == restaurant_id).ToList();
            return View(restaurant_categories);
        }














    }


   








}