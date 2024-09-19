using Castle.Core.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Moq;
using OneTrack_v2.DbData.Models;
using OneTrack_v2.DataModel;
using OneTrack_v2.DbData;
using OneTrack_v2.Services;
using OneTrack_v2.Controllers;
using DataModel.Response;

namespace wcfOneTrak_API.Test
{
    public class AgentServiceTests
    {
        private readonly Mock<AppDataContext> _mockDb;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly Mock<IAgentService> _mockAgentService;
        private readonly Mock<IWebHostEnvironment> _mockEnv;
        private readonly AgentController _controller;

        //private readonly YourServiceClass _service;
        private readonly List<Diary> _diaries;
        private readonly List<LicenseTech> _licenseTechs;

        public AgentServiceTests()
        {
            _mockDb = new Mock<AppDataContext>();
            _mockConfig = new Mock<IConfiguration>();
            _mockEnv = new Mock<IWebHostEnvironment>();
            _mockAgentService = new Mock<IAgentService>();

            // Setup mock behavior and data here
            _diaries = new List<Diary>
            {
                // Add mock diaries here
            };

            _licenseTechs = new List<LicenseTech>
            {
                // Add mock licenseTechs here
            };

            //// Mock the Diaries DbSet
            //var mockDiariesDbSet = _diaries.AsQueryable().BuildMockDbSet();
            //_mockDb.Setup(db => db.Diaries).Returns(mockDiariesDbSet.Object);

            //// Mock the LicenseTeches DbSet
            //var mockLicenseTechsDbSet = _licenseTechs.AsQueryable().BuildMockDbSet();
            //_mockDb.Setup(db => db.LicenseTeches).Returns(mockLicenseTechsDbSet.Object);
            _controller = new AgentController(_mockAgentService.Object);
        }

        //[Fact]
        //public void GetAgentByEmployeeID_ReturnsCorrectAgent()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<AppDataContext>()
        //        .UseInMemoryDatabase(databaseName: "ByEmployeeIDDb") // Use a unique name for each test method
        //        .Options;

        //    // Seed the database
        //    using (var context = new AppDataContext(options))
        //    {
        //        context.Employees.Add(new Employee { EmployeeId = 1, /* other properties */ });
        //        context.Addresses.Add(new Address { AddressId = 1, /* other properties */ });
        //        context.EmployeeSsns.Add(new EmployeeSsn { EmployeeSsnid = 1, /* other properties */ });
        //        context.SaveChanges();
        //    }

        //    //var configMock = new Mock<IConfiguration>();
        //    //var envMock = new Mock<IWebHostEnvironment>();
        //    using (var context = new AppDataContext(options))
        //    {
        //        var service = new AgentService(context, null, null);

        //        // Act
        //        var result = service.GetAgentByEmployeeID(1);

        //        // Assert
        //        Assert.True(result.Success);
        //        Assert.NotNull(result.ObjData);
        //        var agent = result.ObjData as OputAgent; // Assuming this is the correct type
        //        Assert.Equal(1, agent.EmployeeID);
        //        // Additional assertions as necessary
        //    }
        //}

        //[Fact]
        //public void GetLicenses_ReturnsExpectedLicenses_ForEmploymentID()
        //{
        //    // Arrange
        //    var mockSetEmployeeLicenses = new Mock<DbSet<EmployeeLicense>>();
        //    var mockSetLicenses = new Mock<DbSet<License>>();
        //    var mockSetLineOfAuthorities = new Mock<DbSet<LineOfAuthority>>();

        //    var dataEmployeeLicenses = new List<EmployeeLicense>
        //    {
        //        new EmployeeLicense { /* Initialize with test data that will match the expected output */ },
        //        // Add other EmployeeLicense objects as needed
        //    }.AsQueryable();

        //    var dataLicenses = new List<License>
        //    {
        //        new License { /* Initialize with test data */ },
        //        // Add other License objects as needed
        //    }.AsQueryable();

        //    var dataLineOfAuthorities = new List<LineOfAuthority>
        //    {
        //        new LineOfAuthority { /* Initialize with test data */ },
        //        // Add other LineOfAuthority objects as needed
        //    }.AsQueryable();

