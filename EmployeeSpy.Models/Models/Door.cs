namespace EmployeeSpy.Models
{
    public class Door : BaseEntity
    {
        public Room Room { get; set; }

        public int KeepOpenSeconds { get; set; }

        public Employee Guard { get; set; }

        public GateKeeper EntranceControl { get; set; }

        public GateKeeper ExitControl { get; set; }
    }
}
