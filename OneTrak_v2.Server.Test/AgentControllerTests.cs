using Moq;
using OneTrack_v2.Services;
using DataModel.Response;
using OneTrack_v2.DataModel;
using OneTrack_v2.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace wcfOneTrak_API.Test
{
    public class AgentControllerTests
    {
        private readonly Mock<IAgentService> _agentService;

        public AgentControllerTests()
        {
            _agentService = new Mock<IAgentService>();
        }

        [Fact]
        public async Task GetAgentByEmployeeIDTest()
        {
            // Arrange
            var expectedServiceResult = new ReturnResult
            {
                Success = true,
                StatusCode = 200,
                ObjData = new OputAgent()
                {
                    EmployeeID = 685519,
                    LastName = "HS WIMTOBRN",
                    FirstName = "AMANDA",
                    MiddleName = "",
                    EmployeeSSN = "",
                    Soeid = null,
                    Address1 = "",
                    Address2 = "",
                    City = "",
                    State = "CO",
                    Zip = "",
                    Phone = null,
                    DateOfBirth = new DateTime(1900, 1, 1)
                }
            };

            _agentService.Setup(x => x.GetAgentByEmployeeID(It.IsAny<int>())).Returns(expectedServiceResult);

            var controller = new AgentController(_agentService.Object);

            // Act
            var actionResult = await controller.GetAgentByEmployeeID(685519);

            // Assert
            var result = Assert.IsType<ObjectResult>(actionResult);
            //Assert.Equal(200, result.StatusCode);
            var returnResult = Assert.IsType<ReturnResult>(result.Value);
            Assert.True(returnResult.Success);
            Assert.Equal(200, returnResult.StatusCode); // This line may be redundant if StatusCode is always expected to be 200 on success

            // Assuming ObjData is expected to be a single object and not a collection
            var agentData = Assert.IsType<OputAgent>(returnResult.ObjData); // Adjust this line if ObjData is actually a collection
            Assert.Equal(685519, agentData.EmployeeID);
            Assert.Equal("CO", agentData.State);
        }

        [Fact]
        public async Task GetLicenses_ReturnsStatusCodeResult_WithCorrectStatusCode()
        {
            // Arrange
            int employmentID = 606709;
            var expectedReturnResult = new ReturnResult
            {
                Success = true,
                StatusCode = 200,
                ObjData = new List<object>(), // Populate with expected data
                ErrMessage = ""
            };

            _agentService.Setup(service => service.GetLicenses(employmentID))
                .Returns(expectedReturnResult);

            var controller = new AgentController(_agentService.Object);

            // Act
            var result = await controller.GetLicenses(employmentID);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(expectedReturnResult.StatusCode, statusCodeResult.StatusCode);

            // Optionally, verify the content of the result
            var returnResult = Assert.IsType<ReturnResult>(statusCodeResult.Value);
            Assert.True(returnResult.Success);
            Assert.Equal(expectedReturnResult.ObjData, returnResult.ObjData);
            Assert.Empty(returnResult.ErrMessage);
        }

        [Fact]
        public async Task GetAppointments_ReturnsStatusCodeResult_WithCorrectStatusCode()
        {
            // Arrange
            int employmentID = 606709;
            var expectedReturnResult = new ReturnResult
            {
                Success = true,
                StatusCode = 200,
                ObjData = new List<object>(), // Populate with expected data
                ErrMessage = ""
            };

            _agentService.Setup(service => service.GetAppointments(employmentID))
                .Returns(expectedReturnResult);

            var controller = new AgentController(_agentService.Object);

            // Act
            var result = await controller.GetAppointments(employmentID);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(expectedReturnResult.StatusCode, statusCodeResult.StatusCode);

            // Optionally, verify the content of the result
            var returnResult = Assert.IsType<ReturnResult>(statusCodeResult.Value);
            Assert.True(returnResult.Success);
            Assert.Equal(expectedReturnResult.ObjData, returnResult.ObjData);
            Assert.Empty(returnResult.ErrMessage);
        }

        [Fact]
        public async Task GetEmploymentTransferHistory_ReturnsStatusCodeResult_WithExpectedStatusCode()
        {
            // Arrange
            var mockAgentService = new Mock<IAgentService>(); // Assuming IAgentService is the interface _agentService implements
            var controller = new AgentController(mockAgentService.Object); // Replace YourController with the actual name of your controller
            var input = new IputAgentEmploymentTransferHistory // Correct the spelling if necessary
            {
                EmploymentID = 606709, // Adjust these values based on your actual model
                EmploymentHistoryID = 0,
                TransferHistoryID = 0,
                EmploymentJobTitleID = 0
            };
            var expectedReturnResult = new ReturnResult // Adjust this to the actual type returned by GetEmploymentTransferHistory
            {
                StatusCode = 200, // Example status code, adjust as necessary
                                  // Populate with any other necessary properties based on your implementation
            };
            mockAgentService.Setup(s => s.GetEmploymentTransferHistory(input.EmploymentID, input.EmploymentHistoryID, input.TransferHistoryID, input.EmploymentJobTitleID))
                            .Returns(expectedReturnResult);

            // Act
            var result = await controller.GetEmploymentTransferHistory(input);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(expectedReturnResult.StatusCode, statusCodeResult.StatusCode);
            // Optionally, verify that the service method was called
            mockAgentService.Verify(s => s.GetEmploymentTransferHistory(input.EmploymentID, input.EmploymentHistoryID, input.TransferHistoryID, input.EmploymentJobTitleID), Times.Once);
        }

        [Fact]
        public async Task GetContEducationRequired_ReturnsStatusCodeResult_WithCorrectStatusCode()
        {
            // Arrange
            int employmentID = 606709;
            var expectedReturnResult = new ReturnResult
            {
                Success = true,
                StatusCode = 200,
                ObjData = new List<object>(), // Populate with expected data
                ErrMessage = ""
            };

            _agentService.Setup(service => service.GetContEducationRequired(employmentID))
                .Returns(expectedReturnResult);

            var controller = new AgentController(_agentService.Object);

            // Act
            var result = await controller.GetContEducationRequired(employmentID);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(expectedReturnResult.StatusCode, statusCodeResult.StatusCode);

            // Optionally, verify the content of the result
            var returnResult = Assert.IsType<ReturnResult>(statusCodeResult.Value);
            Assert.True(returnResult.Success);
            Assert.Equal(expectedReturnResult.ObjData, returnResult.ObjData);
            Assert.Empty(returnResult.ErrMessage);
        }

        [Fact]
        public async Task GetDiary_ReturnsStatusCodeResult_WithCorrectStatusCode()
        {
            // Arrange
            int employmentID = 606709;
            var expectedReturnResult = new ReturnResult
            {
                Success = true,
                StatusCode = 200,
                ObjData = new List<object>(), // Populate with expected data
                ErrMessage = ""
            };

            _agentService.Setup(service => service.GetDiary(employmentID))
                .Returns(expectedReturnResult);

            var controller = new AgentController(_agentService.Object);

            // Act
            var result = await controller.GetDiary(employmentID);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(expectedReturnResult.StatusCode, statusCodeResult.StatusCode);

            // Optionally, verify the content of the result
            var returnResult = Assert.IsType<ReturnResult>(statusCodeResult.Value);
            Assert.True(returnResult.Success);
            Assert.Equal(expectedReturnResult.ObjData, returnResult.ObjData);
            Assert.Empty(returnResult.ErrMessage);
        }

        [Fact]
        public async Task GetCommunications_ReturnsStatusCodeResult_WithCorrectStatusCode()
        {
            // Arrange
            int employmentID = 606709;
            var expectedReturnResult = new ReturnResult
            {
                Success = true,
                StatusCode = 200,
                ObjData = new List<object>(), // Populate with expected data
                ErrMessage = ""
            };

            _agentService.Setup(service => service.GetCommunications(employmentID))
                .Returns(expectedReturnResult);

            var controller = new AgentController(_agentService.Object);

            // Act
            var result = await controller.GetCommunications(employmentID);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(expectedReturnResult.StatusCode, statusCodeResult.StatusCode);

            // Optionally, verify the content of the result
            var returnResult = Assert.IsType<ReturnResult>(statusCodeResult.Value);
            Assert.True(returnResult.Success);
            Assert.Equal(expectedReturnResult.ObjData, returnResult.ObjData);
            Assert.Empty(returnResult.ErrMessage);
        }

        [Fact]
        public async Task InsertAgent_ReturnsStatusCodeResult_WithCorrectStatusCode()
        {
            var testDate = DateTime.Now;

            // Arrange
            var input = new IputAgentInsert // Correct the spelling if necessary
            {
              EmployeeSSN = "",
              NationalProducerNumber = 0,
              GEID = "",
              Alias = "",
              LastName = "EMFTstLastName",
              FirstName = "EMFTstFirstName",
              MiddleName = "",
              PreferredName = "EMFTstPrefered",
              DateOfBirth = testDate,
              SOEID = "",
              ExcludeFromRpts = false,
              EmployeeStatus = "",
              CompanyID = 0,
              WorkPhone = "",
              Email = "emftest@omf.com",
              LicenseIncentive = "",
              LicenseLevel = "",
              Address1 = "",
              Address2 = "",
              City = "",
              State = "",
              Zip = "",
              Phone = "",
              Fax = "",
              HireDate = testDate,
              UserSOEID = "",
              BackgroundCheckStatus = "",
              BackgroundCheckNote = "",
              BranchCode = "string",
              WorkStateAbv = "TX",
              ResStateAbv = "TX",
              JobTitleID = 0,
              JobTitleDate = testDate
            };
            var expectedReturnResult = new ReturnResult
            {
                Success = true,
                StatusCode = 200,
                ObjData = new List<object>(), // Populate with expected data
                ErrMessage = ""
            };

            _agentService.Setup(service => service.InsertAgent(input))
                .Returns(expectedReturnResult);

            var controller = new AgentController(_agentService.Object);

            // Act
            var result = await controller.InsertAgent(input);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(expectedReturnResult.StatusCode, statusCodeResult.StatusCode);

            // Optionally, verify the content of the result
            var returnResult = Assert.IsType<ReturnResult>(statusCodeResult.Value);
            Assert.True(returnResult.Success);
            Assert.Equal(expectedReturnResult.ObjData, returnResult.ObjData);
            Assert.Empty(returnResult.ErrMessage);
        }
    }
}