        //    // Mock the EmployeeLicenses DbSet
        //    mockSetEmployeeLicenses.As<IQueryable<EmployeeLicense>>().Setup(m => m.Provider).Returns(dataEmployeeLicenses.Provider);
        //    mockSetEmployeeLicenses.As<IQueryable<EmployeeLicense>>().Setup(m => m.Expression).Returns(dataEmployeeLicenses.Expression);
        //    mockSetEmployeeLicenses.As<IQueryable<EmployeeLicense>>().Setup(m => m.ElementType).Returns(dataEmployeeLicenses.ElementType);
        //    mockSetEmployeeLicenses.As<IQueryable<EmployeeLicense>>().Setup(m => m.GetEnumerator()).Returns(dataEmployeeLicenses.GetEnumerator());

        //    // Mock the Licenses DbSet
        //    // Follow the same pattern as above for mockSetLicenses

        //    // Mock the LineOfAuthorities DbSet
        //    // Follow the same pattern as above for mockSetLineOfAuthorities

        //    var mockContext = new Mock<AppDataContext>();
        //    mockContext.Setup(c => c.EmployeeLicenses).Returns(mockSetEmployeeLicenses.Object);
        //    mockContext.Setup(c => c.Licenses).Returns(mockSetLicenses.Object);
        //    mockContext.Setup(c => c.LineOfAuthorities).Returns(mockSetLineOfAuthorities.Object);

        //    var service = new AgentService(mockContext.Object, null, null); // Adjust constructor as necessary

        //    // Act
        //    var result = service.GetLicenses(606709);

        //    // Assert
        //    Assert.True(result.Success);
        //    Assert.Equal(200, result.StatusCode);
        //    Assert.NotNull(result.ObjData);
        //    // Further assertions to validate the contents of result.ObjData against the expected results
        //}

        //[Fact]
        //public void GetAppointments_ReturnsExpectedResults()
        //{
        //    // Arrange
        //    var vEmploymentID = 606709;
        //    var dataEmployeeLicenses = new List<EmployeeLicense>
        //    {
        //        new EmployeeLicense { EmployeeLicenseId = 1176643, EmploymentId = 606709 }
        //        // Add more test data as needed
        //    }.AsQueryable();

        //    var dataEmployeeAppointments = new List<EmployeeAppointment>
        //    {
        //        new EmployeeAppointment
        //        {
        //            EmployeeAppointmentId = 294636,
        //            EmployeeLicenseId = 1176643,
        //            AppointmentEffectiveDate = new DateTime(2022, 9, 2),
        //            AppointmentStatus = "Active",
        //            // Initialize other properties as needed
        //        }
        //        // Add more test data as needed
        //    }.AsQueryable();

        //    var mockSetLicenses = new Mock<DbSet<EmployeeLicense>>();
        //    mockSetLicenses.As<IQueryable<EmployeeLicense>>().Setup(m => m.Provider).Returns(dataEmployeeLicenses.Provider);
        //    mockSetLicenses.As<IQueryable<EmployeeLicense>>().Setup(m => m.Expression).Returns(dataEmployeeLicenses.Expression);
        //    mockSetLicenses.As<IQueryable<EmployeeLicense>>().Setup(m => m.ElementType).Returns(dataEmployeeLicenses.ElementType);
        //    mockSetLicenses.As<IQueryable<EmployeeLicense>>().Setup(m => m.GetEnumerator()).Returns(dataEmployeeLicenses.GetEnumerator());

        //    var mockSetAppointments = new Mock<DbSet<EmployeeAppointment>>();
        //    mockSetAppointments.As<IQueryable<EmployeeAppointment>>().Setup(m => m.Provider).Returns(dataEmployeeAppointments.Provider);
        //    mockSetAppointments.As<IQueryable<EmployeeAppointment>>().Setup(m => m.Expression).Returns(dataEmployeeAppointments.Expression);
        //    mockSetAppointments.As<IQueryable<EmployeeAppointment>>().Setup(m => m.ElementType).Returns(dataEmployeeAppointments.ElementType);
        //    mockSetAppointments.As<IQueryable<EmployeeAppointment>>().Setup(m => m.GetEnumerator()).Returns(dataEmployeeAppointments.GetEnumerator());

        //    var mockContext = new Mock<AppDataContext>();
        //    mockContext.Setup(c => c.EmployeeLicenses).Returns(mockSetLicenses.Object);
        //    mockContext.Setup(c => c.EmployeeAppointments).Returns(mockSetAppointments.Object);

        //    var service = new AgentService(mockContext.Object, null, null); // Assuming your service is named YourService

        //    // Act
        //    var result = service.GetAppointments(vEmploymentID);

