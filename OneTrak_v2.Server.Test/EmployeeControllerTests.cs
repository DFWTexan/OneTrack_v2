using Microsoft.AspNetCore.Mvc;
using Moq;
using OneTrack_v2.DataModel.StoredProcedures;
using OneTrack_v2.Controllers;
using OneTrack_v2.Services;
using DataModel.Response;

namespace wcfOneTrak_API.Test
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _employeeService;

        public EmployeeControllerTests()
        {
            _employeeService = new Mock<IEmployeeService>();
        }

        [Theory]
        [InlineData(1, null, null, null, 0, null, null, null, null, null, null, 0, null, null, null, 0, "CompanyID")]
        [InlineData(0, "123-45-6789", null, null, 0, null, null, null, null, null, null, 0, null, null, null, 0, "EmployeeSSN")]
        [InlineData(0, null, "3303627", null, 0, null, null, null, null, null, null, 0, null, null, null, 0, "GEID")]
        [InlineData(0, null, null, "4356", 0, null, null, null, null, null, null, 0, null, null, null, 0, "SCORENumber")]
        [InlineData(0, null, null, null, 123456, null, null, null, null, null, null, 0, null, null, null, 0, "NationalProducerNumber")]
        [InlineData(0, null, null, null, 0, "SMITH", null, null, null, null, null, 0, null, null, null, 0, "LastName")]
        [InlineData(0, null, null, null, 0, null, "AASSUIMR", null, null, null, null, 0, null, null, null, 0, "FirstName")]
        [InlineData(0, null, null, null, 0, null, null, "Active", null, null, null, 0, null, null, null, 0, "AgentStatus")]
        [InlineData(0, null, null, null, 0, null, null, null, "TX", null, null, 0, null, null, null, 0, "ResState")]
        [InlineData(0, null, null, null, 0, null, null, null, null, "TX", null, 0, null, null, null, 0, "WrkState")]
        [InlineData(0, null, null, null, 0, null, null, null, null, null, "MACHESNEY PARK, IL", 0, null, null, null, 0, "BranchCode")]
        [InlineData(0, null, null, null, 0, null, null, null, null, null, null, 123456, null, null, null, 0, "EmployeeLicenseID")]
        [InlineData(0, null, null, null, 0, null, null, null, null, null, null, 0, "Active", null, null, 0, "LicStatus")]
        [InlineData(0, null, null, null, 0, null, null, null, null, null, null, 0, null, "TX", null, 0, "LicState")]
        [InlineData(0, null, null, null, 0, null, null, null, null, null, null, 0, null, null, "Insurance", 0, "LicenseName")]
        [InlineData(0, null, null, null, 0, null, null, null, null, null, null, 0, null, null, null, 49282, "EmploymentID")]
        public async Task SearchEmployee_ParameterizedTest_ReturnsValidData(
        int vCompanyID, string vEmployeeSSN, string vGEID, string vSCORENumber,
        int vNationalProducerNumber, string vLastName, string vFirstName,
        string vAgentStatus, string vResState, string vWrkState, string vBranchCode,
        int vEmployeeLicenseID, string vLicStatus, string vLicState, string vLicenseName, int vEmploymentID, string testedParameter)
        {
            // Example of a valid return result from the service
            //"employeeID": 49282,
            //"geid": "3303627",
            //"name": "SMITH, AASSUIMR ",
            //"resStateAbv": "TX",
            //"workStateAbv": "TX",
            //"scoreNumber": "4356",
            //"branchName": "MACHESNEY PARK, IL",
            //"employmentID": 49403,
            //"state": "  "

            // Arrange
            var expectedServiceResult = new ReturnResult
            {
                Success = true,
                StatusCode = 200,
                ObjData = new List<SPOUT_uspEmployeeGridSearchNew>
                {
                    new SPOUT_uspEmployeeGridSearchNew
                    {
                        EmployeeID = 49282,
                        GEID = "3303627",
                        Name = "SMITH, AASSUIMR",
                        ResStateAbv = "TX",
                        WorkStateAbv = "TX",
                        ScoreNumber = "4356",
                        BranchName = "MACHESNEY PARK, IL",
                        EmploymentID = 49403,
                        State = "  "
                    }
                }
            };

            _employeeService.Setup(x => x.SearchEmployee(
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(expectedServiceResult); // Ensure this matches the service's actual return type

            var controller = new EmployeeController(_employeeService.Object);

            // Act
            var actionResult = await controller.SearchEmployee(0, null, null, null, 0, "Smith", null, null, null, null, null, 0, null, null, null, 0);

            // Assert
            var result = Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);

            var returnResult = Assert.IsType<ReturnResult>(result.Value);
            Assert.True(returnResult.Success);
            Assert.Equal(200, returnResult.StatusCode);
            Assert.NotEmpty((IEnumerable<SPOUT_uspEmployeeGridSearchNew>)returnResult.ObjData);
        }
    }
}
