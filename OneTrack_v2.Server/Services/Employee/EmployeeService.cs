﻿using Microsoft.EntityFrameworkCore;
using System.Data;
using OneTrack_v2.DbData;
using OneTrack_v2.DataModel.StoredProcedures;
using DataModel.Response;
using OneTrak_v2.DataModel;
using System;
using NuGet.Packaging;
using Microsoft.Data.SqlClient;

namespace OneTrack_v2.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDataContext _db;
        private readonly IUtilityHelpService _utilityService;
        //private readonly UtilityService.Utility _utility;

        //private readonly IConfiguration _config;
        //private readonly IWebHostEnvironment _env;
        //private readonly ILogger _logger;

        public EmployeeService(AppDataContext db, IUtilityHelpService utilityHelpService)
        {
            _db = db;
            _utilityService = utilityHelpService;
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
        public async Task<ReturnResult> SearchEmployee(string? vEmployeeSSN = null, string? vGEID = null, string? vSCORENumber = null, int? vCompanyID = 0,
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
            return result;
        }

        //private static List<string> StringToTable(string input, char delimiter, bool trimSpace)
        //{
        //    var result = new List<string>();

        //    if (input == null) return result;

        //    var elements = input.Split(new[] { delimiter }, StringSplitOptions.None);
        //    foreach (var element in elements)
        //    {
        //        var val = trimSpace ? element.Trim() : element;
        //        result.Add(val);
        //    }

        //    return result;
        //}

        //public DataModel.Response.ReturnResult GetEmployee(int vEmployeeID)
        //{
        //    var result = new DataModel.Response.ReturnResult();
        //    try
        //    {
        //        var employees = _db.Employees.ToList();
        //        result.ObjData = employees;
        //        result.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.ErrMessage = ex.Message;
        //        //_logger.LogError(ex, "Error in EmployeeService.GetEmployees()");
        //    }
        //    return result;
        //}

    }
}