        //    // Assert
        //    Assert.True(result.Success);
        //    Assert.Equal(200, result.StatusCode);
        //    var appointments = Assert.IsType<List<OputAgentAppointments>>(result.ObjData);
        //    Assert.Single(appointments); // Assuming only one match based on the test data
        //    var appointment = appointments.First();
        //    Assert.Equal(294636, appointment.EmployeeAppointmentID);
        //    Assert.Equal("Active", appointment.AppointmentStatus);
        //    // Continue asserting other fields as necessary
        //}

        //[Fact]
        //public async Task GetEmploymentTransferHistory_ReturnsExpectedResult()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<AppDataContext>()
        //        .UseInMemoryDatabase(databaseName: "EmpTransHistDb") // Use a unique name for each test method
        //        .Options;

        //    // Populate the in-memory database
        //    using (var context = new AppDataContext(options))
        //    {
        //        context.EmploymentHistories.Add(new EmploymentHistory { /* Populate properties */ });
        //        context.TransferHistories.Add(new TransferHistory { /* Populate properties */ });
        //        context.Employments.Add(new Employment { /* Populate properties */ });
        //        context.Employees.Add(new Employee { /* Populate properties */ });
        //        context.Addresses.Add(new Address { /* Populate properties */ });
        //        context.EmploymentCompanyRequirements.Add(new EmploymentCompanyRequirement { /* Populate properties */ });
        //        context.EmploymentJobTitles.Add(new EmploymentJobTitle { /* Populate properties */ });
        //        context.JobTitles.Add(new JobTitle { /* Populate properties */ });
        //        await context.SaveChangesAsync();
        //    }

        //    // Use a clean instance of the context to run the test
        //    using (var context = new AppDataContext(options))
        //    {
        //        var service = new AgentService(context, null, null); // Assuming GetEmploymentTransferHistory is in YourService

        //        // Act
        //        var result = service.GetEmploymentTransferHistory(vEmploymentID: 1 /*, other parameters as needed */);

        //        // Assert
        //        Assert.True(result.Success);
        //        Assert.NotNull(result.ObjData);
        //        Assert.Equal(200, result.StatusCode);
        //        // Add more assertions here to validate the contents of result.ObjData
        //    }
        //}

        //[Fact]
        //public void GetContEducationRequired_ReturnsExpectedResult_WithValidEmploymentID()
        //{
        //    // Arrange
        //    int validEmploymentID = 1; // Example EmploymentID

        //    // Mocking the data for ContEducationRequirements and Employments
        //    var contEducationRequirementsData = new List<ContEducationRequirement>
        //{
        //    new ContEducationRequirement
        //    {
        //        // Initialize with properties relevant to the test
        //        EmploymentId = validEmploymentID,
        //        IsExempt = false,
        //        // other properties as needed
        //    },
        //    // Add more objects as needed for testing
        //}.AsQueryable();

        //    var employmentsData = new List<Employment>
        //{
        //    new Employment
        //    {
        //        EmploymentId = validEmploymentID,
        //        Cerequired = true,
        //        // other properties
        //    },
        //    // Add more objects as needed for testing
        //}.AsQueryable();

        //    var mockSetContEdReq = MockDbSetHelper.CreateMockSet(contEducationRequirementsData);
        //    var mockSetEmployments = MockDbSetHelper.CreateMockSet(employmentsData);

        //    var mockContext = new Mock<AppDataContext>();
        //    mockContext.Setup(db => db.ContEducationRequirements).Returns(mockSetContEdReq.Object);
        //    mockContext.Setup(db => db.Employments).Returns(mockSetEmployments.Object);

        //    var service = new AgentService(mockContext.Object, null, null);

        //    // Act
        //    var result = service.GetContEducationRequired(validEmploymentID);

        //    // Assert
        //    Assert.True(result.Success);
        //    Assert.Equal(200, result.StatusCode);
        //    Assert.NotNull(result.ObjData);
        //    // Additional assertions to verify the correctness of ObjData based on your mock data
        //}

        ////[Fact]
        ////public void GetContEducationRequired_HandlesException_WithInvalidData()
        ////{
        ////    // Arrange
        ////    int invalidEmploymentID = -1; // Example EmploymentID that leads to an exception

        ////    // Setup your context to throw an exception when the method is called
        ////    // This could be through setting up the mock to throw when accessed or by manipulating the test data to cause an exception
        ////    var mockContext = new Mock<AppDataContext>();
        ////    mockContext.Setup(db => db.ContEducationRequirements).Throws(new Exception("Test Exception"));

        ////    var service = new AgentService(mockContext.Objectt);

        ////    // Act
        ////    var result = service.GetContEducationRequired(invalidEmploymentID);

