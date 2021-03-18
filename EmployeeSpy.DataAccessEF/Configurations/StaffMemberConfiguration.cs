using EmployeeSpy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeSpy.DataAccess.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(b => b.Id).UseIdentityColumn<int>();

            builder.HasMany(b => b.DoorsUderControl).WithOne(d => d.Guard);
            builder.HasOne(b => b.WorkPlace).WithMany().IsRequired();
        }
    }
}
