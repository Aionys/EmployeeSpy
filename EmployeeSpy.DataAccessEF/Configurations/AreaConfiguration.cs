using EmployeeSpy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeSpy.DataAccess.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(b => b.Id).UseIdentityColumn<int>();
            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Property(b => b.AccessLevel).IsRequired().HasDefaultValue(AccessLevelType.Anyone);

            builder.HasOne(b => b.Entrance).WithOne().HasForeignKey<Room>(d => d.EntranceId);
            builder.HasMany(b => b.InternalDoors).WithOne(d => d.Room);
        }
    }
}
