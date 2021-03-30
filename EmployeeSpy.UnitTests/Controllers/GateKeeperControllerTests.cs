using EmployeeSpy.Controllers;
using EmployeeSpy.Core.Abstractions;
using Moq;
using Xunit;

namespace EmployeeSpy.UnitTests.Controllers
{
    public class GateKeeperControllerTests
    {
        private readonly Mock<IGateKeeperService> _gkeeperService;

        public GateKeeperControllerTests()
        {
            _gkeeperService = new Mock<IGateKeeperService>();
        }

        [Fact]
        public void Visitor_PassAttempt()
        {
            // Arrange
            var expectedResult = true;
            var dto = new Dto.PassAttemptDto()
            {
                GateKeeperId = 3,
                PersonId = 5,
                PersonType = Dto.PersonType.Visitor
            };

            _gkeeperService.Setup(s => s.VerifyVisitorPassAttempt(dto.PersonId, dto.GateKeeperId)).Returns(expectedResult);

            // Act
            var target = new GateKeeperController(_gkeeperService.Object);
            var actualResult = target.Post(dto);

            // Assert
            Assert.Equal(expectedResult, actualResult);
            _gkeeperService.Verify(s => s.VerifyVisitorPassAttempt(
                It.Is<int>(i => i == dto.PersonId),
                It.Is<int>(i => i == dto.GateKeeperId)));
        }

        [Fact]
        public void Employee_PassAttempt()
        {
            // Arrange
            var expectedResult = true;
            var dto = new Dto.PassAttemptDto()
            {
                GateKeeperId = 3,
                PersonId = 5,
                PersonType = Dto.PersonType.Employee
            };

            _gkeeperService.Setup(s => s.VerifyEmployeePassAttempt(dto.PersonId, dto.GateKeeperId)).Returns(expectedResult);

            // Act
            var target = new GateKeeperController(_gkeeperService.Object);
            var actualResult = target.Post(dto);

            // Assert
            Assert.Equal(expectedResult, actualResult);
            _gkeeperService.Verify(s => s.VerifyEmployeePassAttempt(
                It.Is<int>(i => i == dto.PersonId),
                It.Is<int>(i => i == dto.GateKeeperId)));
        }

    }
}
