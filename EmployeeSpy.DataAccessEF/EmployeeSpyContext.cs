using System.Reflection;
using EmployeeSpy.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSpy.DataAccessEF
{
    public class EmployeeSpyContext : DbContext
    {
        public EmployeeSpyContext(DbContextOptions<EmployeeSpyContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Door> Doors { get; set; }

        public DbSet<GateKeeper> GateKeepers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<MovementLogRecord> MovementLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
