using EmployeeSpy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeSpy.DataAccess.Configurations
{
    public class DoorConfiguration : IEntityTypeConfiguration<Door>
    {
        public void Configure(EntityTypeBuilder<Door> builder)
        {
            builder.Property(b => b.Id).UseIdentityColumn<int>();
            builder.Property(b => b.KeepOpenSeconds).IsRequired().HasDefaultValue(10);

            builder.HasOne(b => b.EntranceControl);
            builder.HasOne(b => b.ExitControl);
            builder.HasOne(b => b.Guard);
        }
    }
}
