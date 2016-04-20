using System;
using System.Collections.Generic;

namespace AdminPanel.Models
{
    public partial class Device
    {
        public string MacAdress { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
