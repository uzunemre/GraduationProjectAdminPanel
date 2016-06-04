using System;
using System.Collections.Generic;

namespace AdminPanel.Models
{
    public partial class Order
    {
        public long Id { get; set; }
        public int RestaurantId { get; set; }
        public int TableNumber { get; set; }
        public string Products { get; set; }
        public string Detail { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Price { get; set; }
        public bool Delivery { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
