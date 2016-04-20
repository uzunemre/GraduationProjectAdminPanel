using System;
using System.Collections.Generic;

namespace AdminPanel.Models
{
    public partial class Category
    {
        public Category()
        {
            this.Foods = new List<Food>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PicturePath { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
    }
}
