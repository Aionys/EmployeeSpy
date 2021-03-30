using System;
using EmployeeSpy.Core.Abstractions;
using EmployeeSpy.Models;

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

        public bool VerifyVisitorPassAttempt(int personId, int gateKeeperId)
        {
            var visitor = _visitorRepo.GetById(personId);

            if (visitor == null)
            {
                return false;
            }

            var room = _roomsRepo.GetByControlId(gateKeeperId);

            LogMovement(visitor, room.Entrance);
            return room.AccessLevel == AccessLevelType.Anyone ||
                 (room.AccessLevel == AccessLevelType.StaffOnly && GetMoveDirection(room.Entrance) == MoveDirection.Exit);
        }

        public bool VerifyEmployeePassAttempt(int personId, int gateKeeperId)
        {
            var employee = _employeeRepo.GetById(personId);

            if (employee == null)
            {
                return false;
            }

            var room = _roomsRepo.GetByControlId(gateKeeperId);
            LogMovement(employee, room.Entrance);

            switch (room.AccessLevel)
            {
                case AccessLevelType.Anyone:
                case AccessLevelType.StaffOnly:
                    return true;

                case AccessLevelType.HighSecurity:
                    return employee.WorkPlace.Id == room.Id;

                default:
                    return false;
            }
        }

        private void LogMovement(Person person, Door door)
        {
            var r = new MovementLogRecord()
            {
                MoveTime = DateTime.UtcNow,
                PassedDoor = door,
                Person = person,
                MoveDirection = GetMoveDirection(door),
            };

            _movementsRepo.Add(r);
        }

        private MoveDirection GetMoveDirection(Door door)
        {
            return door.EntranceControl != null ? MoveDirection.Enter : MoveDirection.Exit;
        }
    }
}
