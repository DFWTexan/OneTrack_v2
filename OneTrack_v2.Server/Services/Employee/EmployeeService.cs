using Microsoft.EntityFrameworkCore;
using System.Data;
using OneTrack_v2.DbData;
using OneTrack_v2.DataModel.StoredProcedures;
using DataModel.Response;
using OneTrak_v2.DataModel;
using System;
using NuGet.Packaging;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using OneTrak_v2.Services.Employee.Model;
using System.IO;

namespace OneTrack_v2.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration _config;
        private readonly AppDataContext _db;
        private readonly IUtilityHelpService _utilityService;

        private readonly string? _importExportLoc;
        //private readonly UtilityService.Utility _utility;

        //private readonly IConfiguration _config;
        //private readonly IWebHostEnvironment _env;
        //private readonly ILogger _logger;

        public EmployeeService(IConfiguration config, AppDataContext db, IUtilityHelpService utilityHelpService)
        {
            _db = db;
            _config = config;
            _utilityService = utilityHelpService;
            // Retrieve the current environment setting
            string environment = _config.GetValue<string>("Environment") ?? "DVLP";

            // Construct the keys for accessing environment-specific settings
            string importExportKey = $"EnvironmentSettings:{environment}:Paths:ImportExportLoc";

            // Retrieve the value of the key from the configuration
            _importExportLoc = _config.GetValue<string>(importExportKey);

        }

        /// <summary>
        ///
        /// STORED PROCEDURE:  uspEmployeeGridSearchNew
        ///
        /// </summary>
        /// <param name="@vchCompanyID" int></param>
        /// <param name="@vchEmployeeSSN" varchar(25)></param>
        /// <param name="@vchGEID" varchar(25)></param>
        /// <param name="@vchSCORENumber" varchar(25)></param>
        /// <param name="@vchNationalProducerNumber" int></param>
        /// <param name="@vchLastName" varchar(50)></param>
        /// <param name="@vchFirstName" varchar(50)></param>
        /// <param name="@vchAgentStatus" varchar(800)></param>
        /// <param name="@vchResState" varchar(10)></param>
        /// <param name="@vchWrkState" varchar(10)></param>
        /// <param name="@vchBranchCode" varchar(25)></param>
        /// <param name="@vchEmployeeLicenseID" int></param>
        /// <param name="@vchLicStatus" varchar(800)></param>
        /// <param name="@vchLicState" varchar(10)></param>
        /// <param name="@vchLicenseName" varchar(25)></param>
        /// <param name="@vchEmploymentID" int></param>
        /// <returns>
        ///     List of Employees matching the search criteria
        /// 
        ///     {
        ///     // Example of a valid return result from the service
        ///     new SPOUT_uspEmployeeGridSearchNew {
        ///         {
        ///            EmployeeID = 49282,
        ///            GEID = "3303627",
        ///            Name = "SMITH, AASSUIMR",
        ///            ResStateAbv = "TX",
        ///            WorkStateAbv = "TX",
        ///            ScoreNumber = "4356",
        ///            BranchName = "MACHESNEY PARK, IL",
        ///            EmploymentID = 49403,
        ///            State = "  "
        ///        }
        /// </returns>
        public Task<ReturnResult> SearchEmployee(string? vEmployeeSSN = null, string? vGEID = null, string? vSCORENumber = null, int? vCompanyID = 0,
            string? vLastName = null, string? vFirstName = null, List<string>? vAgentStatus = null, string? vResState = null, string? vWrkState = null, string? vBranchCode = null,
            int? vEmployeeLicenseID = 0, List<string>? vLicStatus = null, string? vLicState = null, string? vLicenseName = null, int? vNationalProducerNumber = 0)
        {

            var result = new ReturnResult();
            try
            {
                var sql = @"
                       SELECT  
                             e.EmployeeID,
	                         e.GEID,
	                         CONCAT(e.LastName,', ',e.FirstName,' ', LEFT(e.MiddleName,1)) AS Name,
                             th.ResStateAbv,
                             th.WorkStateAbv,
	                         ISNULL(bdh.ScoreNumber,'0000') AS ScoreNumber,
                             ISNULL(bdh.Name,'UNKNOWN') AS BranchName,
                             m.EmploymentID,
	                         a.[State],
                             e.FirstName,
                             e.LastName
                        FROM dbo.EmployeeSSN ss
                        RIGHT OUTER JOIN dbo.Employee e ON ss.EmployeeSSNID = e.EmployeeSSNID
                        INNER JOIN dbo.Employment m ON e.EmployeeID = m.EmployeeID
                        LEFT OUTER JOIN dbo.Address a ON e.AddressID = a.AddressID
                        LEFT OUTER JOIN (SELECT 
					                        EmploymentID, MAX(TransferHistoryID) AS 'TransferHistoryID'
				                        FROM TransferHistory 
				                        GROUP BY EmploymentID) T ON m.EmploymentID = T.EmploymentID
                        LEFT OUTER JOIN dbo.TransferHistory th ON th.TransferHistoryID = T.TransferHistoryID
                        LEFT OUTER JOIN dbo.EmployeeLicense el ON el.EmploymentID = T.EmploymentID
                        LEFT OUTER JOIN [dbo].[BIF] bdh ON RIGHT(bdh.[HR_Department_ID],8) = RIGHT(th.BranchCode,8)
                        LEFT OUTER JOIN dbo.License l ON el.LicenseID = l.LicenseID
                        LEFT OUTER JOIN [dbo].[LineOfAuthority] la ON l.LineOfAuthorityID = la.LineOfAuthorityID
                        LEFT OUTER JOIN dbo.Company c ON m.CompanyID = c.CompanyID
                        INNER JOIN [dbo].[fn_String_To_Table]( @vchAgentStatus, ',',1) rs ON @vchAgentStatus = 'ALL' OR m.EmployeeStatus = rs.[Val] 
                        INNER JOIN [dbo].[fn_String_To_Table]( @vchLicStatus, ',',1) rs1 ON @vchLicStatus = 'ALL' OR ISNULL(el.LicenseStatus,'NoLicense') = rs1.[Val] 
                        WHERE (@vchCompanyID IS NULL OR m.CompanyID = @vchCompanyID) 
	                        AND (@vchEmployeeSSN IS NULL OR ss.EmployeeSSN = @vchEmployeeSSN) 
	                        AND (@vchGEID IS NULL OR e.GEID=@vchGEID) 
	                        AND (@vchNationalProducerNumber IS NULL OR e.NationalProducerNumber = @vchNationalProducerNumber) 
	                        AND (@vchLastName IS NULL OR e.LastName LIKE @vchLastName + '%') 
	                        AND (@vchFirstName IS NULL OR e.FirstName LIKE @vchFirstName + '%') 
	                        AND (@vchEmploymentID IS NULL OR m.EmploymentID = @vchEmploymentID) 
	                        AND (@vchResState IS NULL OR th.ResStateAbv = @vchResState) 
	                        AND (@vchWrkState IS NULL OR th.WorkStateAbv = @vchWrkState) 
	                        AND (@vchBranchCode IS NULL OR th.BranchCode = @vchBranchCode) 
	                        AND (@vchSCORENumber IS NULL OR th.SCORENumber = @vchSCORENumber) 
	                        AND (@vchEmployeeLicenseID IS NULL OR el.EmployeeLicenseID = @vchEmployeeLicenseID) 
	                        AND (@vchLicState IS NULL OR l.StateProvinceAbv = @vchLicState) 
	                        AND (@vchLicenseName IS NULL OR l.LicenseName = @vchLicenseName)
                        GROUP BY  
                             e.EmployeeID,
	                         e.GEID,
	                         CONCAT(e.LastName,', ',e.FirstName,' ', LEFT(e.MiddleName,1)),
                             th.ResStateAbv,
                             th.WorkStateAbv,
	                         ISNULL(bdh.ScoreNumber,'0000') ,
                             ISNULL(bdh.Name,'UNKNOWN'),
                             m.EmploymentID,
	                         a.[State],
                             e.FirstName,
                             e.LastName
                        ORDER BY  
                             CONCAT(e.LastName,', ',e.FirstName,' ', LEFT(e.MiddleName,1)) ";

                // Convert the lists to comma separated strings for the Function [dbo].[fn_String_To_Table] used in SQL
                string vAgentStatusString = vAgentStatus?.Count() != 0 ? string.Join(",", vAgentStatus) : "ALL";
                string vLicStatusString = vLicStatus?.Count() != 0 ? string.Join(",", vLicStatus) : "ALL";

                var parameters = new[]
                                {
                                    new SqlParameter("@vchCompanyID", vCompanyID == 0 ? (object)DBNull.Value : vCompanyID),
                                    new SqlParameter("@vchEmployeeSSN", vEmployeeSSN ?? (object)DBNull.Value),
                                    new SqlParameter("@vchGEID", vGEID ?? (object)DBNull.Value),
                                    new SqlParameter("@vchSCORENumber", vSCORENumber ?? (object)DBNull.Value),
                                    new SqlParameter("@vchNationalProducerNumber", vNationalProducerNumber == 0 ? (object)DBNull.Value : vNationalProducerNumber),
                                    new SqlParameter("@vchLastName", vLastName ?? (object)DBNull.Value),
                                    new SqlParameter("@vchFirstName", vFirstName ?? (object)DBNull.Value),
                                    new SqlParameter("@vchAgentStatus", vAgentStatusString),
                                    new SqlParameter("@vchResState", vResState ?? (object)DBNull.Value),
                                    new SqlParameter("@vchWrkState", vWrkState ?? (object)DBNull.Value),
                                    new SqlParameter("@vchBranchCode", vBranchCode ?? (object)DBNull.Value),
                                    new SqlParameter("@vchEmployeeLicenseID", vEmployeeLicenseID ?? (object)DBNull.Value),
                                    new SqlParameter("@vchLicStatus", vLicStatusString),
                                    new SqlParameter("@vchLicState", vLicState ?? (object)DBNull.Value),
                                    new SqlParameter("@vchLicenseName", vLicenseName ?? (object)DBNull.Value),
                                    new SqlParameter("@vchEmploymentID", DBNull.Value)
                                };

                var queryEmployeeSearchResults = _db.OputEmployeeSearchResult
                                            .FromSqlRaw(sql, parameters)
                                            .AsNoTracking()
                                            .ToList();

                result.Success = true;
                result.ObjData = queryEmployeeSearchResults;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                //_logger.LogError(ex, "Error in EmployeeService.SearchEmployee()");
                _utilityService.LogError(ex.Message, "EMFTEST-Source", new { }, "EMFTEST-UserSOEID");
            }
            //return result;
            return Task.FromResult(result);
        }

        public Task<ReturnResult> SearchEmployeeName(string vInput)
        {
            var result = new ReturnResult();
            try
            {
                var query = from a in _db.Employees
                            join b in _db.Employments on a.EmployeeId equals b.EmployeeId
                            join c in (
                                from th in _db.TransferHistories
                                group th by th.EmploymentId into g
                                select new
                                {
                                    EmploymentId = g.Key,
                                    TransferHistoryId = g.Max(th => th.TransferHistoryId)
                                }
                            ) on b.EmploymentId equals c.EmploymentId into cGroup
                            from c in cGroup.DefaultIfEmpty()
                            join d in _db.TransferHistories on c.TransferHistoryId equals d.TransferHistoryId into dGroup
                            from d in dGroup.DefaultIfEmpty()
                            join bdh in _db.Bifs on d.BranchCode.Substring(d.BranchCode.Length - 8) equals bdh.HrDepartmentId.Substring(bdh.HrDepartmentId.Length - 8) into bdhGroup
                            from bdh in bdhGroup.DefaultIfEmpty()
                            where (a.FirstName + " " + a.LastName).Contains(vInput)
                            orderby a.FirstName, d.ResStateAbv, d.WorkStateAbv
                            select new
                            {
                                a.EmployeeId,
                                a.Geid,
                                Name = a.LastName + ", " + a.FirstName + (a.MiddleName != null ? a.MiddleName.Substring(0, 1) : ""),
                                ResState = d.ResStateAbv,
                                WorkState = d.WorkStateAbv,
                                BranchName = bdh.Name ?? "UNKNOWN",
                            };

                var queryResult = query.ToList();

                result.Success = true;
                result.ObjData = queryResult;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.StatusCode = 500;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# EMPL-1309-30410].";

                //_logger.LogError(ex, "Error in EmployeeService.SearchEmployee()");
                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, "EmplQuick-Name-Search");
            }

            return Task.FromResult(result);
        }

        public Task<ReturnResult> SearchEmployeeTMNumber(string vInput)
        {
            var result = new ReturnResult();
            try
            {
                var query = _db.Employees
                    .Where(e => (e.Geid == vInput))
                    .Select(e => new
                    {
                        e.EmployeeId,
                        e.Geid,
                        Name = e.LastName + ", " + e.FirstName + " " + (e.MiddleName != null ? e.MiddleName.Substring(0, 1) : ""),
                    })
                    .FirstOrDefault();

                result.Success = true;
                result.ObjData = query;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.StatusCode = 500;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# EMPL-1309-90412].";

                //_logger.LogError(ex, "Error in EmployeeService.SearchEmployee()");
                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, "EmplQuick-TM-Search");
            }

            return Task.FromResult(result);
        }

        public Task<ReturnResult> GetEmploymentCommunication(int vEmploymentCommunicationID)
        {
            var result = new ReturnResult();
            try
            {
                var query = _db.EmploymentCommunications
                    .Where(ec => ec.EmploymentCommunicationId == vEmploymentCommunicationID)
                    .Select(ec => new
                    {
                        ec.EmploymentCommunicationId,
                        ec.EmailTo,
                        ec.EmailFrom,
                        ec.EmailSubject,
                        ec.EmailBodyHtml,
                        ec.EmailAttachments,
                        ec.EmailCreator,
                        ec.EmailCreateDate,
                        ec.EmailSentDate,
                    })
                    .FirstOrDefault();

                result.Success = true;
                result.ObjData = query;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.StatusCode = 500;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# EMPL-6309-90412].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, "Employment-Comm");
            }

            return Task.FromResult(result);

        }

        public ReturnResult Index([FromBody] EmployeeIndex vInput)
        {
            String txt = String.Empty;
            int intSec = 1;
            DateTime dtDateTime = System.DateTime.Now;
            char[] delimiterChars = { '|' };

            string[] docs = vInput.Files.ToString().Split(delimiterChars);

            var result = new ReturnResult();
            try
            {
                foreach (var doc in vInput.Files)
                {
                    txt = String.Empty;
                    intSec = intSec + 1;

                    if (doc.Length > 0)
                    {
                        // Process the file
                        var fileName = Path.GetFileName(doc.FileName);
                        var destinationPath = Path.Combine(_importExportLoc, dtDateTime.AddSeconds(intSec).ToString("yyyyMMddHHmmss") + fileName);

                        using (var stream = new FileStream(destinationPath, FileMode.Create))
                        {
                            doc.CopyTo(stream);
                        }

                        FileInfo fi = new FileInfo(destinationPath);
                        fi.CopyTo(_importExportLoc + dtDateTime.AddSeconds(intSec).ToString("yyyyMMddHHmmss") + fi.Name);
                        fi.Delete();

                        String InFi = _importExportLoc + dtDateTime.AddSeconds(intSec).ToString("yyyyMMddHHmmss") + ".OCR";
                        //String InFi2 = path + dtDateTime.AddSeconds(intSec).ToString("yyyyMMddHHmmss") + ".OCR";
                        //File.Create(InFi).Dispose();

                        String strNow = dtDateTime.ToString("yyyy-MM-dd");
                        String strFilePath = _importExportLoc + dtDateTime.AddSeconds(intSec).ToString("yyyyMMddHHmmss") + fi.Name;

                        intSec++;
                    }
                }

                result.Success = true;
                result.ObjData = null;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.StatusCode = 500;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# EMPL-1309-90412].";
                //_logger.LogError(ex, "Error in EmployeeService.SearchEmployee()");
                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, "EmplQuick-TM-Search");
            }
            return result;
        }
        
        public async Task<ReturnResult> IndexV2([FromForm] EmployeeIndex vInput)
        {
            String txt = String.Empty;
            int intSec = 1;
            DateTime dtDateTime = System.DateTime.Now;

            var result = new ReturnResult();
            try
            {
                foreach (var doc in vInput.Files)
                {
                    txt = String.Empty;
                    intSec = intSec + 1;

                    if (doc.Length > 0)
                    {
                        // Process the file - use EXACT same approach as DocumentService.Upload
                        var fileName = Path.GetFileName(doc.FileName);
                        var timestamp = dtDateTime.AddSeconds(intSec).ToString("yyyyMMddHHmmss");
                        var finalFileName = timestamp + fileName;
                        var finalFilePath = Path.Combine(_importExportLoc, finalFileName);

                        // Ensure directory exists (same as DocumentService)
                        if (!Directory.Exists(_importExportLoc))
                        {
                            Directory.CreateDirectory(_importExportLoc);
                        }

                        // Use EXACT same pattern as DocumentService.Upload
                        using (var inputStream = doc.OpenReadStream())
                        using (var fileStream = new FileStream(finalFilePath, FileMode.Create))
                        {
                            await inputStream.CopyToAsync(fileStream);
                        }

                        // Create the .OCR file
                        String ocrFilePath = Path.Combine(_importExportLoc, timestamp + ".OCR");

                        String strNow = dtDateTime.ToString("yyyy-MM-dd");
                        String strTMFirstName = vInput.FirstName?.ToUpper() ?? String.Empty;
                        String strTMLastName = vInput.LastName?.ToUpper() ?? String.Empty;

                        String strBranchNumber = vInput.BranchName ?? "00000000";
                        String strScoreNumber = vInput.ScoreNumber ?? "0000";
                        String strDocType = vInput.DocumentType?.ToUpper() ?? String.Empty;
                        String strDocSubType = vInput.DocumentSubType?.ToUpper() ?? String.Empty;
                        String strEmployeeID = vInput.EmployeeID?.ToString() ?? String.Empty;
                        String strWorkState = vInput.WorkState ?? String.Empty;
                        String strTMNumber = vInput.Geid ?? String.Empty;
                        String strLicState = (vInput.LicenseState != "{StateProv}") ? vInput.LicenseState ?? String.Empty : String.Empty;

                        // Build OCR content (Tab = \t)
                        txt = strNow + "\t" +
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

                        // Write OCR file
                        await File.WriteAllTextAsync(ocrFilePath, txt);

                        intSec++;
                    }
                }

                result.Success = true;
                result.ObjData = null;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.StatusCode = 500;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# EMPL-1309-90412].";
                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, "EmplQuick-TM-Search");
            }

            return result;
        }

        public async Task<ReturnResult> IndexV3([FromForm] EmployeeIndex vInput)
        {
            var result = new ReturnResult();
            int intSec = 1;
            DateTime dtDateTime = System.DateTime.Now;

            try
            {
                // Validate configuration
                if (string.IsNullOrEmpty(_importExportLoc))
                {
                    result.Success = false;
                    result.StatusCode = 500;
                    result.ErrMessage = "Import/Export location not configured.";
                    return result;
                }

                // Ensure directory exists with proper permissions
                if (!Directory.Exists(_importExportLoc))
                {
                    Directory.CreateDirectory(_importExportLoc);
                    _utilityService.LogError($"Created directory: {_importExportLoc}", "IndexV3-Directory", new { }, "SYSTEM");
                }

                // Validate files exist
                if (vInput.Files == null || !vInput.Files.Any())
                {
                    result.Success = false;
                    result.StatusCode = 400;
                    result.ErrMessage = "No files provided for upload.";
                    return result;
                }

                foreach (var doc in vInput.Files)
                {
                    intSec = intSec + 1;

                    if (doc?.Length > 0)
                    {
                        var originalFileName = Path.GetFileName(doc.FileName) ?? $"document_{intSec}.pdf";
                        var timestamp = dtDateTime.AddSeconds(intSec).ToString("yyyyMMddHHmmss");
                        var finalFileName = timestamp + originalFileName;
                        var finalFilePath = Path.Combine(_importExportLoc, finalFileName);

                        _utilityService.LogError(
                            $"Starting file upload: {originalFileName} ({doc.Length} bytes) -> {finalFilePath}",
                            "IndexV3-FileStart",
                            new { 
                                OriginalFileName = originalFileName,
                                FileSize = doc.Length,
                                ContentType = doc.ContentType,
                                FinalPath = finalFilePath 
                            },
                            "SYSTEM");

                        // Save file with robust error handling
                        await SaveFileRobustly(doc, finalFilePath);

                        // Comprehensive file validation
                        await ValidateUploadedFile(doc, finalFilePath, originalFileName);

                        // Create OCR file
                        var ocrFilePath = Path.Combine(_importExportLoc, timestamp + ".OCR");
                        await CreateOcrFileRobustly(ocrFilePath, finalFilePath, vInput, dtDateTime);

                        _utilityService.LogError(
                            $"Successfully processed: {finalFilePath}",
                            "IndexV3-Success",
                            new { 
                                FileName = finalFileName,
                                FileSize = new FileInfo(finalFilePath).Length,
                                OcrFile = ocrFilePath 
                            },
                            "SYSTEM");
                    }
                    else
                    {
                        _utilityService.LogError(
                            $"Skipped empty file: {doc?.FileName ?? "unknown"}",
                            "IndexV3-SkippedEmpty",
                            new { FileName = doc?.FileName, Length = doc?.Length },
                            "SYSTEM");
                    }
                }

                result.Success = true;
                result.StatusCode = 200;
                result.ObjData = $"Successfully processed {vInput.Files.Count} files";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.StatusCode = 500;
                result.ErrMessage = "Server Error - Please Contact Support [REF# EMPL-1309-90412].";
                _utilityService.LogError(
                    ex.Message, 
                    "IndexV3-FileUpload-Error", 
                    new { 
                        StackTrace = ex.StackTrace,
                        InnerException = ex.InnerException?.Message,
                        ImportExportLoc = _importExportLoc
                    }, 
                    "SYSTEM");
            }

            return result;
        }

        // Robust file saving with multiple fallback strategies
        private async Task SaveFileRobustly(IFormFile file, string destinationPath)
        {
            const int maxRetries = 3;
            const int bufferSize = 4096;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    // Strategy 1: Direct stream copy (preferred)
                    using (var inputStream = file.OpenReadStream())
                    using (var outputStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, FileOptions.WriteThrough))
                    {
                        await inputStream.CopyToAsync(outputStream, bufferSize);
                        await outputStream.FlushAsync();
                        
                        // Force OS to write to disk
                        outputStream.Close();
                    }
                    
                    // Verify immediately after write
                    if (File.Exists(destinationPath) && new FileInfo(destinationPath).Length > 0)
                    {
                        return; // Success
                    }
                    
                    throw new InvalidOperationException("File write verification failed");
                }
                catch (Exception ex) when (attempt < maxRetries)
                {
                    _utilityService.LogError(
                        $"File save attempt {attempt} failed: {ex.Message}. Retrying...",
                        "IndexV3-SaveRetry",
                        new { Attempt = attempt, Error = ex.Message },
                        "SYSTEM");
                    
                    // Clean up partial file
                    if (File.Exists(destinationPath))
                    {
                        try { File.Delete(destinationPath); } catch { }
                    }
                    
                    // Wait before retry
                    await Task.Delay(100 * attempt);
                }
            }
            
            throw new InvalidOperationException($"Failed to save file after {maxRetries} attempts");
        }

        // Comprehensive file validation
        private async Task ValidateUploadedFile(IFormFile originalFile, string savedFilePath, string originalFileName)
        {
            var fileInfo = new FileInfo(savedFilePath);
            
            // Basic existence and size checks
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException($"Saved file not found: {savedFilePath}");
            }
            
            if (fileInfo.Length == 0)
            {
                throw new InvalidOperationException($"Saved file is empty: {savedFilePath}");
            }
            
            if (fileInfo.Length != originalFile.Length)
            {
                throw new InvalidOperationException(
                    $"File size mismatch for {originalFileName}. Expected: {originalFile.Length}, Got: {fileInfo.Length}");
            }
            
            // PDF-specific validation
            if (Path.GetExtension(savedFilePath).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                await ValidatePdfFile(savedFilePath);
            }
            
            _utilityService.LogError(
                $"File validation passed: {savedFilePath}",
                "IndexV3-ValidationSuccess",
                new { 
                    OriginalSize = originalFile.Length,
                    SavedSize = fileInfo.Length,
                    IsValid = true 
                },
                "SYSTEM");
        }

        // Enhanced PDF validation
        private async Task ValidatePdfFile(string filePath)
        {
            try
            {
                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                
                if (fileStream.Length < 5)
                {
                    throw new InvalidOperationException("File too small to be a valid PDF");
                }
                
                var pdfHeader = new byte[5];
                await fileStream.ReadAsync(pdfHeader, 0, 5);
                
                // Check for PDF signature: %PDF-
                if (pdfHeader[0] == 0x25 && // %
                    pdfHeader[1] == 0x50 && // P
                    pdfHeader[2] == 0x44 && // D
                    pdfHeader[3] == 0x46 && // F
                    pdfHeader[4] == 0x2D)   // -
                {
                    // Additional check: look for EOF marker
                    fileStream.Seek(-1024, SeekOrigin.End);
                    var endBuffer = new byte[1024];
                    await fileStream.ReadAsync(endBuffer, 0, 1024);
                    var endString = System.Text.Encoding.ASCII.GetString(endBuffer);
                    
                    if (endString.Contains("%%EOF"))
                    {
                        return; // Valid PDF
                    }
                    
                    _utilityService.LogError(
                        $"PDF missing EOF marker: {filePath}",
                        "IndexV3-PDFWarning",
                        new { Warning = "PDF missing EOF marker but has valid header" },
                        "SYSTEM");
                    return; // Still consider valid
                }
                
                var headerString = System.Text.Encoding.ASCII.GetString(pdfHeader);
                throw new InvalidOperationException($"Invalid PDF header. Found: '{headerString}' instead of '%PDF-'");
            }
            catch (Exception ex) when (!(ex is InvalidOperationException))
            {
                throw new InvalidOperationException($"PDF validation failed for {filePath}: {ex.Message}");
            }
        }

        // Robust OCR file creation
        private async Task CreateOcrFileRobustly(string ocrFilePath, string documentFilePath, EmployeeIndex vInput, DateTime dtDateTime)
        {
            try
            {
                var ocrContent = BuildOcrContent(dtDateTime, documentFilePath, vInput);
                
                // Write with explicit encoding and error handling
                await File.WriteAllTextAsync(ocrFilePath, ocrContent, System.Text.Encoding.UTF8);
                
                // Verify OCR file was created
                if (!File.Exists(ocrFilePath))
                {
                    throw new InvalidOperationException($"Failed to create OCR file: {ocrFilePath}");
                }
                
                var ocrSize = new FileInfo(ocrFilePath).Length;
                if (ocrSize == 0)
                {
                    throw new InvalidOperationException($"OCR file is empty: {ocrFilePath}");
                }
                
                _utilityService.LogError(
                    $"OCR file created: {ocrFilePath} ({ocrSize} bytes)",
                    "IndexV3-OCRSuccess",
                    new { OcrPath = ocrFilePath, Size = ocrSize },
                    "SYSTEM");
            }
            catch (Exception ex)
            {
                _utilityService.LogError(
                    $"OCR file creation failed: {ex.Message}",
                    "IndexV3-OCRError",
                    new { OcrPath = ocrFilePath, Error = ex.Message },
                    "SYSTEM");
                throw;
            }
        }

        // Enhanced OCR content builder with validation
        private string BuildOcrContent(DateTime dtDateTime, string finalFilePath, EmployeeIndex vInput)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(vInput.FirstName) || string.IsNullOrWhiteSpace(vInput.LastName))
            {
                throw new ArgumentException("FirstName and LastName are required for OCR file generation");
            }
            
            var ocrFields = new[]
            {
                dtDateTime.ToString("yyyy-MM-dd"),
                finalFilePath,
                (vInput.FirstName?.ToUpper() ?? string.Empty).Trim(),
                (vInput.LastName?.ToUpper() ?? string.Empty).Trim(),
                (!string.IsNullOrEmpty(vInput.BranchName)) ? vInput.BranchName.Trim() : "00000000",
                (!string.IsNullOrEmpty(vInput.ScoreNumber)) ? vInput.ScoreNumber.Trim() : "0000",
                (!string.IsNullOrEmpty(vInput.DocumentType)) ? vInput.DocumentType.ToUpper().Trim() : "DOCUMENT",
                (!string.IsNullOrEmpty(vInput.DocumentSubType)) ? vInput.DocumentSubType.ToUpper().Trim() : "GENERAL",
                (vInput.EmployeeID?.ToString() ?? string.Empty).Trim(),
                (!string.IsNullOrEmpty(vInput.WorkState)) ? vInput.WorkState.Trim() : string.Empty,
                (vInput.Geid ?? string.Empty).Trim(),
                (!string.IsNullOrEmpty(vInput.LicenseState) && vInput.LicenseState != "{StateProv}") ? vInput.LicenseState.Trim() : string.Empty
            };

            // Clean and validate fields
            for (int i = 0; i < ocrFields.Length; i++)
            {
                ocrFields[i] = ocrFields[i]?.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ") ?? string.Empty;
            }

            return string.Join("\t", ocrFields);
        }
    }
}
