using DataModel.Response;
using OneTrack_v2.DbData;
using OneTrack_v2.Services;
using Microsoft.AspNetCore.Http;
using OneTrak_v2.Services.Employee.Model;

namespace OneTrak_v2.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IConfiguration _config;
        private readonly AppDataContext _db;
        private readonly IUtilityHelpService _utilityService;

        private readonly string? _attachmentLocation;

        public DocumentService(IConfiguration config, AppDataContext db, IUtilityHelpService utilityHelpService)
        {
            _config = config;
            _db = db;
            _utilityService = utilityHelpService;

            // Retrieve the current environment setting
            string environment = _config.GetValue<string>("Environment") ?? "DVLP";

            // Construct the keys for accessing environment-specific settings
            string attachmentLocKey = $"EnvironmentSettings:{environment}:Paths:AttachmentLoc";

            // Retrieve the values based on the constructed keys
            _attachmentLocation = _config.GetValue<string>(attachmentLocKey);
        }

        public async Task<ReturnResult> Upload(Stream vStream, string vFileName, string vFilePathType)
        {
            if (vStream == null)
            {
                _utilityService.LogError("vStream is NULL.", "Server Error - Please Contact Support [REF# DOCS-3569-92418].", new { }, null);
                return new ReturnResult
                {
                    StatusCode = 400,
                    Success = false
                };
            }

            try
            {
                // Retrieve the current environment setting
                string environment = _config.GetValue<string>("Environment") ?? "DVLP";

                // Construct the keys for accessing environment-specific settings
                string fileDestinationPath = $"EnvironmentSettings:{environment}:Paths:{vFilePathType}";

                // Retrieve the values based on the constructed keys
                string? fileDestinationLocPath = _config.GetValue<string>(fileDestinationPath);

                if (!Directory.Exists(fileDestinationLocPath))
                {
                    Directory.CreateDirectory(fileDestinationLocPath);
                }

                var fullPath = Path.Combine(fileDestinationLocPath, vFileName);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await vStream.CopyToAsync(fileStream);
                }

                return new ReturnResult
                {
                    StatusCode = 200,
                    Success = true,
                    ObjData = new { Message = "File Upload Successful", FullPath = fullPath }
                };
            }
            catch (Exception ex)
            {
                _utilityService.LogError(ex.Message, "Server Error - Please Contact Support [REF# DOCS-3509-93313].", new { }, null);
                return new ReturnResult
                {
                    StatusCode = 500,
                    Success = false,
                    ErrMessage = "Server Error - Please Contact Support [REF# DOCS-3509-93313]."
                };
            }
        }

        /// <summary>
        /// Creates a document and OCR file pair for Docfinity processing
        /// </summary>
        /// <param name="doc">The uploaded file</param>
        /// <param name="employeeData">Employee index data for OCR content</param>
        /// <param name="timestamp">Timestamp for file naming</param>
        /// <param name="vFilePathType">Configuration key for destination path (e.g., "ImportExportLoc")</param>
        /// <returns>ReturnResult with file paths</returns>
        public async Task<ReturnResult> CreateDocfinityOCR(IFormFile doc, EmployeeIndex employeeData, string timestamp, string vFilePathType = "ImportExportLoc")
        {
            if (doc == null || doc.Length == 0)
            {
                _utilityService.LogError("Document file is null or empty.", "Server Error - Please Contact Support [REF# DOCS-3570-92419].", new { }, null);
                return new ReturnResult
                {
                    StatusCode = 400,
                    Success = false,
                    ErrMessage = "Document file is null or empty."
                };
            }

            if (employeeData == null)
            {
                _utilityService.LogError("Employee data is null.", "Server Error - Please Contact Support [REF# DOCS-3570-92420].", new { }, null);
                return new ReturnResult
                {
                    StatusCode = 400,
                    Success = false,
                    ErrMessage = "Employee data is required."
                };
            }

            try
            {
                // Retrieve the current environment setting
                string environment = _config.GetValue<string>("Environment") ?? "DVLP";

                // Construct the keys for accessing environment-specific settings
                string fileDestinationPath = $"EnvironmentSettings:{environment}:Paths:{vFilePathType}";

                // Retrieve the values based on the constructed keys
                string? importExportLoc = _config.GetValue<string>(fileDestinationPath);

                if (string.IsNullOrWhiteSpace(importExportLoc))
                {
                    _utilityService.LogError($"Configuration path not found: {fileDestinationPath}", "Server Error - Please Contact Support [REF# DOCS-3570-92421].", new { }, null);
                    return new ReturnResult
                    {
                        StatusCode = 500,
                        Success = false,
                        ErrMessage = "Import/Export location not configured."
                    };
                }

                // Process the file - use EXACT same approach as DocumentService.Upload
                var fileName = Path.GetFileName(doc.FileName);
                var finalFileName = timestamp + fileName;
                var finalFilePath = Path.Combine(importExportLoc, finalFileName);

                // Ensure directory exists (same as DocumentService)
                if (!Directory.Exists(importExportLoc))
                {
                    Directory.CreateDirectory(importExportLoc);
                }

                // Use EXACT same pattern as DocumentService.Upload
                using (var inputStream = doc.OpenReadStream())
                using (var fileStream = new FileStream(finalFilePath, FileMode.Create))
                {
                    await inputStream.CopyToAsync(fileStream);
                }

                // Create the .OCR file
                string ocrFilePath = Path.Combine(importExportLoc, timestamp + ".OCR");

                // Build OCR content
                string ocrContent = BuildOcrContent(employeeData, finalFilePath);

                // Write OCR file
                await File.WriteAllTextAsync(ocrFilePath, ocrContent);

                // Log successful creation
                //_utilityService.LogInfo($"Docfinity OCR created successfully. Document: {finalFilePath}, OCR: {ocrFilePath}", new { 
                //    DocumentPath = finalFilePath, 
                //    OcrPath = ocrFilePath, 
                //    EmployeeID = employeeData.EmployeeID,
                //    DocumentType = employeeData.DocumentType 
                //});

                return new ReturnResult
                {
                    StatusCode = 200,
                    Success = true,
                    ObjData = new { 
                        Message = "Docfinity OCR created successfully",
                        DocumentPath = finalFilePath,
                        OcrPath = ocrFilePath
                    }
                };
            }
            catch (Exception ex)
            {
                _utilityService.LogError(ex.Message, "Server Error - Please Contact Support [REF# DOCS-3570-93314].", new { 
                    FileName = doc?.FileName,
                    EmployeeID = employeeData?.EmployeeID,
                    Timestamp = timestamp,
                    StackTrace = ex.StackTrace 
                }, null);

                return new ReturnResult
                {
                    StatusCode = 500,
                    Success = false,
                    ErrMessage = "Server Error - Please Contact Support [REF# DOCS-3570-93314]."
                };
            }
        }

        /// <summary>
        /// Builds OCR content string from employee data
        /// </summary>
        /// <param name="employeeData">Employee index data</param>
        /// <param name="finalFilePath">Path to the uploaded document</param>
        /// <returns>Tab-delimited OCR content string</returns>
        private string BuildOcrContent(EmployeeIndex employeeData, string finalFilePath)
        {
            DateTime dtDateTime = DateTime.Now;

            string strNow = dtDateTime.ToString("yyyy-MM-dd");
            string strTMFirstName = employeeData.FirstName?.ToUpper() ?? string.Empty;
            string strTMLastName = employeeData.LastName?.ToUpper() ?? string.Empty;

            string strBranchNumber = employeeData.BranchName ?? "00000000";
            string strScoreNumber = employeeData.ScoreNumber ?? "0000";
            string strDocType = employeeData.DocumentType?.ToUpper() ?? string.Empty;
            string strDocSubType = employeeData.DocumentSubType?.ToUpper() ?? string.Empty;
            string strEmployeeID = employeeData.EmployeeID?.ToString() ?? string.Empty;
            string strWorkState = employeeData.WorkState ?? string.Empty;
            string strTMNumber = employeeData.Geid ?? string.Empty;
            string strLicState = (employeeData.LicenseState != "{StateProv}") ? employeeData.LicenseState ?? string.Empty : string.Empty;

            // Build OCR content (Tab = \t)
            string ocrContent = strNow + "\t" +
                               finalFilePath + "\t" +
                               strTMFirstName + "\t" +
                               strTMLastName + "\t" +
                               strBranchNumber + "\t" +
                               strScoreNumber + "\t" +
                               strDocType + "\t" +
                               strDocSubType + "\t" +
                               strEmployeeID + "\t" +
                               strWorkState + "\t" +
                               strTMNumber + "\t" +
                               strLicState;

            return ocrContent;
        }
    }
}
