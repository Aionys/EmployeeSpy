using EmployeeSpy.Core.Abstractions;
using EmployeeSpy.Models;
using EmployeeSpy.Services;
using Moq;
using Xunit;

namespace EmployeeSpy.UnitTests
{
    public class GateKeeperServiceTests
    {
        private const int _controlId = 55;
        private const int _personId = 4;

        private readonly Mock<IRoomsRepository> _roomsRepo;
        private readonly Mock<IRepository<Employee>> _employeesRepo;
        private readonly Mock<IRepository<Visitor>> _visitorsRepo;
        private readonly Mock<IRepository<MovementLogRecord>> _movementsRepo;

        public GateKeeperServiceTests()
        {
            _roomsRepo = new Mock<IRoomsRepository>();
            _employeesRepo = new Mock<IRepository<Employee>>();
            _visitorsRepo = new Mock<IRepository<Visitor>>();
            _movementsRepo = new Mock<IRepository<MovementLogRecord>>();
        }

        [Theory]
        [InlineData(AccessLevelType.Anyone, true)]
        [InlineData(AccessLevelType.StaffOnly, false)]
        public void Visitor_PassInto_Room(AccessLevelType roomAccessLevel, bool isAllowed)
        {
            // Arrange
            var visitor = new Visitor() { Id = _personId, FirstName = "Mary", LastName = "Smith" };
            var room = CreateTestRoom(roomAccessLevel, MoveDirection.Enter);

            _roomsRepo.Setup(r => r.GetByControlId(_controlId)).Returns(room);
            _visitorsRepo.Setup(r => r.GetById(_personId)).Returns(visitor);

            // Act
            var target = new GateKeeperService(
                _roomsRepo.Object,
                _employeesRepo.Object,
                _visitorsRepo.Object,
                _movementsRepo.Object);

            var result = target.VerifyVisitorPassAttempt(_personId, _controlId);

            // Accert
            Assert.Equal(isAllowed, result);
            _movementsRepo.Verify(m => m.Add(It.Is<MovementLogRecord>(r =>
                    r.Person.Id == visitor.Id &&
                    r.PassedDoor.Id == room.Entrance.Id &&
                    r.MoveDirection == MoveDirection.Enter)));
        }

        [Theory]
        [InlineData(AccessLevelType.Anyone, true)]
        [InlineData(AccessLevelType.StaffOnly, true)]
        [InlineData(AccessLevelType.HighSecurity, false)]
        public void Visitor_ExitFrom_Room(AccessLevelType roomAccessLevel, bool isAllowed)
        {
            // Arrange
            var visitor = new Visitor() { Id = _personId, FirstName = "Mary", LastName = "Smith" };
            var room = CreateTestRoom(roomAccessLevel, MoveDirection.Exit);

            _roomsRepo.Setup(r => r.GetByControlId(_controlId)).Returns(room);
            _visitorsRepo.Setup(r => r.GetById(_personId)).Returns(visitor);

            // Act
            var target = new GateKeeperService(
                _roomsRepo.Object,
                _employeesRepo.Object,
                _visitorsRepo.Object,
                _movementsRepo.Object);

            var result = target.VerifyVisitorPassAttempt(_personId, _controlId);

            // Accert
            Assert.Equal(isAllowed, result);
            
            _visitorsRepo.Verify(m => m.GetById(_personId));
            _roomsRepo.Verify(m => m.GetByControlId(_controlId));

            _movementsRepo.Verify(m => m.Add(It.Is<MovementLogRecord>(r =>
                    r.Person.Id == visitor.Id &&
                    r.PassedDoor.Id == room.Entrance.Id &&
                    r.MoveDirection == MoveDirection.Exit)));
        }

