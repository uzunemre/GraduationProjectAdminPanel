using AdminPanel.Models;
using AdminPanel.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AdminPanel.Controllers
{
    public class ApiProductController : ApiController
    {

        GraduationProjectContext context = new GraduationProjectContext();
        List<CategoryProductDTO> list_cat = new List<CategoryProductDTO>();
        RootObject root = new RootObject();
        List<CategoryDTO> list_cat_res = new List<CategoryDTO>();


        [ResponseType(typeof(List<string>))]
        public async Task<IHttpActionResult> GetCategory(int id)
        {

            List<string> list_cat_res = new List<string>();

            var category = from cat in context.Categories
                           where cat.RestaurantId == id
                           select new CategoryDTO()
                           {
                               categoryname = cat.Name
                           };

            foreach (var item in category)
            {
                list_cat_res.Add(item.categoryname);
            }

            if (category == null)
            {
                return NotFound();
            }

            return Ok(list_cat_res);

        }


        public RootObject GetFood(string id)
        {

            Thread.Sleep(2000);

            //Buraya devam et

            /*    var category = from cat in context.Categories
                               where cat.restaurant_code == id
                               select new CategoryDTO()
                               {
                                   categoryname = cat.name
                               };*/

            // bu cihaza ait kategorileri tutar. test_category nesnesi
            var test_category = from device in context.Devices
                                join rest in context.Restaurants on device.RestaurantId equals rest.Id
                                join cat in context.Categories on rest.Id equals cat.RestaurantId
                                where device.MacAdress == id
                                select new CategoryDTO()
                                {
                                    categoryname = cat.Name,
                                    categorypicture = cat.PicturePath  // BURAYA BAK PİCTURE GÖSTERİLMEYECEK
                                };


            /*   var test = from device in context.Devices
                          join rest in context.Restaurants on device.restaurant_code equals rest.restaurant_code
                          join food in context.Foods on rest.restaurant_code equals food.restaurant_code
                          join cat in context.Categories on food.category_id equals cat.id;*/



            foreach (var item in test_category)
            {


                var product_accord_category = (from device in context.Devices
                                               join rest in context.Restaurants on device.RestaurantId equals rest.Id
                                               join food in context.Foods on rest.Id equals food.RestaurantId
                                               join cat in context.Categories on food.CategoryId equals cat.Id
                                               where id == device.MacAdress && item.categoryname == cat.Name



                                               select new ProductDTO()
                                               {
                                                   Id = food.Id,
                                                   name = food.Name,
                                                   description = food.Description,
                                                   price = food.Price,
                                                   picture = food.PicturePath,
                                                   calorie = (int)food.Calorie
                                                   
                                                    }).ToList();




                list_cat.Add(new CategoryProductDTO { name = item.categoryname, products = product_accord_category, picture = item.categorypicture });

            }

            root.categories = list_cat;

            return root;



        }



        [HttpPost]
        public String OrderPost([FromBody]OrderDTO order)
        {
            // int default_table_number = 0;

            //  string description = order != null ? order.description : "";
            //  string table_number = order != null ? (order.table_number).ToString() :"0";


            /*if (Int32.TryParse(table_number, out default_table_number))
            {
                //return description+" "+table_number; // masa numarası 0 veya doğru haliyle döner

                

            }*/

            if (order!= null)
            {
                var entity_order = new Order();
                var restaurant_id = (from devices in context.Devices                  // istekte bulunulan mac 
                                    where devices.MacAdress == order.mac_address     // adresinden
                                    select devices.RestaurantId).SingleOrDefault();                     // id bul

                // id doğru gelmezse hata var. Doğru geldiği sürece sıkıntı yok. Mac adresi doğru
                // alındığı sürece sıkıntı çıkarmaz
                
                entity_order.RestaurantId = restaurant_id;
                entity_order.TableNumber = order.table_number;
                entity_order.Products = order.products;
                entity_order.Detail = order.description;
                entity_order.Date = DateTime.Now;
                entity_order.Price = order.price;
                entity_order.Delivery = order.delivery;
                
                context.Orders.Add(entity_order);
                context.SaveChanges();
                return "Success";
            }


          


            return "Something wrong";
        }






    }
}
