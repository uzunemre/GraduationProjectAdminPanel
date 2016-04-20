using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AdminPanel.Models.Mapping;

namespace AdminPanel.Models
{
    public partial class GraduationProjectContext : DbContext
    {
        static GraduationProjectContext()
        {
            Database.SetInitializer<GraduationProjectContext>(null);
        }

        public GraduationProjectContext()
            : base("Name=GraduationProjectContext")
        {
        }

        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Device> Devices { get; set; }
        public IDbSet<Food> Foods { get; set; }
        public IDbSet<Restaurant> Restaurants { get; set; }
        public IDbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new DeviceMap());
            modelBuilder.Configurations.Add(new FoodMap());
            modelBuilder.Configurations.Add(new RestaurantMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
