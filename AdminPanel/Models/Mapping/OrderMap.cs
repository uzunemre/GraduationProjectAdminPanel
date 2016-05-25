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
            this.Property(t => t.OrderDetail)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Orders");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RestaurantId).HasColumnName("RestaurantId");
            this.Property(t => t.TableNumber).HasColumnName("TableNumber");
            this.Property(t => t.OrderDetail).HasColumnName("OrderDetail");

            // Relationships
            this.HasRequired(t => t.Restaurant)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.RestaurantId);

        }
    }
}
