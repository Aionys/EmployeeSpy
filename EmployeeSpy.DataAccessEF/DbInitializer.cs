using System.Linq;
using EmployeeSpy.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSpy.DataAccessEF
{
    public static class DbInitializer
    {
        public static void Initialize(EmployeeSpyContext context)
        {
            context.Database.Migrate();

            if (context.Employees.Count() > 0)
            {
                return;
            }

            PopulateData(context);
        }

        private static void PopulateData(EmployeeSpyContext context)
        {
            var room = new Room()
            {
                Name = "Reception",
                Entrance = new Door()
                {
                    KeepOpenSeconds = 10,
                    EntranceControl = new GateKeeper() { SerialNo = "fff45a" },
                    ExitControl = new GateKeeper() { SerialNo = "dff44b" },
                },
            };

            var room2 = new Room()
            {
                Name = "hr department",
                Entrance = new Door()
                {
                    KeepOpenSeconds = 10,
                    EntranceControl = new GateKeeper() { SerialNo = "fff44a" },
                    ExitControl = new GateKeeper() { SerialNo = "dff43b" },
                },
            };

            context.Doors.Add(room.Entrance);
            context.Rooms.Add(room);
            context.Doors.Add(room2.Entrance);
            context.Rooms.Add(room2);

            room.InternalDoors.Add(room2.Entrance);

            var hr = new Employee()
            {
                FirstName = "Jennifer",
                LastName = "Lopez",
                WorkPlace = room2,
            };

            context.Employees.Add(hr);
            context.SaveChanges();
        }
    }
}