using System.Collections.Generic;

namespace EmployeeSpy.Models
{
    public class Employee : Person
    {
        public List<Door> DoorsUderControl { get; set; }

        public Room WorkPlace { get; set; }
    }
}
