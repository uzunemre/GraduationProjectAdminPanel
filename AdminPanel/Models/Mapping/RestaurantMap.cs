using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AdminPanel.Models.Mapping
{
    public class RestaurantMap : EntityTypeConfiguration<Restaurant>
    {
        public RestaurantMap()
        {
            // Primary Key
            this.HasKey(t => t.Code);

            // Properties
            this.Property(t => t.User_Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Restaurant_Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Password)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Restaurants");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.User_Name).HasColumnName("User_Name");
            this.Property(t => t.Restaurant_Name).HasColumnName("Restaurant_Name");
            this.Property(t => t.Password).HasColumnName("Password");
        }
    }
}
