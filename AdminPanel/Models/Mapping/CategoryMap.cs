using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AdminPanel.Models.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PicturePath)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Category");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.PicturePath).HasColumnName("PicturePath");
            this.Property(t => t.RestaurantId).HasColumnName("RestaurantId");

            // Relationships
            this.HasRequired(t => t.Restaurant)
                .WithMany(t => t.Categories)
                .HasForeignKey(d => d.RestaurantId);

        }
    }
}
