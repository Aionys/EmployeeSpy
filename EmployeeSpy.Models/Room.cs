using System.Collections.Generic;

namespace EmployeeSpy.Models
{
    public enum AccessLevelType 
    {
        /// <summary>
        /// Any person can enter and exit the Room
        /// </summary>
        Anyone = 0,

        /// <summary>
        /// Only staff members can enter the Room, anyone can exit
        /// </summary>
        StaffOnly = 1,
        
        /// <summary>
        /// Only staff members who work in the Room can enter/exit.
        /// </summary>
        HighSecurity = 2
    }

    public class Room : BaseEntity
    {
        public Room()
        {
            InternalDoors = new List<Door>();
        }

        public string Name { get; set; }

        public List<Door> InternalDoors { get; set; }

        public Door Entrance { get; set; }

        public int EntranceId { get; set; }

        public AccessLevelType AccessLevel { get; set; }
    }
}
