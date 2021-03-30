using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSpy.DataAccessEF
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var typesToRegister = assembly
                .GetTypes()
                .Where(t => t
                    .GetInterfaces()
                    .Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