        ////    // Assert
        ////    Assert.False(result.Success);
        ////    Assert.Equal(500, result.StatusCode);
        ////    Assert.Equal("Test Exception", result.ErrMessage);
        ////}

        //// Helper method to mock DbSet<T>
        //private static Mock<DbSet<T>> CreateMockSet<T>(IQueryable<T> data) where T : class
        //{
        //    var mockSet = new Mock<DbSet<T>>();
        //    mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        //    mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        //    mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        //    mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
        //    return mockSet;
        //}

        //[Fact]
        //public void GetDiary_WithValidEmploymentID_ReturnsCorrectDiaries()
        //{
        //    //// Arrange
        //    //var controller = new AgentController(_mockDb.Object); // Replace YourController with the actual controller that contains GetDiary
        //    //int testEmploymentId = 606709; // Example employment ID

        //    //// Act
        //    //var result = controller.GetDiary(testEmploymentId);

        //    //// Assert
        //    //Assert.True(result.Success);
        //    //Assert.Equal(200, result.StatusCode);
        //    //Assert.NotNull(result.ObjData);
        //    //// More assertions to verify the content of ObjData based on your mock data
        //}

        //[Fact]
        //public void GetDiary_WithInvalidEmploymentID_ReturnsEmptyResult()
        //{
        //    //// Arrange
        //    //var controller = new YourController(_mockDb.Object);
        //    //int testEmploymentId = -1; // Example of an employment ID that does not exist

        //    //// Act
        //    //var result = controller.GetDiary(testEmploymentId);

        //    //// Assert
        //    //Assert.True(result.Success);
        //    //Assert.Equal(200, result.StatusCode);
        //    //Assert.Empty(result.ObjData);
        //}

        //[Fact]
        //public void GetDiary_ThrowsException_ReturnsStatusCode500()
        //{
        //    //// Arrange
        //    //_mockDb.Setup(db => db.Diaries).Throws(new Exception("Database error"));
        //    //var controller = new YourController(_mockDb.Object);

        //    //// Act
        //    //var result = controller.GetDiary(0);

        //    //// Assert
        //    //Assert.False(result.Success);
        //    //Assert.Equal(500, result.StatusCode);
        //    //Assert.Equal("Database error", result.ErrMessage);
        //}

        //[Fact]
        //public void InsertAgent_v2_ReturnsSuccess_WhenDataIsValid()
        //{
        //    var testDate = DateTime.Now;

        //    // Arrange
        //    var input = new IputAgentInsert
        //    {
        //        EmployeeSSN = "",
        //        NationalProducerNumber = 0,
        //        GEID = "",
        //        Alias = "",
        //        LastName = "EMFTstLastName",
        //        FirstName = "EMFTstFirstName",
        //        MiddleName = "",
        //        PreferredName = "EMFTstPrefered",
        //        DateOfBirth = testDate,
        //        SOEID = "",
        //        ExcludeFromRpts = false,
        //        EmployeeStatus = "",
        //        CompanyID = 0,
        //        WorkPhone = "",
        //        Email = "emftest@omf.com",
        //        LicenseIncentive = "",
        //        LicenseLevel = "",
        //        Address1 = "",
        //        Address2 = "",
        //        City = "",
        //        State = "",
        //        Zip = "",
        //        Phone = "",
        //        Fax = "",
        //        HireDate = testDate,
        //        UserSOEID = "",
        //        BackgroundCheckStatus = "",
        //        BackgroundCheckNote = "",
        //        BranchCode = "string",
        //        WorkStateAbv = "TX",
        //        ResStateAbv = "TX",
        //        JobTitleID = 0,
        //        JobTitleDate = testDate
        //    };

        //    // Mock database sets and any method calls as necessary
        //    // For example, if using EF Core, mock DbSet and any LINQ operations
        //    _mockDb.Setup(ctx => ctx.Employees.Add(It.IsAny<Employee>()));
        //    _mockDb.Setup(ctx => ctx.SaveChanges()).Returns(1); // Simulate successful save

        //    // Act
        //    var result = _controller.InsertAgent_v2(input);

        //    // Assert
        //    Assert.NotNull(result);
        //    var returnResult = Assert.IsType<ReturnResult>(result);
        //    Assert.True(returnResult.Success);

        //    // Additional assertions as necessary, e.g., verify that methods were called on the mocks
        //    _mockDb.Verify(ctx => ctx.SaveChanges(), Times.AtLeastOnce());
        //}
    }
}
