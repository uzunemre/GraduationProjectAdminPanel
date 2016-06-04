using System;
using System.Collections.Generic;

namespace AdminPanel.Models
{
    public partial class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string PicturePath { get; set; }
        public int RestaurantId { get; set; }
        public int Calorie { get; set; }
        public virtual Category Category { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
