using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using OneTrack_v2.DbData;
using OneTrack_v2.Services;
using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DbData.Models;

namespace wcfOneTrak_API.Test
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IUtilityHelpService> _mockUtilityHelpService;
        private readonly AppDataContext _dbContext;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            // Set up the in-memory database
            var options = new DbContextOptionsBuilder<AppDataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new AppDataContext(options);

            // Mock the utility help service
            _mockUtilityHelpService = new Mock<IUtilityHelpService>();

            // Initialize the EmployeeService with the mocked dependencies
            _employeeService = new EmployeeService(_dbContext, _mockUtilityHelpService.Object);
        }

        [Fact]
        public async Task SearchEmployee_ReturnsExpectedResults()
        {
            // Arrange
            // Add test data to the in-memory database
            _dbContext.Employees.Add(new Employee { EmployeeId = 1, Geid = "12345", FirstName = "John", LastName = "Doe" });
            _dbContext.SaveChanges();

            // Act
            var result = await _employeeService.SearchEmployee(vGEID: "12345");

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.ObjData);
            //Assert.Single((IAsyncEnumerable<T>)result.ObjData);
        }

        [Fact]
        public async Task SearchEmployee_ReturnsEmptyResults_WhenNoMatch()
        {
            // Act
            var result = await _employeeService.SearchEmployee(vGEID: "NonExistentGEID");

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.ObjData);
            Assert.Empty((string)result.ObjData);
        }

        [Fact]
        public async Task SearchEmployee_HandlesException()
        {
            // Arrange
            // Force an exception by passing an invalid parameter
            _mockUtilityHelpService.Setup(x => x.LogError(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()));

            // Act
            var result = await _employeeService.SearchEmployee(vGEID: null);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.StatusCode);
            _mockUtilityHelpService.Verify(x => x.LogError(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()), Times.Once);
        }
    }
}
