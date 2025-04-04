﻿using DataModel.Response;
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
