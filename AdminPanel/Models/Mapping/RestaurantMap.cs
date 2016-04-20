using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AdminPanel.Models.Mapping
{
    public class RestaurantMap : EntityTypeConfiguration<Restaurant>
    {
        public RestaurantMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(128);

            this.Property(t => t.Phone)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Mail)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PictureFolderPath)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Restaurant");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Mail).HasColumnName("Mail");
            this.Property(t => t.PictureFolderPath).HasColumnName("PictureFolderPath");
        }
    }
}
