using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AdminPanel.Models.Mapping
{
    public class FoodMap : EntityTypeConfiguration<Food>
    {
        public FoodMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PicturePath)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Food");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.PicturePath).HasColumnName("PicturePath");
            this.Property(t => t.RestaurantId).HasColumnName("RestaurantId");

            // Relationships
            this.HasRequired(t => t.Category)
                .WithMany(t => t.Foods)
                .HasForeignKey(d => d.CategoryId);
            this.HasRequired(t => t.Restaurant)
                .WithMany(t => t.Foods)
                .HasForeignKey(d => d.RestaurantId);

        }
    }
}
