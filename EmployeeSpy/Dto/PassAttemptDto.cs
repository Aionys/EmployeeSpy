using EmployeeSpy.Dto.Enumerations;

namespace EmployeeSpy.Dto
{
    public class PassAttemptDto
    {
        public int PersonId { get; set; }

        public PersonType PersonType { get; set; }

        public int GateKeeperId { get; set; }
    }
}
