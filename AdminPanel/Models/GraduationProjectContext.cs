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

        public DbSet<Category> Categories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new DeviceMap());
            modelBuilder.Configurations.Add(new FoodMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new RestaurantMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
