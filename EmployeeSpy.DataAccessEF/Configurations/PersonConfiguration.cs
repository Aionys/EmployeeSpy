using EmployeeSpy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeSpy.DataAccess.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(b => b.Id).UseIdentityColumn<int>();
            builder.Property(b => b.FirstName).IsRequired().HasMaxLength(250);
            builder.Property(b => b.LastName).IsRequired().HasMaxLength(250);
        }
    }
}
