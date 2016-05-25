using System;
using System.Collections.Generic;

namespace AdminPanel.Models
{
    public partial class Order
    {
        public long Id { get; set; }
        public int RestaurantId { get; set; }
        public int TableNumber { get; set; }
        public string OrderDetail { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
