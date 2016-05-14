using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<decimal> price { get; set; }
        public string picture { get; set; }
       // public string category_name { get; set; }

    }


   

    
}