        [Theory]
        [InlineData(AccessLevelType.Anyone, true)]
        [InlineData(AccessLevelType.StaffOnly, true)]
        [InlineData(AccessLevelType.HighSecurity, false)]
        public void Employee_PassInto_Room(AccessLevelType roomAccessLevel, bool isAllowed)
        {
            // Arrange
            var employee = CreateTestEmployee();
            var room = CreateTestRoom(roomAccessLevel, MoveDirection.Enter);

            _roomsRepo.Setup(r => r.GetByControlId(_controlId)).Returns(room);
            _employeesRepo.Setup(r => r.GetById(_personId)).Returns(employee);

            // Act
            var target = new GateKeeperService(
                _roomsRepo.Object,
                _employeesRepo.Object,
                _visitorsRepo.Object,
                _movementsRepo.Object);

            var result = target.VerifyEmployeePassAttempt(_personId, _controlId);

            // Accert
            Assert.Equal(isAllowed, result);

            _employeesRepo.Verify(m => m.GetById(_personId));
            _roomsRepo.Verify(m => m.GetByControlId(_controlId));

            _movementsRepo.Verify(m => m.Add(It.Is<MovementLogRecord>(r =>
                    r.Person.Id == employee.Id &&
                    r.PassedDoor.Id == room.Entrance.Id &&
                    r.MoveDirection == MoveDirection.Enter)));
        }

        [Theory]
        [InlineData(AccessLevelType.Anyone, true)]
        [InlineData(AccessLevelType.StaffOnly, true)]
        [InlineData(AccessLevelType.HighSecurity, false)]
        public void Employee_ExitFrom_Room(AccessLevelType roomAccessLevel, bool isAllowed)
        {
            // Arrange
            var employee = CreateTestEmployee();
            var room = CreateTestRoom(roomAccessLevel, MoveDirection.Exit);

            _roomsRepo.Setup(r => r.GetByControlId(_controlId)).Returns(room);
            _employeesRepo.Setup(r => r.GetById(_personId)).Returns(employee);

            // Act
            var target = new GateKeeperService(
                _roomsRepo.Object,
                _employeesRepo.Object,
                _visitorsRepo.Object,
                _movementsRepo.Object);

            var result = target.VerifyEmployeePassAttempt(_personId, _controlId);

            // Accert
            Assert.Equal(isAllowed, result);

            _employeesRepo.Verify(m => m.GetById(_personId));
            _roomsRepo.Verify(m => m.GetByControlId(_controlId));

            _movementsRepo.Verify(m => m.Add(It.Is<MovementLogRecord>(r =>
                    r.Person.Id == employee.Id &&
                    r.PassedDoor.Id == room.Entrance.Id &&
                    r.MoveDirection == MoveDirection.Exit)));
        }

        [Theory]
        [InlineData(MoveDirection.Enter)]
        [InlineData(MoveDirection.Exit)]
        public void Employee_HghSecurityRoom_Allowed(MoveDirection moveDirection)
        {
            // Arrange
            var employee = CreateTestEmployee();
            var room = CreateTestRoom(AccessLevelType.HighSecurity, moveDirection);
            employee.WorkPlace = room;

            _roomsRepo.Setup(r => r.GetByControlId(_controlId)).Returns(room);
            _employeesRepo.Setup(r => r.GetById(_personId)).Returns(employee);
            

            // Act
            var target = new GateKeeperService(
                _roomsRepo.Object,
                _employeesRepo.Object,
                _visitorsRepo.Object,
                _movementsRepo.Object);

            var result = target.VerifyEmployeePassAttempt(_personId, _controlId);

            // Accert
            Assert.True(result, $"Employee should be allowed to {moveDirection} the room");

            _employeesRepo.Verify(m => m.GetById(_personId));
            _roomsRepo.Verify(m => m.GetByControlId(_controlId));

            _movementsRepo.Verify(m => m.Add(It.Is<MovementLogRecord>(r =>
                    r.Person.Id == employee.Id &&
                    r.PassedDoor.Id == room.Entrance.Id &&
                    r.MoveDirection == moveDirection)));
        }

        private Room CreateTestRoom(AccessLevelType roomAccessLevel, MoveDirection moveDirection)
        {
            return new Room()
            {
                Id = 1,
                AccessLevel = roomAccessLevel,
                Entrance = new Door()
                {
                    Id = 1,

                    EntranceControl = moveDirection == MoveDirection.Enter
                        ? new GateKeeper() { Id = _controlId, SerialNo = "tt55" }
                        : null,

                    ExitControl = moveDirection == MoveDirection.Exit
                        ? new GateKeeper() { Id = _controlId, SerialNo = "tt55" }
                        : null,

                    KeepOpenSeconds = 5
                }
            };
        }

        private Employee CreateTestEmployee()
        {
            return new Employee()
            {
                Id = _personId,
                FirstName = "Mary",
                LastName = "Smith",
                WorkPlace = new Room() { Id = 5, AccessLevel = AccessLevelType.HighSecurity, Name = "work place1" }
            };
        }
    }
}
