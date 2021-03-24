using EmployeeSpy.Core.Abstractions;
using EmployeeSpy.Models;
using System;

namespace EmployeeSpy.Services
{
    public class GateKeeperService : IGateKeeperService
    {
        private readonly IRoomsRepository _roomsRepo;
        private readonly IRepository<Employee> _employeeRepo;
        private readonly IRepository<Visitor> _visitorRepo;
        private readonly IRepository<MovementLogRecord> _movementsRepo;

        public GateKeeperService(
            IRoomsRepository roomsRepo,
            IRepository<Employee> employeeRepo,
            IRepository<Visitor> visitorRepo,
            IRepository<MovementLogRecord> movementsRepo)
        {
            _roomsRepo = roomsRepo;
            _employeeRepo = employeeRepo;
            _visitorRepo = visitorRepo;
            _movementsRepo = movementsRepo;
        }

        public bool VerifyVisitorPassAttempt(int personId,  int gateKeeperId)
        {
            return false;
        }

        public bool VerifyEmployeePassAttempt(int personId, int gateKeeperId)
        {
            return false;
        }
    }
}
