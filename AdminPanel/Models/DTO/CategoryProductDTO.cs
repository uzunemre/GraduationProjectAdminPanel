using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.DTO
{
    public class CategoryProductDTO
    {
        public string name { get; set; }
        public string picture { get; set; }
        public List<ProductDTO> products { get; set; }
    }
}
