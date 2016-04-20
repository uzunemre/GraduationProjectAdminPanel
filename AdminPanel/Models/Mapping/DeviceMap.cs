using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AdminPanel.Models.Mapping
{
    public class DeviceMap : EntityTypeConfiguration<Device>
    {
        public DeviceMap()
        {
            // Primary Key
            this.HasKey(t => t.MacAdress);

            // Properties
            this.Property(t => t.MacAdress)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Device");
            this.Property(t => t.MacAdress).HasColumnName("MacAdress");
            this.Property(t => t.RestaurantId).HasColumnName("RestaurantId");

            // Relationships
            this.HasRequired(t => t.Restaurant)
                .WithMany(t => t.Devices)
                .HasForeignKey(d => d.RestaurantId);

        }
    }
}
