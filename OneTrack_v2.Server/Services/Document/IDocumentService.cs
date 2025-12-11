using DataModel.Response;
using OneTrak_v2.Services.Employee.Model;

namespace OneTrak_v2.Services
{
    public interface IDocumentService
    {
        Task<ReturnResult> Upload(Stream vStream, string vFileName, string vFilePathType);
        Task<ReturnResult> CreateDocfinityOCR(IFormFile doc, EmployeeIndex employeeData, string timestamp, string vFilePathType = "ImportExportLoc");
        Task<ReturnResult> CreateDocfinityOCR(string emlFilePath, EmployeeIndex vEmployeeData, string subject, string communicationId, string userSOEID, string vFilePathType = "ImportExportLoc");
        //Task<ReturnResult> CreateDocfinityOCR(string emlFilePath, string subject, string communicationId, string userSOEID);
    }
}
