using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AdminPanel.Models.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Products)
                .IsRequired();

            this.Property(t => t.Detail)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Orders");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RestaurantId).HasColumnName("RestaurantId");
            this.Property(t => t.TableNumber).HasColumnName("TableNumber");
            this.Property(t => t.Products).HasColumnName("Products");
            this.Property(t => t.Detail).HasColumnName("Detail");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Delivery).HasColumnName("Delivery");

            // Relationships
            this.HasRequired(t => t.Restaurant)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.RestaurantId);

        }
    }
}
