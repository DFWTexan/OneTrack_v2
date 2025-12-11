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
        private readonly string? _importExportLoc;

        public DocumentService(IConfiguration config, AppDataContext db, IUtilityHelpService utilityHelpService)
        {
            _config = config;
            _db = db;
            _utilityService = utilityHelpService;

            // Retrieve the current environment setting
            string environment = _config.GetValue<string>("Environment") ?? "DVLP";

            // Construct the keys for accessing environment-specific settings
            string attachmentLocKey = $"EnvironmentSettings:{environment}:Paths:AttachmentLoc";
            string importExportKey = $"EnvironmentSettings:{environment}:Paths:ImportExportLoc";

            // Retrieve the values based on the constructed keys
            _attachmentLocation = _config.GetValue<string>(attachmentLocKey);
            _importExportLoc = _config.GetValue<string>(importExportKey);
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
                string ocrContent = await BuildOcrContent(employeeData, finalFilePath);

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
        /// Creates OCR file for existing EML file (Docfinity integration)
        /// </summary>
        public async Task<ReturnResult> CreateDocfinityOCR(string emlFilePath, EmployeeIndex vEmployeeData, string subject, string communicationId, string userSOEID, string vFilePathType = "ImportExportLoc")
        {
            try
            {
                // Get timestamp from the EML filename or generate new one
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                // Create minimal EmployeeIndex object for OCR content
                //var employeeData = new EmployeeIndex
                //{
                //    DocumentType = "EMAIL",
                //    DocumentSubType = "COMMUNICATION",
                //    FirstName = "EMAIL",
                //    LastName = "COMMUNICATION",
                //    // Set other required fields with defaults or extract from email data
                //};

                // Create OCR file using existing logic
                string ocrContent = await BuildOcrContent(vEmployeeData, emlFilePath);

                //string ocrFilePath = Path.Combine(
                //    Path.GetDirectoryName(emlFilePath),
                //    Path.GetFileNameWithoutExtension(emlFilePath) + ".OCR"
                //);
                String ocrFilePath = Path.Combine(_importExportLoc, timestamp + ".OCR");

                await File.WriteAllTextAsync(ocrFilePath, ocrContent);

                return new ReturnResult
                {
                    StatusCode = 200,
                    Success = true,
                    ObjData = new
                    {
                        Message = "Email OCR created successfully",
                        OcrPath = ocrFilePath
                    }
                };
            }
            catch (Exception ex)
            {
                _utilityService.LogError(ex.Message, "CreateDocfinityOCR Email Error", new { }, userSOEID);
                return new ReturnResult
                {
                    StatusCode = 500,
                    Success = false,
                    ErrMessage = "Failed to create email OCR file"
                };
            }
        }
        /// <summary>
        /// Builds OCR content string from employee data
        /// </summary>
        /// <param name="employeeData">Employee index data</param>
        /// <param name="finalFilePath">Path to the uploaded document</param>
        /// <returns>Tab-delimited OCR content string</returns>
        private async Task<string> BuildOcrContent(EmployeeIndex vEmployeeData, string vEmailFilePath)
        {
            DateTime dtDateTime = DateTime.Now;

            // Verify source file exists
            if (!File.Exists(vEmailFilePath))
            {
                throw new FileNotFoundException($"Source file not found: {vEmailFilePath}");
            }

            // Process the file - use EXACT same approach as DocumentService.Upload
            var fileName = Path.GetFileName(vEmailFilePath);
            var timestamp = dtDateTime.ToString("yyyyMMddHHmmss");

            // Create the timestamped filename for the final destination
            var timestampedFileName = timestamp + fileName;
            var finalFilePath = Path.Combine(_importExportLoc, timestampedFileName);

            // Ensure directory exists (same as DocumentService)
            if (!Directory.Exists(_importExportLoc))
            {
                Directory.CreateDirectory(_importExportLoc);
            }

            // Move the file from vEmailFilePath to _importExportLoc with timestamped name
            try
            {
                // Copy the file to the new location
                File.Copy(vEmailFilePath, finalFilePath, overwrite: true);

                // Optionally delete the original file after successful copy
                // File.Delete(vEmailFilePath);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to move file from {vEmailFilePath} to {finalFilePath}: {ex.Message}", ex);
            }

            string strNow = dtDateTime.ToString("yyyy-MM-dd");
            string strTMFirstName = vEmployeeData.FirstName?.ToUpper() ?? string.Empty;
            string strTMLastName = vEmployeeData.LastName?.ToUpper() ?? string.Empty;

            string strBranchNumber = vEmployeeData.BranchName ?? "00000000";
            string strScoreNumber = vEmployeeData.ScoreNumber ?? "0000";
            string strDocType = vEmployeeData.DocumentType?.ToUpper() ?? string.Empty;
            string strDocSubType = vEmployeeData.DocumentSubType?.ToUpper() ?? string.Empty;
            string strEmployeeID = vEmployeeData.EmployeeID?.ToString() ?? string.Empty;
            string strWorkState = vEmployeeData.WorkState ?? string.Empty;
            string strTMNumber = vEmployeeData.Geid ?? string.Empty;
            string strLicState = (vEmployeeData.LicenseState != "{StateProv}") ? vEmployeeData.LicenseState ?? vEmployeeData.WorkState : string.Empty;

            // Build OCR content (Tab = \t) using final file path
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

        //private string BuildOcrContentForEmail(string emlFilePath, EmployeeIndex vEmployeeData, string communicationId, string userSOEID)
        //{
        //    DateTime dtDateTime = DateTime.Now;

        //    // Build OCR content for email (Tab = \t)
        //    string ocrContent = dtDateTime.ToString("yyyy-MM-dd") + "\t" +
        //                       emlFilePath + "\t" +
        //                       vEmployeeData.FirstName + "\t" +  // FirstName
        //                       vEmployeeData.LastName + "\t" +  // LastName
        //                       vEmployeeData.BranchName + "\t" +  // BranchNumber
        //                       vEmployeeData.ScoreNumber + "\t" +  // ScoreNumber
        //                       vEmployeeData.DocumentType + "\t" +  // DocumentType
        //                       vEmployeeData.DocumentSubType + "\t" +  // DocumentSubType
        //                       communicationId + "\t" +  // EmployeeID (using communicationId)
        //                       "" + "\t" +  // WorkState
        //                       userSOEID + "\t" +  // TMNumber (using userSOEID)
        //                       "";  // LicenseState

        //    return ocrContent;
        //}

    }
}
