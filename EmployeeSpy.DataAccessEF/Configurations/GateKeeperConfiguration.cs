using EmployeeSpy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeSpy.DataAccess.Configurations
{
    public class GateKeeperConfiguration : IEntityTypeConfiguration<GateKeeper>
    {
        public void Configure(EntityTypeBuilder<GateKeeper> builder)
        {
            builder.Property(b => b.Id).UseIdentityColumn<int>();
            builder.Property(b => b.SerialNo).HasMaxLength(50).IsRequired();
        }
    }
}
