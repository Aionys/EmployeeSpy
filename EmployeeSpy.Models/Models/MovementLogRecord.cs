using System;

namespace EmployeeSpy.Models
{
    public enum MoveDirection { enter = 0, exit = 1 }

    public class MovementLogRecord : BaseEntity
    {
        public Person Person { get; set; }

        public Door PassedDoor { get; set; }

        public MoveDirection MoveDirection { get; set; }

        public DateTime MoveTime { get; set; }
    }
}
