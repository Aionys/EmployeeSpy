using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EmployeeSpy.Models;

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
            var Room = new Room()
            {
                Name = "Reception",
                Entrance = new Door()
                {
                    KeepOpenSeconds = 10,
                    EntranceControl = new GateKeeper() { SerialNo = "fff45a" },
                    ExitControl = new GateKeeper() { SerialNo = "dff44b" }
                }
            };

            var Room2 = new Room()
            {
                Name = "hr department",
                Entrance = new Door()
                {
                    KeepOpenSeconds = 10,
                    EntranceControl = new GateKeeper() { SerialNo = "fff44a" },
                    ExitControl = new GateKeeper() { SerialNo = "dff43b" }
                }
            };

            context.Doors.Add(Room.Entrance);
            context.Rooms.Add(Room);
            context.Doors.Add(Room2.Entrance);
            context.Rooms.Add(Room2);

            Room.InternalDoors.Add(Room2.Entrance);

            var hr = new Employee()
            {
                FirstName = "Jennifer",
                LastName = "Lopez",
                WorkPlace = Room2
            };

            context.Employees.Add(hr);
            context.SaveChanges();
        }
    }
}