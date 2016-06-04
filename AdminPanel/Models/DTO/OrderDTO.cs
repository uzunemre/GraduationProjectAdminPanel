using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.DTO
{
    public class OrderDTO
    {

        public long id;
        public int table_number;
        public string description;
        public string mac_address;
        public string products;
        public DateTime order_date;
        public decimal price;
        public bool delivery;


    }
}
