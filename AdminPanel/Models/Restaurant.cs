using System;
using System.Collections.Generic;

namespace AdminPanel.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            this.Categories = new List<Category>();
            this.Devices = new List<Device>();
            this.Foods = new List<Food>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string PicturePath { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
    }
}
