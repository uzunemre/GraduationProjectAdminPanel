using AdminPanel.Models;
using AdminPanel.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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


            //Buraya devam et

            /*    var category = from cat in context.Categories
                               where cat.restaurant_code == id
                               select new CategoryDTO()
                               {
                                   categoryname = cat.name
                               };*/

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
                                                   picture = food.PicturePath



                                               }).ToList();




                list_cat.Add(new CategoryProductDTO { name = item.categoryname, products = product_accord_category, picture = item.categorypicture });

            }

            root.categories = list_cat;

            return root;



        }


    }
}
