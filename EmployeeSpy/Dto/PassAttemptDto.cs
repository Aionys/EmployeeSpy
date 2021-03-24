using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSpy.Dto
{
    public enum PersonType { Visitor = 0, Employee = 1 }

    public class PassAttemptDto
    {
        public int PersonId { get; set; }

        public PersonType PersonType { get; set; }

        public int GateKeeperId { get; set; }
    }
}
