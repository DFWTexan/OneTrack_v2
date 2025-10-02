using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DbData;
using OneTrack_v2.DbData.Models;
using OneTrack_v2.Services;
using OneTrak_v2.DataModel;

namespace OneTrak_v2.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDataContext _db;
        private readonly IUtilityHelpService _utilityService;

        public DashboardService(AppDataContext db, IUtilityHelpService utilityHelpService) { _db = db; _utilityService = utilityHelpService; }

        public ReturnResult GetAdBankerIncompleteCount()
        {

            var result = new ReturnResult();
            try
            {
                var incompleteCount = _db.StgADBankerImports.Count(x => x.IsImportComplete == false && x.TeamMemberId != 0);
                result.ObjData = incompleteCount;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-12347].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }
            return result;
        }
        public ReturnResult GetAdBankerImportStatus()
        {
            var result = new ReturnResult();
            try
            {
                var maxCreateDate = _db.StgADBankerImports.Max(x => x.CreateDate.Date);
                var count = _db.StgADBankerImports.Count(x => x.CreateDate.Date == maxCreateDate);

                var importStatus = new { RecordCount = count, LastImportDate = maxCreateDate };

                result.ObjData = importStatus;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-32007].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }
        public ReturnResult GetAdBankerImportData(DateTime vStartDate, DateTime vEndDate, bool? vImportStatus = null)
        {
            var result = new ReturnResult();
            try
            {
                var query = _db.StgADBankerImports.AsQueryable();

                query = query.Where(x => x.CreateDate.Date >= vStartDate.Date && x.CreateDate.Date <= vEndDate.Date);
                query = query.Where(x => x.TeamMemberId != 0);

                if (vImportStatus.HasValue)
                {
                    query = query.Where(x => x.IsImportComplete == vImportStatus);
                }

                var adBankerImportData = query.Select(x => new
                {
                    x.TeamMemberId,
                    x.CourseState,
                    x.StudentName,
                    x.CourseTitle,
                    x.CompletionDate,
                    x.ReportedDate,
                    x.TotalCredits,
                    x.CreateDate,
                    x.IsImportComplete
                }).OrderBy(x => x.IsImportComplete) // false first
                  .ThenByDescending(x => x.CreateDate).ToList();

                result.ObjData = adBankerImportData;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-12347].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }
        public ReturnResult CompleteImportStatus([FromBody] IputADBankerImportStatus vInput)
        {

            var result = new ReturnResult();
            try
            {
                var adBankerImport = _db.StgADBankerImports
                    .FirstOrDefault(x => x.TeamMemberId == vInput.TeamMemberID && x.CourseState == vInput.CourseState && x.StudentName == vInput.StudentName && x.CourseTitle == vInput.CourseTitle);
                if (adBankerImport != null)
                {
                    var sql = "UPDATE stg_ADBankerImport SET " +
                              "IsImportComplete = @IsImportComplete, ModifiedBy = @ModifiedBy, ModifyDate = @ModifyDate " +
                              "WHERE CourseState = @CourseState AND StudentName = @StudentName AND CourseTitle = @CourseTitle";

                    var parameters = new[]
                    {
                        new SqlParameter("@IsImportComplete", true),
                        new SqlParameter("@ModifiedBy", vInput.UserSOEID),
                        new SqlParameter("@ModifyDate", DateTime.Now),
                        new SqlParameter("@CourseState", vInput.CourseState ?? (object)DBNull.Value),
                        new SqlParameter("@StudentName", vInput.StudentName ?? (object)DBNull.Value),
                        new SqlParameter("@CourseTitle", vInput.CourseTitle ?? (object)DBNull.Value)
                    };

                    _db.Database.ExecuteSqlRaw(sql, parameters);

                    result.Success = true;
                    result.StatusCode = 200;
                }
                else
                {
                    result.Success = false;
                    result.StatusCode = 404;
                    result.ErrMessage = "Record Not Found.";
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-78947].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }
            return result;
        }
        public ReturnResult GetAuditModifiedBy(bool vIsActive = true)
        {
            var result = new ReturnResult();
            try
            {
                if (vIsActive)
                {
                    var modifiedBy = _db.AuditLogs
                                    .Join(
                                        _db.LicenseTeches,
                                        auditLog => auditLog.ModifiedBy,
                                        licenseTech => licenseTech.Soeid,
                                        (auditLog, licenseTech) => new
                                        {
                                            auditLog.ModifiedBy,
                                            IsActive = licenseTech.IsActive,
                                            FullName = (licenseTech.FirstName != null ? licenseTech.FirstName.Substring(0, 1) + ". " : "") + licenseTech.LastName
                                        }
                                    )
                                    .Where(x => x.IsActive == true)
                                    .Select(x => new { x.ModifiedBy, x.FullName })
                                    .Distinct()
                                    .OrderBy(x => x.ModifiedBy)
                                    .ToList();

                    result.ObjData = modifiedBy;
                }
                else
                {
                    var modifiedBy = _db.AuditLogs
                                    .GroupJoin(
                                        _db.LicenseTeches,
                                        auditLog => auditLog.ModifiedBy,
                                        licenseTech => licenseTech.Soeid,
                                        (auditLog, licenseTeches) => new { auditLog, licenseTeches }
                                    )
                                    .SelectMany(
                                        x => x.licenseTeches.DefaultIfEmpty(),
                                        (x, licenseTech) => new
                                        {
                                            x.auditLog.ModifiedBy,
                                            FullName = licenseTech != null
                                                ? (licenseTech.FirstName != null ? licenseTech.FirstName.Substring(0, 1) + ". " : "") + licenseTech.LastName
                                                : x.auditLog.ModifiedBy
                                        }
                                    )
                                    .Distinct()
                                    .OrderBy(x => x.ModifiedBy)
                                    .ToList();

                    result.ObjData = modifiedBy;
                }

                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-12007].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }
        public ReturnResult GetAuditLog(DateTime vStartDate, DateTime vEndDate, string? vModifiedBy = null)
        {
            var result = new ReturnResult();
            try
            {
                var query = _db.AuditLogs.AsQueryable();

                query = query.Where(x => x.ModifyDate >= vStartDate && x.ModifyDate <= vEndDate);

                if (!string.IsNullOrEmpty(vModifiedBy))
                {
                    query = query.Where(x => x.ModifiedBy != null && x.ModifiedBy == vModifiedBy);
                }

                var data = query.ToList();

                var auditLog = data.Select(x => new
                {
                    x.BaseTableName,
                    x.BaseTableKeyValue,
                    x.ModifyDate,
                    x.ModifiedBy,
                    x.AuditFieldName,
                    x.AuditAction,
                    x.AuditValueBefore,
                    x.AuditValueAfter,
                    LicenseTechName = GetLicensTechName(x.ModifiedBy)
                })
                    .AsEnumerable()
                    .Select(x => new AuditLog
                    {
                        BaseTableName = x.BaseTableName,
                        BaseTableKeyValue = x.BaseTableKeyValue,
                        ModifyDate = x.ModifyDate,
                        ModifiedBy = string.IsNullOrEmpty(x.LicenseTechName) ? x.ModifiedBy : x.LicenseTechName,
                        AuditFieldName = x.AuditFieldName,
                        AuditAction = x.AuditAction,
                        AuditValueBefore = x.AuditValueBefore,
                        AuditValueAfter = x.AuditValueAfter
                    })
                    .OrderByDescending(x => x.ModifyDate)
                    .ToList();

                result.ObjData = auditLog;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-12099].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }
        public ReturnResult GetAuditLogAdHoc(DateTime vStartDate, DateTime vEndDate,
                string? vModifiedBy = null, string? vBaseTableName = null,
                string? vBaseTableKeyValue = null, string? vAuditFieldName = null,
                string? vAuditAction = null, int pageNumber = 1, int pageSize = 100)
        {
            var result = new ReturnResult();
            try
            {
                // Option 1: Use direct SQL for better performance with pagination
                // Build SQL with parameters to avoid SQL injection
                var sql = @"
                    SELECT * FROM (
                        SELECT 
                            al.BaseTableName, 
                            al.BaseTableKeyValue, 
                            al.ModifyDate, 
                            al.ModifiedBy, 
                            al.AuditFieldName, 
                            al.AuditAction, 
                            al.AuditValueBefore, 
                            al.AuditValueAfter,
                            ROW_NUMBER() OVER (ORDER BY al.ModifyDate DESC) AS RowNum
                        FROM dbo.AuditLog al
                        WHERE al.ModifyDate >= @StartDate AND al.ModifyDate <= @EndDate
                            AND (@ModifiedBy IS NULL OR al.ModifiedBy = @ModifiedBy)
                            AND (@BaseTableName IS NULL OR al.BaseTableName LIKE '%' + @BaseTableName + '%')
                            AND (@BaseTableKeyValue IS NULL OR al.BaseTableKeyValue LIKE '%' + @BaseTableKeyValue + '%')
                            AND (@AuditFieldName IS NULL OR al.AuditFieldName LIKE '%' + @AuditFieldName + '%')
                            AND (@AuditAction IS NULL OR al.AuditAction = @AuditAction)
                    ) AS FilteredResults
                    WHERE RowNum BETWEEN @StartRow AND @EndRow";

                // Build the count query separately for pagination metadata
                var countSql = @"
                    SELECT COUNT(*)
                    FROM dbo.AuditLog al
                    WHERE al.ModifyDate >= @StartDate AND al.ModifyDate <= @EndDate
                        AND (@ModifiedBy IS NULL OR al.ModifiedBy = @ModifiedBy)
                        AND (@BaseTableName IS NULL OR al.BaseTableName LIKE '%' + @BaseTableName + '%')
                        AND (@BaseTableKeyValue IS NULL OR al.BaseTableKeyValue LIKE '%' + @BaseTableKeyValue + '%')
                        AND (@AuditFieldName IS NULL OR al.AuditFieldName LIKE '%' + @AuditFieldName + '%')
                        AND (@AuditAction IS NULL OR al.AuditAction = @AuditAction)";

                // Calculate pagination values
                int startRow = (pageNumber - 1) * pageSize + 1;
                int endRow = startRow + pageSize - 1;

                var parameters = new[]
                {
                    new SqlParameter("@StartDate", vStartDate),
                    new SqlParameter("@EndDate", vEndDate),
                    new SqlParameter("@ModifiedBy", vModifiedBy ?? (object)DBNull.Value),
                    new SqlParameter("@BaseTableName", vBaseTableName ?? (object)DBNull.Value),
                    new SqlParameter("@BaseTableKeyValue", vBaseTableKeyValue ?? (object)DBNull.Value),
                    new SqlParameter("@AuditFieldName", vAuditFieldName ?? (object)DBNull.Value),
                    new SqlParameter("@AuditAction", vAuditAction ?? (object)DBNull.Value),
                    new SqlParameter("@StartRow", startRow),
                    new SqlParameter("@EndRow", endRow)
                };

                // Execute count query first to get total records
                var countParameters = new[]
                {
                    new SqlParameter("@StartDate", vStartDate),
                    new SqlParameter("@EndDate", vEndDate),
                    new SqlParameter("@ModifiedBy", vModifiedBy ?? (object)DBNull.Value),
                    new SqlParameter("@BaseTableName", vBaseTableName ?? (object)DBNull.Value),
                    new SqlParameter("@BaseTableKeyValue", vBaseTableKeyValue ?? (object)DBNull.Value),
                    new SqlParameter("@AuditFieldName", vAuditFieldName ?? (object)DBNull.Value),
                    new SqlParameter("@AuditAction", vAuditAction ?? (object)DBNull.Value)
                };

                // Use connection directly for better control of the query execution
                var totalCount = 0;
                using (var connection = new SqlConnection(_db.Database.GetConnectionString()))
                {
                    connection.Open();
                    
                    // Get total count
                    using (var command = new SqlCommand(countSql, connection))
                    {
                        foreach (var param in countParameters)
                        {
                            command.Parameters.Add(param);
                        }
                        
                        totalCount = (int)command.ExecuteScalar();
                    }
                    
                    // Get paginated data with a separate command
                    var auditLogs = new List<AuditLog>();
                    
                    using (var command = new SqlCommand(sql, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.Add(param);
                        }
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var baseTableName = reader["BaseTableName"] as string;
                                var baseTableKeyValue = reader["BaseTableKeyValue"] as string;
                                var modifyDate = (DateTime)reader["ModifyDate"];
                                var modifiedBy = reader["ModifiedBy"] as string;
                                var auditFieldName = reader["AuditFieldName"] as string;
                                var auditAction = reader["AuditAction"] as string;
                                var auditValueBefore = reader["AuditValueBefore"] as string;
                                var auditValueAfter = reader["AuditValueAfter"] as string;
                                
                                // Get license tech name efficiently
                                var licenseTechName = GetLicensTechName(modifiedBy);
                                
                                auditLogs.Add(new AuditLog
                                {
                                    BaseTableName = baseTableName,
                                    BaseTableKeyValue = baseTableKeyValue,
                                    ModifyDate = modifyDate,
                                    ModifiedBy = string.IsNullOrEmpty(licenseTechName) ? modifiedBy : licenseTechName,
                                    AuditFieldName = auditFieldName,
                                    AuditAction = auditAction,
                                    AuditValueBefore = auditValueBefore,
                                    AuditValueAfter = auditValueAfter
                                });
                            }
                        }
                    }
                    
                    // Create a result object that includes pagination metadata
                    var paginatedResult = new
                    {
                        TotalCount = totalCount,
                        PageSize = pageSize,
                        CurrentPage = pageNumber,
                        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                        Data = auditLogs
                    };
                    
                    result.ObjData = paginatedResult;
                    result.Success = true;
                    result.StatusCode = 200;
                    
                    _utilityService.LogError("AuditLogAdHoc Results",
                        $"Total Records: {totalCount}, Page {pageNumber} of {(int)Math.Ceiling(totalCount / (double)pageSize)}, Showing {auditLogs.Count} records",
                        new { }, null);
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-12101].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }

        // Option 2: Using stored procedure - create this method to call a stored procedure
        //public ReturnResult GetAuditLogAdHocStoredProc(DateTime vStartDate, DateTime vEndDate,
        //    string? vModifiedBy = null, string? vBaseTableName = null,
        //    string? vBaseTableKeyValue = null, string? vAuditFieldName = null,
        //    string? vAuditAction = null, int pageNumber = 1, int pageSize = 100)
        //{
        //    var result = new ReturnResult();
        //    try
        //    {
        //        // Create parameters for stored procedure
        //        var parameters = new[]
        //        {
        //            new SqlParameter("@StartDate", vStartDate),
        //            new SqlParameter("@EndDate", vEndDate),
        //            new SqlParameter("@ModifiedBy", vModifiedBy ?? (object)DBNull.Value),
        //            new SqlParameter("@BaseTableName", vBaseTableName ?? (object)DBNull.Value),
        //            new SqlParameter("@BaseTableKeyValue", vBaseTableKeyValue ?? (object)DBNull.Value),
        //            new SqlParameter("@AuditFieldName", vAuditFieldName ?? (object)DBNull.Value),
        //            new SqlParameter("@AuditAction", vAuditAction ?? (object)DBNull.Value),
        //            new SqlParameter("@PageNumber", pageNumber),
        //            new SqlParameter("@PageSize", pageSize),
        //            new SqlParameter("@TotalCount", SqlDbType.Int) { Direction = ParameterDirection.Output }
        //        };

        //        // Execute stored procedure and map to DTO
        //        var auditLogs = new List<AuditLog>();
                
        //        using (var connection = new SqlConnection(_db.Database.GetConnectionString()))
        //        {
        //            connection.Open();
        //            using (var command = new SqlCommand("uspGetAuditLogAdHoc", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                foreach (var param in parameters)
        //                {
        //                    command.Parameters.Add(param);
        //                }

        //                using (var reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        var baseTableName = reader["BaseTableName"] as string;
        //                        var baseTableKeyValue = reader["BaseTableKeyValue"] as string;
        //                        var modifyDate = (DateTime)reader["ModifyDate"];
        //                        var modifiedBy = reader["ModifiedBy"] as string;
        //                        var auditFieldName = reader["AuditFieldName"] as string;
        //                        var auditAction = reader["AuditAction"] as string;
        //                        var auditValueBefore = reader["AuditValueBefore"] as string;
        //                        var auditValueAfter = reader["AuditValueAfter"] as string;
                                
        //                        // Get license tech name efficiently
        //                        var licenseTechName = GetLicensTechName(modifiedBy);
                                
        //                        auditLogs.Add(new AuditLog
        //                        {
        //                            BaseTableName = baseTableName,
        //                            BaseTableKeyValue = baseTableKeyValue,
        //                            ModifyDate = modifyDate,
        //                            ModifiedBy = string.IsNullOrEmpty(licenseTechName) ? modifiedBy : licenseTechName,
        //                            AuditFieldName = auditFieldName,
        //                            AuditAction = auditAction,
        //                            AuditValueBefore = auditValueBefore,
        //                            AuditValueAfter = auditValueAfter
        //                        });
        //                    }
        //                }
        //            }
                    
        //            // Get total count from output parameter
        //            int totalCount = (int)parameters.First(p => p.ParameterName == "@TotalCount").Value;
                    
        //            // Create a result object that includes pagination metadata
        //            var paginatedResult = new
        //            {
        //                TotalCount = totalCount,
        //                PageSize = pageSize,
        //                CurrentPage = pageNumber,
        //                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
        //                Data = auditLogs
        //            };
                    
        //            result.ObjData = paginatedResult;
        //            result.Success = true;
        //            result.StatusCode = 200;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.StatusCode = 500;
        //        result.Success = false;
        //        result.ObjData = null;
        //        result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-12102].";

        //        _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
        //    }

        //    return result;
        //}

        // Option 3: Add a compiled query for better performance
        //private static Func<AppDataContext, DateTime, DateTime, string, string, string, string, string, IQueryable<AuditLog>> _compiledAuditLogQuery = 
        //    EF.CompileQuery((AppDataContext context, DateTime startDate, DateTime endDate, string modifiedBy, 
        //    string baseTableName, string baseTableKeyValue, string auditFieldName, string auditAction) =>
        //        from log in context.AuditLogs
        //        where log.ModifyDate >= startDate && log.ModifyDate <= endDate
        //          && (string.IsNullOrEmpty(modifiedBy) || log.ModifiedBy == modifiedBy)
        //          && (string.IsNullOrEmpty(baseTableName) || log.BaseTableName.Contains(baseTableName))
        //          && (string.IsNullOrEmpty(baseTableKeyValue) || log.BaseTableKeyValue.Contains(baseTableKeyValue))
        //          && (string.IsNullOrEmpty(auditFieldName) || log.AuditFieldName.Contains(auditFieldName))
        //          && (string.IsNullOrEmpty(auditAction) || log.AuditAction == auditAction)
        //        orderby log.ModifyDate descending
        //        select new AuditLog
        //        {
        //            BaseTableName = log.BaseTableName,
        //            BaseTableKeyValue = log.BaseTableKeyValue,
        //            ModifyDate = log.ModifyDate,
        //            ModifiedBy = log.ModifiedBy,
        //            AuditFieldName = log.AuditFieldName,
        //            AuditAction = log.AuditAction,
        //            AuditValueBefore = log.AuditValueBefore,
        //            AuditValueAfter = log.AuditValueAfter
        //        });

        //public ReturnResult GetAuditLogAdHocCompiled(DateTime vStartDate, DateTime vEndDate,
        //    string? vModifiedBy = null, string? vBaseTableName = null,
        //    string? vBaseTableKeyValue = null, string? vAuditFieldName = null,
        //    string? vAuditAction = null, int pageNumber = 1, int pageSize = 100)
        //{
        //    var result = new ReturnResult();
        //    try
        //    {
        //        // Use compiled query for better performance
        //        var query = _compiledAuditLogQuery(_db, vStartDate, vEndDate, 
        //            vModifiedBy ?? "", vBaseTableName ?? "", vBaseTableKeyValue ?? "", 
        //            vAuditFieldName ?? "", vAuditAction ?? "");
                
        //        // Count query cannot be optimized further without separate compilation
        //        var totalCount = query.Count();
                
        //        // Apply pagination
        //        var paginatedData = query
        //            .Skip((pageNumber - 1) * pageSize)
        //            .Take(pageSize)
        //            .ToList();
                
        //        // Post-process to add license tech names
        //        foreach (var item in paginatedData)
        //        {
        //            var licenseTechName = GetLicensTechName(item.ModifiedBy);
        //            if (!string.IsNullOrEmpty(licenseTechName))
        //            {
        //                item.ModifiedBy = licenseTechName;
        //            }
        //        }
                
        //        // Create a result object that includes pagination metadata
        //        var paginatedResult = new
        //        {
        //            TotalCount = totalCount,
        //            PageSize = pageSize,
        //            CurrentPage = pageNumber,
        //            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
        //            Data = paginatedData
        //        };
                
        //        result.ObjData = paginatedResult;
        //        result.Success = true;
        //        result.StatusCode = 200;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.StatusCode = 500;
        //        result.Success = false;
        //        result.ObjData = null;
        //        result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-12103].";

        //        _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
        //    }

        //    return result;
        //}

        public async Task<ReturnResult> GetAuditBaseTableNames()
        {
            var result = new ReturnResult();
            try
            {
                var baseTableNames = await _db.AuditLogs
                                    .Where(x => x.BaseTableName != null)
                                    .Select(x => x.BaseTableName)
                                    .Distinct()
                                    .OrderBy(x => x)
                                    .ToListAsync();

                result.ObjData = baseTableNames;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# MISC-8807-32099].";
                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }
            return result;
        }
        public ReturnResult GetEmployeeIdWithTMemberID(string vMemberID)
        {
            var result = new ReturnResult();
            try
            {
                var employeeID = _db.Employees.Where(e => e.Geid == vMemberID).Select(e => e.EmployeeId).FirstOrDefault();

                result.ObjData = employeeID;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DASH-8807-22872].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }
        
        private string GetLicensTechName(string vSoeid)
        {
            var licenseTech = _db.LicenseTeches.FirstOrDefault(x => x.Soeid == vSoeid);
            if (licenseTech != null)
            {
                var firstNameInitial = licenseTech.FirstName?.FirstOrDefault().ToString() ?? string.Empty;
                return firstNameInitial + ". " + licenseTech.LastName;
            }
            return string.Empty;
        }
    }
}
