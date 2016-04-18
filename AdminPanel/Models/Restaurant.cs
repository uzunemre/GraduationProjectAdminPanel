using System;
using System.Collections.Generic;

namespace AdminPanel.Models
{
    public partial class Restaurant
    {
        public int Code { get; set; }
        public string User_Name { get; set; }
        public string Restaurant_Name { get; set; }
        public string Password { get; set; }
    }
}
