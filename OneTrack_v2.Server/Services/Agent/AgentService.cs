using Microsoft.EntityFrameworkCore;
using System.Data;
using OneTrack_v2.DbData;
using DataModel.Response;
using OneTrack_v2.DataModel;
using Microsoft.Data.SqlClient;
using OneTrack_v2.DbData.Models;
using Microsoft.AspNetCore.Mvc;
using OneTrak_v2.DataModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq;

namespace OneTrack_v2.Services
{
    public class AgentService : IAgentService
    {
        private readonly IConfiguration _config;
        private readonly AppDataContext _db;
        private readonly IUtilityHelpService _utilityService;
        private readonly string? _connectionString;

        //private readonly IWebHostEnvironment _env;
        //private readonly ILogger _logger;

        public AgentService(AppDataContext db, IConfiguration config, IUtilityHelpService utilityHelpService)
        {
            _db = db;
            _config = config;
            _connectionString = _config.GetConnectionString(name: "DefaultConnection");
            _utilityService = utilityHelpService;
        }

        /// <summary>
        ///  <param name="@vchEmployeeID" int>699606</param>   
        ///  <returns>
        ///     "employeeID": 699606,
        ///     "employmentID": 606709,
        ///     "employeeStatus": "Active",
        ///     "lastName": "SMITH",
        ///     "firstName": "AMANDA",
        ///     "middleName": null,
        ///     "jobTitle": null,
        ///     "employeeSSN": "000008400",
        ///     "soeid": null,
        ///     "address1": "1514 SUNSET ST",
        ///     "address2": null,
        ///     "city": "BLUE SPRINGS",
        ///     "state": "MO",
        ///     "zip": "64015",
        ///     "phone": null,
        ///     "dateOfBirth": "1900-01-01T00:00:00" 
        /// </returns>
        public ReturnResult GetAgentByEmployeeID(int vEmployeeID)
        {
            var result = new ReturnResult();
            try
            {
                var query = from employee in _db.Employees
                            join employment in _db.Employments on employee.EmployeeId equals employment.EmployeeId
                            join company in _db.Companies on employment.CompanyId equals company.CompanyId
                            join address in _db.Addresses on employee.AddressId equals address.AddressId
                            join employeeSSN in _db.EmployeeSsns on employee.EmployeeSsnid equals employeeSSN.EmployeeSsnid into employeeSSNJoin
                            where employee.EmployeeId == vEmployeeID
                            from employeeSSN in employeeSSNJoin.DefaultIfEmpty()
                            where employee.EmployeeId == vEmployeeID
                            let transferHistory = _db.TransferHistories.Where(th => th.EmploymentId == employment.EmploymentId && th.IsCurrent).FirstOrDefault()
                            let employmentJobTitle = _db.EmploymentJobTitles.FirstOrDefault(ejt => ejt.EmploymentId == employment.EmploymentId && ejt.IsCurrent)
                            let jobTitle = _db.JobTitles.FirstOrDefault(jt => jt.JobTitleId == employmentJobTitle.JobTitleId)
                            let diary = _db.Diaries.Where(d => d.DiaryName == "Agent" && d.EmploymentId == employment.EmploymentId).OrderByDescending(d => d.DiaryDate).FirstOrDefault()
                            select new OputAgent
                            {
                                EmployeeID = employee.EmployeeId,
                                EmploymentID = employment.EmploymentId,
                                EmployeeStatus = employment.EmployeeStatus,
                                CompanyID = employment.CompanyId,
                                CompanyName = company.CompanyName,
                                GEID = employee.Geid,
                                LastName = employee.LastName,
                                FirstName = employee.FirstName,
                                MiddleName = employee.MiddleName,
                                JobTitle = jobTitle.JobTitle1,
                                JobDate = employmentJobTitle.JobTitleDate,
                                EmployeeSSN = employeeSSN != null ? employeeSSN.EmployeeSsn1 : "",
                                Soeid = employee.Soeid,
                                Address1 = address.Address1,
                                Address2 = address.Address2,
                                City = address.City,
                                State = address.State,
                                Zip = address.Zip,
                                Phone = address.Phone,
                                Email = employment.Email,
                                CERequired = employment.Cerequired,
                                ExcludeFromReports = employee.ExcludeFromRpts,
                                DateOfBirth = employee.DateOfBirth,
                                LicenseIncentive = employment.LicenseIncentive,
                                LicenseLevel = employment.LicenseLevel,
                                IsLicenseincentiveSecondChance = employment.SecondChance,
                                NationalProdercerNumber = employee.NationalProducerNumber,
                                DiarySOEID = diary != null ? diary.Soeid : null,
                                DiaryEntryName = diary != null ? diary.DiaryName : null,
                                DiaryEntryDate = diary != null ? diary.DiaryDate : null,
                                DiaryNotes = diary != null ? diary.Notes : null,
                                BranchCode = transferHistory != null ? transferHistory.BranchCode : null,
                            };

                var agent = query.AsNoTracking().Single();

                var sql = @"
                       SELECT TOP 1 1 AS Hierarchy,
                        rtrim(e1.lastname) + ', ' + e1.FirstName AS MgrName, j.JobTitle AS MgrTitle,
                        h1.BranchCode AS MgrDeptCode, b1.Name AS MgrDeptName, b1.Address1 AS MgrDeptAddress1, b1.Address2 AS MgrDeptAddress2, 
                        b1.City AS MgrDeptCity, b1.State AS MgrDeptState, b1.Zip_Code AS MgrDeptZip, b1.Phone AS MgrDeptPhone, b1.Fax AS MgrDeptFax, m1.Email AS MgrEmail
                        FROM [dbo].[Employment] m
                        INNER JOIN [dbo].[Employment] m1 ON m.H1EmploymentID = m1.EmploymentID AND m.EmploymentID = @EmploymentID
                        INNER JOIN [dbo].[EmploymentJobTitle] ej ON m1.EmploymentID = ej.EmploymentID AND ej.[IsCurrent] = 1
                        INNER JOIN [dbo].[JobTitles] j ON ej.JobTitleID = j.JobTitleID
                        INNER JOIN [dbo].[Employee] e1 ON m1.EmployeeID = e1.EmployeeID 
                        INNER JOIN [dbo].[TransferHistory] h1 ON m1.EmploymentID = h1.EmploymentID AND h1.IsCurrent = 1
                        LEFT OUTER JOIN [dbo].[BIF] b1 ON RIGHT(h1.BranchCode,8) = RIGHT(b1.HR_Department_ID,8)

                        UNION

                        SELECT TOP 1 2 AS Hierarchy,
                        rtrim(e1.lastname) + ', ' + e1.FirstName AS MgrName, j.JobTitle AS MgrTitle,
                        h1.BranchCode AS MgrDeptCode, b1.Name AS MgrDeptName, b1.Address1 AS MgrDeptAddress1, b1.Address2 AS MgrDeptAddress2, 
                        b1.City AS MgrDeptCity, b1.State AS MgrDeptState, b1.Zip_Code AS MgrDeptZip, b1.Phone AS MgrDeptPhone, b1.Fax AS MgrDeptFax, m1.Email AS MgrEmail
                        FROM [dbo].[Employment] m
                        INNER JOIN [dbo].[Employment] m1 ON m.H2EmploymentID = m1.EmploymentID AND m.EmploymentID = @EmploymentID
                        INNER JOIN [dbo].[EmploymentJobTitle] ej ON m1.EmploymentID = ej.EmploymentID AND ej.[IsCurrent] = 1
                        INNER JOIN [dbo].[JobTitles] j ON ej.JobTitleID = j.JobTitleID
                        INNER JOIN [dbo].[Employee] e1 ON m1.EmployeeID = e1.EmployeeID 
                        INNER JOIN [dbo].[TransferHistory] h1 ON m1.EmploymentID = h1.EmploymentID AND h1.IsCurrent = 1
                        LEFT OUTER JOIN [dbo].[BIF] b1 ON RIGHT(h1.BranchCode,8) = RIGHT(b1.HR_Department_ID,8)

                        UNION

                        SELECT TOP 1 3 AS Hierarchy,
                        rtrim(e1.lastname) + ', ' + e1.FirstName AS MgrName, j.JobTitle AS MgrTitle,
                        h1.BranchCode AS MgrDeptCode, b1.Name AS MgrDeptName, b1.Address1 AS MgrDeptAddress1, b1.Address2 AS MgrDeptAddress2, 
                        b1.City AS MgrDeptCity, b1.State AS MgrDeptState, b1.Zip_Code AS MgrDeptZip, b1.Phone AS MgrDeptPhone, b1.Fax AS MgrDeptFax, m1.Email AS MgrEmail
                        FROM [dbo].[Employment] m
                        INNER JOIN [dbo].[Employment] m1 ON m.H3EmploymentID = m1.EmploymentID AND m.EmploymentID = @EmploymentID
                        INNER JOIN [dbo].[EmploymentJobTitle] ej ON m1.EmploymentID = ej.EmploymentID AND ej.[IsCurrent] = 1
                        INNER JOIN [dbo].[JobTitles] j ON ej.JobTitleID = j.JobTitleID
                        INNER JOIN [dbo].[Employee] e1 ON m1.EmployeeID = e1.EmployeeID 
                        INNER JOIN [dbo].[TransferHistory] h1 ON m1.EmploymentID = h1.EmploymentID AND h1.IsCurrent = 1
                        LEFT OUTER JOIN [dbo].[BIF] b1 ON RIGHT(h1.BranchCode,8) = RIGHT(b1.HR_Department_ID,8)

                        UNION

                        SELECT TOP 1 4 AS Hierarchy,
                        rtrim(e1.lastname) + ', ' + e1.FirstName AS MgrName, j.JobTitle AS MgrTitle,
                        h1.BranchCode AS MgrDeptCode, b1.Name AS MgrDeptName, b1.Address1 AS MgrDeptAddress1, b1.Address2 AS MgrDeptAddress2, 
                        b1.City AS MgrDeptCity, b1.State AS MgrDeptState, b1.Zip_Code AS MgrDeptZip, b1.Phone AS MgrDeptPhone, b1.Fax AS MgrDeptFax, m1.Email AS MgrEmail
                        FROM
                        [dbo].[Employment] m
                        INNER JOIN
                        [dbo].[Employment] m1
                        ON m.H4EmploymentID = m1.EmploymentID AND m.EmploymentID = @EmploymentID
                        INNER JOIN
                        [dbo].[EmploymentJobTitle] ej
                        ON m1.EmploymentID = ej.EmploymentID AND ej.[IsCurrent] = 1
                        INNER JOIN
                        [dbo].[JobTitles] j
                        ON ej.JobTitleID = j.JobTitleID
                        INNER JOIN
                        [dbo].[Employee] e1
                        ON m1.EmployeeID = e1.EmployeeID 
                        INNER JOIN
                        [dbo].[TransferHistory] h1
                        ON m1.EmploymentID = h1.EmploymentID AND h1.IsCurrent = 1
                        LEFT OUTER JOIN
                        [dbo].[BIF] b1
                        ON RIGHT(h1.BranchCode,8) = RIGHT(b1.HR_Department_ID,8)
                        UNION

                        SELECT TOP 1 5 AS Hierarchy,
                        rtrim(e1.lastname) + ', ' + e1.FirstName AS MgrName, j.JobTitle AS MgrTitle,
                        h1.BranchCode AS MgrDeptCode, b1.Name AS MgrDeptName, b1.Address1 AS MgrDeptAddress1, b1.Address2 AS MgrDeptAddress2, 
                        b1.City AS MgrDeptCity, b1.State AS MgrDeptState, b1.Zip_Code AS MgrDeptZip, b1.Phone AS MgrDeptPhone, b1.Fax AS MgrDeptFax, m1.Email AS MgrEmail
                        FROM
                        [dbo].[Employment] m
                        INNER JOIN
                        [dbo].[Employment] m1
                        ON m.H5EmploymentID = m1.EmploymentID AND m.EmploymentID = @EmploymentID
                        INNER JOIN
                        [dbo].[EmploymentJobTitle] ej
                        ON m1.EmploymentID = ej.EmploymentID AND ej.[IsCurrent] = 1
                        INNER JOIN
                        [dbo].[JobTitles] j
                        ON ej.JobTitleID = j.JobTitleID
                        INNER JOIN
                        [dbo].[Employee] e1
                        ON m1.EmployeeID = e1.EmployeeID 
                        INNER JOIN
                        [dbo].[TransferHistory] h1
                        ON m1.EmploymentID = h1.EmploymentID AND h1.IsCurrent = 1
                        LEFT OUTER JOIN
                        [dbo].[BIF] b1
                        ON RIGHT(h1.BranchCode,8) = RIGHT(b1.HR_Department_ID,8)
                        UNION

                        SELECT TOP 1 6 AS Hierarchy,
                        rtrim(e1.lastname) + ', ' + e1.FirstName AS MgrName, j.JobTitle AS MgrTitle,
                        h1.BranchCode AS MgrDeptCode, b1.Name AS MgrDeptName, b1.Address1 AS MgrDeptAddress1, b1.Address2 AS MgrDeptAddress2, 
                        b1.City AS MgrDeptCity, b1.State AS MgrDeptState, b1.Zip_Code AS MgrDeptZip, b1.Phone AS MgrDeptPhone, b1.Fax AS MgrDeptFax, m1.Email AS MgrEmail
                        FROM
                        [dbo].[Employment] m
                        INNER JOIN
                        [dbo].[Employment] m1
                        ON m.H6EmploymentID = m1.EmploymentID AND m.EmploymentID = @EmploymentID
                        INNER JOIN
                        [dbo].[EmploymentJobTitle] ej
                        ON m1.EmploymentID = ej.EmploymentID AND ej.[IsCurrent] = 1
                        INNER JOIN
                        [dbo].[JobTitles] j
                        ON ej.JobTitleID = j.JobTitleID
                        INNER JOIN
                        [dbo].[Employee] e1
                        ON m1.EmployeeID = e1.EmployeeID 
                        INNER JOIN
                        [dbo].[TransferHistory] h1
                        ON m1.EmploymentID = h1.EmploymentID AND h1.IsCurrent = 1
                        LEFT OUTER JOIN
                        [dbo].[BIF] b1
                        ON RIGHT(h1.BranchCode,8) = RIGHT(b1.HR_Department_ID,8)

                        ORDER BY Hierarchy";

                var parameters = new[] { new SqlParameter("@EmploymentID", agent.EmploymentID) };

                var queryHierarchyResults = _db.OputAgentHiearchy
                    .FromSqlRaw(sql, parameters)
                    .ToList();

                agent.MgrHiearchy = queryHierarchyResults;
                agent.AgentLicenseAppointments = FillAgentLicenseAppointment(agent.EmploymentID);

                var queryBranchInfo = from bif in _db.Bifs
                                      where bif.HrDepartmentId == agent.BranchCode.Substring(agent.BranchCode.Length - 8)
                                      select new
                                      {
                                          HrDeptmentID = bif.HrDepartmentId != null ? bif.HrDepartmentId : "",
                                          OfficeName = bif.Name,
                                          ScoreNumber = bif.ScoreNumber != null ? bif.ScoreNumber : "",
                                          StreetAddress1 = bif.Address1,
                                          StreetAddress2 = bif.Address2,
                                          StreetZip = bif.ZipCode,
                                          CustomerPhone = bif.Phone,
                                          FaxNumber = bif.Fax,
                                          Email = bif.HrDepartmentId != null ? bif.HrDepartmentId.Substring(bif.HrDepartmentId.Length - 8) + "@xxx.onemainfinancial.com" : ""
                                      };
                var branchInfo = queryBranchInfo.AsNoTracking().FirstOrDefault();

                agent.BranchDeptScoreNumber = branchInfo.ScoreNumber != null ? branchInfo.ScoreNumber : "";
                agent.BranchDeptNumber = branchInfo.HrDeptmentID != null ? branchInfo.HrDeptmentID : "";
                agent.BranchDeptName = branchInfo.OfficeName;
                agent.BranchDeptStreet1 = branchInfo.StreetAddress1;
                agent.BranchDeptStreet2 = branchInfo.StreetAddress2;
                agent.BranchDeptStreetZip = branchInfo.StreetZip;
                agent.BranchDeptStreetCity = agent.City;
                agent.BranchDeptStreetState = agent.State;
                agent.BranchDeptPhone = branchInfo.CustomerPhone;
                agent.BranchDeptFax = branchInfo.FaxNumber;
                agent.BranchDeptEmail = branchInfo.Email;

                // Employment History
                var agentEmpTransHistory = GetEmploymentHistoryInfo(agent.EmploymentID);

                agent.EmploymentHistory = agentEmpTransHistory.AgentEmploymentItems;
                agent.TransferHistory = agentEmpTransHistory.AgentTransferItems;
                agent.CompayRequirementsHistory = agentEmpTransHistory.CompayRequirementsItems;
                agent.EmploymentJobTitleHistory = agentEmpTransHistory.EmploymentJobTitleItems;

                result.Success = true;
                result.ObjData = agent;
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            };
        }

        /// <summary>
        ///  <param name="@vchEmploymentID" int>606709</param>   
        ///  <returns>
        ///     "licenseID": null,
        ///     "licenseState": "MO",
        ///     "lineOfAuthority": "CL",
        ///     "licenseStatus": "Active",
        ///     "employmentID": 606709,
        ///     "licenseName": "CREDIT",
        ///     "licenseNumber": "3002117601",
        ///     "resNoneRes": null,
        ///     "originalIssueDate": "2022-08-30T00:00:00",
        ///     "lineOfAuthIssueDate": "2022-08-30T00:00:00",
        ///     "licenseEffectiveDate": "2022-08-30T00:00:00",
        ///     "licenseExpirationDate": "2025-05-30T00:00:00"
        /// </returns>
        public ReturnResult GetLicenses(int vEmploymentID)
        {
            var result = new ReturnResult();
            try
            {
                var query = from employeeLicense in _db.EmployeeLicenses
                            join license in _db.Licenses on employeeLicense.LicenseId equals license.LicenseId
                            join lineOfAuthority in _db.LineOfAuthorities on license.LineOfAuthorityId equals lineOfAuthority.LineOfAuthorityId
                            where employeeLicense.EmploymentId == vEmploymentID
                            select new OputAgentLicenses
                            {
                                //LicenseID = (int)employeeLicense.LicenseId == 0 ? 0 : employeeLicense.LicenseId,
                                LicenseState = license.StateProvinceAbv,
                                LineOfAuthority = lineOfAuthority.LineOfAuthorityAbv,
                                LicenseStatus = employeeLicense.LicenseStatus,
                                EmploymentID = employeeLicense.EmploymentId,
                                LicenseName = license.LicenseName,
                                LicenseNumber = employeeLicense.LicenseNumber,
                                //ResNoneRes = (bool)employeeLicense.NonResident ? "Res" : "None",
                                OriginalIssueDate = employeeLicense.LicenseIssueDate,
                                LineOfAuthIssueDate = employeeLicense.LineOfAuthorityIssueDate,
                                LicenseEffectiveDate = employeeLicense.LicenseEffectiveDate,
                                LicenseExpirationDate = employeeLicense.LicenseExpireDate
                            };

                var licenses = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = licenses;
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }
        }

        /// <summary>
        ///  <param name="@vchEmploymentID" int>606709</param>   
        ///  <returns>
        ///     "employeeAppointmentID": 294636,
        ///     "appointmentEffectiveDate": "2022-09-02T00:00:00",
        ///     "appointmentStatus": "Active",
        ///     "employeeLicenseID": 1176643,
        ///     "carrierDate": null,
        ///     "appointmentExpireDate": null,
        ///     "appointmentTerminationDate": null,
        ///     "companyID": 6,
        ///     "retentionDate": null
        /// </returns>    
        public ReturnResult GetAppointments(int vEmploymentID)
        {
            var result = new ReturnResult();
            try
            {
                var employeeLicenses = _db.EmployeeLicenses
                       .Where(x => x.EmploymentId == vEmploymentID)
                       .Select(x => x.EmployeeLicenseId)
                       .ToList();

                var query = from appointment in _db.EmployeeAppointments
                            where employeeLicenses.Contains((int)appointment.EmployeeLicenseId)
                            select new OputAgentAppointments
                            {
                                EmployeeAppointmentID = appointment.EmployeeAppointmentId,
                                EmployeeLicenseID = (int)appointment.EmployeeLicenseId,
                                AppointmentEffectiveDate = appointment.AppointmentEffectiveDate,
                                AppointmentStatus = appointment.AppointmentStatus,
                                CarrierDate = appointment.CarrierDate,
                                AppointmentExpireDate = appointment.AppointmentExpireDate,
                                AppointmentTerminationDate = appointment.AppointmentTerminationDate,
                                CompanyID = appointment.CompanyId,
                                RetentionDate = appointment.RetentionDate
                            };

                var appointments = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = appointments;
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }
        }

        /// <summary>
        ///  <param name="@vchEmploymentID" int>606709</param>   
        ///  <returns>
        ///     "licenseID": null,
        ///     "licenseState": "MO",
        ///     "lineOfAuthority": "CL",
        ///     "licenseStatus": "Active",
        ///     "employmentID": 606709,
        ///     "licenseName": "CREDIT",
        ///     "licenseNumber": "3002117601",
        ///     "resNoneRes": null,
        ///     "originalIssueDate": "2022-08-30T00:00:00",
        ///     "lineOfAuthIssueDate": "2022-08-30T00:00:00",
        ///     "licenseEffectiveDate": "2022-08-30T00:00:00",
        ///     "licenseExpirationDate": "2025-05-30T00:00:00"
        /// </returns>
        public ReturnResult GetLicenseAppointments(int vEmploymentID)
        {
            var result = new ReturnResult();
            try
            {
                result.Success = true;
                result.ObjData = FillAgentLicenseAppointment(vEmploymentID);
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }
        }

        /// <summary>
        ///  <param name="@vchEmploymentID" int>606709</param>   
        ///  <param name="@EmploymentHistoryID" int>0</param>   
        ///  <param name="@TransferHistoryID" int>0</param>   
        ///  <param name="@EmploymentJobTitleID" int>0</param>   
        ///  <returns>
        ///     "agentEmploymentItems": [
        ///     {
        ///         "employmentHistoryID": 637583,
        ///         "hireDate": "2022-08-22T00:00:00",
        ///         "rehireDate": null,
        ///         "notifiedTermDate": null,
        ///         "hrTermDate": null,
        ///         "hrTermCode": null,
        ///         "isForCause": false,
        ///         "backgroundCheckStatus": "Completed",
        ///         "backGroundCheckNotes": "Sterling complete/clear 8/24/2022",
        ///         "isCurrent": true
        ///     }
        ///     ],
        ///         "agentTransferItems": [
        ///     {
        ///         "transferHistoryID": 907998,
        ///         "branchCode": "25800012",
        ///         "workStateAbv": "MO",
        ///         "resStateAbv": "MO",
        ///         "transferDate": "2022-08-22T01:03:33.447",
        ///         "state": null,
        ///         "isCurrent": true
        ///     }
        ///     ],
        ///         "compayRequirementsItems": [
        ///     {
        ///         "employmentCompanyRequirementID": 67906,
        ///         "assetIdString": "Optional Products - Membership Products",
        ///         "learningProgramStatus": "Completed",
        ///         "learningProgramEnrollmentDate": null,
        ///         "learningProgramCompletionDate": "2022-09-08T00:00:00"
        ///     },
        ///     {
        ///         "employmentCompanyRequirementID": 66716,
        ///         "assetIdString": "Optional Products - Sales",
        ///         "learningProgramStatus": "Completed",
        ///         "learningProgramEnrollmentDate": null,
        ///         "learningProgramCompletionDate": "2022-09-06T00:00:00"
        ///     },
        ///     {
        ///         "employmentCompanyRequirementID": 66805,
        ///         "assetIdString": "Optional Products – Non-Credit Insurance",
        ///         "learningProgramStatus": "Completed",
        ///         "learningProgramEnrollmentDate": null,
        ///         "learningProgramCompletionDate": "2022-09-09T00:00:00"
        ///     },
        ///     {
        ///         "employmentCompanyRequirementID": 66448,
        ///         "assetIdString": "Compliance Certification",
        ///         "learningProgramStatus": "Completed",
        ///         "learningProgramEnrollmentDate": null,
        ///         "learningProgramCompletionDate": "2022-08-25T00:00:00"
        ///     },
        ///     {
        ///         "employmentCompanyRequirementID": 66581,
        ///         "assetIdString": "Optional Products – Introduction to Credit Products",
        ///         "learningProgramStatus": "Completed",
        ///         "learningProgramEnrollmentDate": null,
        ///         "learningProgramCompletionDate": "2022-08-31T00:00:00"
        ///     }
        ///     ],
        ///         "employmentJobTitleItems": [
        ///     {
        ///         "employmentJobTitleID": 59500,
        ///         "employmentID": 606709,
        ///         "jobTitleDate": "2022-08-22T00:00:00",
        ///         "jobCode": "11657 ",
        ///         "jobTitle": "PERSONAL LOAN SPEC",
        ///         "isCurrent": true
        ///     }
        ///     ]
        /// </returns>    
        public ReturnResult GetEmploymentTransferHistory(int vEmploymentID, int vEmploymentHistoryID = 0, int vTransferHistoryID = 0, int vEmploymentJobTitleID = 0)
        {
            var result = new ReturnResult();
            try
            {
                var employmentTransferHistory = new OputAgentEmploymentTranserHistory();

                var queryEmplymentHistories = _db.EmploymentHistories
                                            .Where(h => (vEmploymentID == 0 || h.EmploymentId == vEmploymentID) &&
                                                        (vEmploymentHistoryID == 0 || h.EmploymentHistoryId == vEmploymentHistoryID))
                                            .OrderByDescending(h => h.IsCurrent)
                                            .ThenByDescending(h => h.HireDate)
                                            .Select(h => new AgentEmploymentItem
                                            {
                                                EmploymentHistoryID = h.EmploymentHistoryId,
                                                HireDate = h.HireDate,
                                                RehireDate = h.RehireDate,
                                                NotifiedTermDate = h.NotifiedTermDate,
                                                HRTermDate = h.HrtermDate,
                                                HRTermCode = h.HrtermCode,
                                                IsForCause = h.ForCause,
                                                BackgroundCheckStatus = h.BackgroundCheckStatus,
                                                BackGroundCheckNotes = h.BackGroundCheckNotes,
                                                IsCurrent = h.IsCurrent
                                            });

                var emplymentHistories = queryEmplymentHistories.AsNoTracking().ToList();
                employmentTransferHistory.AgentEmploymentItems = emplymentHistories;

                var queryTransferHistories = from th in _db.TransferHistories
                                             join m in _db.Employments on th.EmploymentId equals m.EmploymentId
                                             join e in _db.Employees on m.EmployeeId equals e.EmployeeId
                                             join a in _db.Addresses on e.AddressId equals a.AddressId into ea
                                             from a in ea.DefaultIfEmpty()
                                             where (th.EmploymentId == vEmploymentID || vEmploymentID == 0) &&
                                             (th.TransferHistoryId == vTransferHistoryID || vTransferHistoryID == 0)
                                             orderby th.TransferHistoryId descending
                                             select new AgentTransferItem
                                             {
                                                 TransferHistoryID = th.TransferHistoryId,
                                                 BranchCode = th.BranchCode,
                                                 WorkStateAbv = th.WorkStateAbv,
                                                 ResStateAbv = th.ResStateAbv ?? a.State,
                                                 TransferDate = th.TransferDate,
                                                 IsCurrent = th.IsCurrent,
                                                 //State = a != null ? a.State : null // Handling the case where 'a' might be null due to the left outer join
                                             };

                var transferHistories = queryTransferHistories.AsNoTracking().ToList();
                employmentTransferHistory.AgentTransferItems = transferHistories;

                var queryCompanyRequirementHistories = _db.EmploymentCompanyRequirements
                                                    .Where(ecr => ecr.EmploymentId == vEmploymentID)
                                                    .Select(ecr => new CompayRequirementsItem
                                                    {
                                                        EmploymentCompanyRequirementID = ecr.EmploymentCompanyRequirementId,
                                                        AssetIdString = ecr.AssetId,
                                                        LearningProgramStatus = ecr.LearningProgramStatus,
                                                        LearningProgramEnrollmentDate = ecr.LearningProgramEnrollmentDate,
                                                        LearningProgramCompletionDate = ecr.LearningProgramCompletionDate
                                                    });

                var companyRequirementHistories = queryCompanyRequirementHistories.AsNoTracking().ToList();
                employmentTransferHistory.CompayRequirementsItems = companyRequirementHistories;

                var queryJobTitleHistories = from ej in _db.EmploymentJobTitles
                                             join j in _db.JobTitles on ej.JobTitleId equals j.JobTitleId
                                             where (ej.EmploymentId == vEmploymentID || vEmploymentID == 0) &&
                                                   (ej.EmploymentJobTitleId == vEmploymentJobTitleID || vEmploymentJobTitleID == 0)
                                             orderby ej.IsCurrent descending, ej.JobTitleDate descending
                                             select new EmploymentJobTitleItem
                                             {
                                                 EmploymentJobTitleID = ej.EmploymentJobTitleId,
                                                 EmploymentID = ej.EmploymentId,
                                                 JobTitleDate = ej.JobTitleDate,
                                                 JobCode = j.JobCode,
                                                 JobTitle = j.JobTitle1,
                                                 IsCurrent = ej.IsCurrent
                                             };

                var jobTitleHistories = queryJobTitleHistories.AsNoTracking().ToList();
                employmentTransferHistory.EmploymentJobTitleItems = jobTitleHistories;

                result.Success = true;
                result.ObjData = employmentTransferHistory;
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }
        }

        /// <summary>
        ///  <param name="@vchEmploymentID" int>606709</param>   
        ///  <returns>
        ///     "contEducationRequirementID": 101413,
        ///     "educationStartDate": "1900-01-01T00:00:00",
        ///     "educationEndDate": "1900-01-01T00:00:00",
        ///     "requiredCreditHours": 0,
        ///     "isExempt": false
        /// </returns>
        public ReturnResult GetContEducationRequired(int vEmploymentID)
        {
            var result = new ReturnResult();
            try
            {
                var query = _db.ContEducationRequirements
                        .Join(_db.Employments, // The table to join
                              ce => ce.EmploymentId, // Join key from ContEducationRequirement
                              e => e.EmploymentId,   // Join key from Employment
                              (ce, e) => new { CE = ce, E = e }) // Result selector
                        .Where(x => (x.CE.EmploymentId ?? 0) == vEmploymentID && x.CE.IsExempt == false && x.E.Cerequired == true)
                        .Select(x => new OputAgentContEducationRequired
                        {
                            ContEducationRequirementID = x.CE.ContEducationRequirementId,
                            EducationStartDate = x.CE.EducationStartDate ?? new DateTime(1900, 1, 1),
                            EducationEndDate = x.CE.EducationEndDate ?? new DateTime(1900, 1, 1),
                            RequiredCreditHours = x.CE.RequiredCreditHours,
                            IsExempt = x.CE.IsExempt,
                        })
                        .OrderByDescending(x => x.EducationStartDate);

                var licenses = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = licenses;
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }
        }

        /// <summary>
        ///  <param name="@vchEmploymentID" int>606709</param>   
        ///  <returns>
        ///{
        ///     "techName": "TAMMY REMKE",
        ///     "soeid": "TREMKE",
        ///     "diaryID": 6560806,
        ///     "diaryDate": "2022-08-30T17:27:47",
        ///     "notes": "Sterling complete/clear 8/24/2022"
        ///},
        ///{
        ///     "techName": "TAMMY REMKE",
        ///     "soeid": "TREMKE",
        ///     "diaryID": 6560820,
        ///     "diaryDate": "2022-08-30T17:48:16",
        ///     "notes": "credit appl sub"
        ///},
        ///{
        ///     "techName": "TAMMY REMKE",
        ///     "soeid": "TREMKE",
        ///     "diaryID": 6562492,
        ///     "diaryDate": "2022-09-15T16:19:13",
        ///     "notes": "fw license"
        ///}
        /// </returns> 
        public ReturnResult GetDiary(int vEmploymentID = 0)
        {
            var result = new ReturnResult();
            try
            {
                var query = _db.Diaries
                        .Where(d => vEmploymentID == 0 || d.EmploymentId == vEmploymentID) // WHERE condition
                        .GroupJoin(_db.LicenseTeches, // Perform a left outer join
                            d => d.Soeid,
                            lt => lt.Soeid,
                            (d, ltGroup) => new { Diary = d, LicenseTech = ltGroup.DefaultIfEmpty() })
                        .SelectMany(
                            temp => temp.LicenseTech,
                            (temp, lt) => new
                            {
                                TechName = lt == null ? temp.Diary.Soeid : lt.FirstName + " " + lt.LastName, // ISNULL equivalent
                                temp.Diary.Soeid,
                                temp.Diary.DiaryId,
                                temp.Diary.DiaryDate,
                                temp.Diary.Notes
                            })
                        .OrderByDescending(d => d.DiaryDate) // ORDER BY DiaryDate DESC
                        .GroupBy(d => new { d.TechName, d.Soeid, d.DiaryId, d.DiaryDate, d.Notes }) // GROUP BY
                        .Select(g => new OputAgentDiary
                        {
                            TechName = g.Key.TechName,
                            SOEID = g.Key.Soeid,
                            DiaryID = g.Key.DiaryId,
                            DiaryDate = g.Key.DiaryDate,
                            Notes = g.Key.Notes
                        });

                var diaries = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = diaries;
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }
        }

        /// <summary>
        ///  <param name="@vchEmploymentID" int>606709</param>   
        ///  <returns>
        ///{
        ///     "letterDataID": 212689,
        ///     "letterName": "APP-New Start",
        ///     "createDate": "2022-08-24T13:51:47.073",
        ///     "sentDate": "2022-08-25T06:42:51"
        ///},
        ///{
        ///     "letterDataID": 215250,
        ///     "letterName": "APP-License Copy",
        ///     "createDate": "2022-09-15T16:19:39.477",
        ///     "sentDate": "2022-09-15T16:19:40.187"
        ///},
        ///{
        ///     "letterDataID": 215278,
        ///     "letterName": "APP-OK To Sell",
        ///     "createDate": "2022-09-16T01:23:06.13",
        ///     "sentDate": "2022-09-16T06:36:49"
        ///}
        /// </returns> 
        public ReturnResult GetCommunications(int vEmploymentID)
        {
            var result = new ReturnResult();
            try
            {
                var query = _db.EmploymentCommunications
                        .Where(ec => ec.EmploymentId == vEmploymentID)
                        .Join(_db.Communications,
                            ec => ec.CommunicationId,
                            c => c.CommunicationId,
                            (ec, c) => new OputAgentCommunication
                            {
                                LetterDataID = ec.EmploymentCommunicationId,
                                LetterName = c.DocTypeAbv + "-" + c.DocSubType, // CONCAT equivalent
                                CreateDate = ec.EmailCreateDate,
                                SentDate = ec.EmailSentDate
                            })
                        .OrderBy(result => result.CreateDate) // ORDER BY CreateDate
                        .ThenBy(result => result.LetterName);

                var communications = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = communications;
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }
        }
        public ReturnResult GetLicenseApplcationInfo(int vEmployeeLicenseID)
        {
            var result = new ReturnResult();
            try
            {
                var agentLicenseInformation = new OputAgentLicApplicationInfo();

                var queryLicApplication = from licenseApplication in _db.LicenseApplications
                                          where licenseApplication.ApplicationType == "Initial Application" && licenseApplication.EmployeeLicenseId == vEmployeeLicenseID
                                          orderby licenseApplication.SentToAgentDate descending, licenseApplication.LicenseApplicationId ascending
                                          select new AgentLicApplicationItem
                                          {
                                              LicenseApplicationID = licenseApplication.LicenseApplicationId,
                                              SentToAgentDate = licenseApplication.SentToAgentDate,
                                              RecFromAgentDate = licenseApplication.RecFromAgentDate,
                                              SentToStateDate = licenseApplication.SentToStateDate,
                                              RecFromStateDate = licenseApplication.RecFromStateDate,
                                              ApplicationStatus = licenseApplication.ApplicationStatus,
                                              ApplicationType = licenseApplication.ApplicationType
                                          };

                var licenseApplications = queryLicApplication.AsNoTracking().ToList();
                agentLicenseInformation.LicenseApplicationItems = licenseApplications;

                var queryPreEducations = from epe in _db.EmployeeLicenseePreEducations
                                         join pe in _db.PreEducations on epe.PreEducationId equals pe.PreEducationId
                                         where epe.EmployeeLicenseId == vEmployeeLicenseID
                                         orderby epe.EducationStartDate descending, pe.EducationName ascending
                                         select new AgentLicPreEducationItem
                                         {
                                             EmployeeLicensePreEducationID = epe.EmployeeLicensePreEducationId,
                                             Status = epe.Status,
                                             EducationStartDate = epe.EducationStartDate,
                                             EducationEndDate = epe.EducationEndDate,
                                             PreEducationID = epe.PreEducationId,
                                             CompanyID = epe.CompanyId,
                                             EducationName = pe.EducationName + " - " + (pe.DeliveryMethod ?? ""),
                                             EmployeeLicenseID = epe.EmployeeLicenseId,
                                             AdditionalNotes = epe.AdditionalNotes
                                         };

                var preEducations = queryPreEducations.AsNoTracking().ToList();
                agentLicenseInformation.LicensePreEducationItems = preEducations;

                var queryPreExams = from x in _db.EmployeeLicensePreExams
                                    join e in _db.Exams on x.ExamId equals e.ExamId into exams
                                    from e in exams.DefaultIfEmpty()
                                    where x.EmployeeLicenseId == vEmployeeLicenseID
                                    orderby x.ExamScheduleDate descending, x.EmployeeLicenseId ascending
                                    select new AgentLicPreExamItem
                                    {
                                        EmployeeLicensePreExamID = x.EmployeeLicensePreExamId,
                                        EmployeeLicenseID = x.EmployeeLicenseId,
                                        Status = x.Status,
                                        ExamID = x.ExamId,
                                        ExamName = e != null ? e.ExamName : null,
                                        ExamScheduleDate = x.ExamScheduleDate,
                                        ExamTakenDate = x.ExamTakenDate,
                                        AdditionalNotes = x.AdditionalNotes
                                    };

                var preExams = queryPreExams.AsNoTracking().ToList();
                agentLicenseInformation.LicensePreExamItems = preExams;

                var queryRenewals = from licenseApplication in _db.LicenseApplications
                                    where licenseApplication.ApplicationType == "Renewal Application"
                                          && licenseApplication.EmployeeLicenseId == vEmployeeLicenseID
                                    select new AgentLicRenewalItem
                                    {
                                        EmployeeLicenseID = (int)licenseApplication.EmployeeLicenseId,
                                        LicenseApplicationID = licenseApplication.LicenseApplicationId,
                                        SentToAgentDate = licenseApplication.SentToAgentDate,
                                        RecFromAgentDate = licenseApplication.RecFromAgentDate,
                                        SentToStateDate = licenseApplication.SentToStateDate,
                                        RecFromStateDate = licenseApplication.RecFromStateDate,
                                        ApplicationStatus = licenseApplication.ApplicationStatus,
                                        ApplicationType = licenseApplication.ApplicationType,
                                        RenewalDate = licenseApplication.RenewalDate,
                                        RenewalMethod = licenseApplication.RenewalMethod
                                    };

                var renewals = queryRenewals.AsNoTracking().ToList();
                agentLicenseInformation.LicenseRenewalItems = renewals;

                result.Success = true;
                result.ObjData = agentLicenseInformation;
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }
        }
        public ReturnResult InsertAgent([FromBody] IputAgentInsert vInput)
        {
            var result = new ReturnResult();
            try
            {
                // TBD: Placeholder method to execute [uspAgentInsert] stored procedure

                result.Success = true;
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }

        }
        public ReturnResult InsertAgent_v2([FromBody] IputAgentInsert vInput)
        {
            var result = new ReturnResult();
            try
            {
                result.StatusCode = 200;

                using (var context = new AppDataContext())
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // Assume GetCurrentDate is a method to get the current date, similar to GETDATE()
                        var transferDate = DateTime.Now;

                        // Retrieve country based on StateProvinceAbv
                        var country = context.StateProvinces
                            .Where(sp => sp.StateProvinceAbv == vInput.State)
                            .Select(sp => sp.Country)
                            .FirstOrDefault();

                        // INSERT into EmployeeSSN and log audit
                        var employeeSSN = new EmployeeSsn { EmployeeSsn1 = vInput.EmployeeSSN };
                        context.EmployeeSsns.Add(employeeSSN);
                        context.SaveChanges();

                        // Log audit for EmployeeSSN insertion
                        int employeeSsnid = employeeSSN.EmployeeSsnid;

                        if (employeeSsnid > 0)
                            _utilityService.LogAudit("EmployeeSSN", employeeSsnid, vInput.UserSOEID, "INSERT", "EmployeeSSN", null, employeeSsnid.ToString());

                        // Address INSERT via stored procedure
                        var isAddressInsert = ExecuteAddressInsert(vInput, country);

                        // Employee INSERT
                        var employee = new Employee
                        {
                            FirstName = vInput.FirstName,
                            MiddleName = vInput.MiddleName,
                            LastName = vInput.LastName,
                            Alias = vInput.Alias,
                            Geid = vInput.GEID,
                            Soeid = vInput.SOEID,
                            DateOfBirth = vInput.DateOfBirth,
                            NationalProducerNumber = vInput.NationalProducerNumber,
                            EmployeeSsnid = employeeSSN.EmployeeSsnid,
                            //AddressId = addressId, // TBD: Placeholder for addressId
                            ExcludeFromRpts = vInput.ExcludeFromRpts
                        };
                        context.Employees.Add(employee);
                        context.SaveChanges();

                        // Log audit for Employee INSERT
                        int employeeId = employee.EmployeeId;

                        if (employeeId > 0)
                            _utilityService.LogAudit("Employee", employeeId, vInput.UserSOEID, "INSERT", "FirstName, MiddleName, LastName", null, employee.EmployeeId.ToString());

                        // Employment INSERT 
                        var isEmploymentInsert = ExecuteEmploymentInsert(vInput, employeeId);

                        transaction.Commit();

                        var empResult = GetAgentByEmployeeID(employeeId);
                        result.Success = true;
                        result.ObjData = empResult.ObjData;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        // Log or handle error
                        //_utilityService.ExecuteErrorHandling(); // Placeholder for error handling stored procedure call
                        result.StatusCode = 500;
                        result.ErrMessage = ex.Message;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }
        }
        #region Private Methods
        private bool ExecuteAddressInsert(IputAgentInsert vInput, string? vCountry = null)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("[uspAddressInsert]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@AddressType", SqlDbType.VarChar, 20).Value = "Agent";
                        command.Parameters.Add("@Address1", SqlDbType.VarChar, 50).Value = vInput.Address1;
                        command.Parameters.Add("@Address2", SqlDbType.VarChar, 50).Value = vInput.Address2;
                        command.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = vInput.City;
                        command.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = vInput.State;
                        command.Parameters.Add("@Phone", SqlDbType.VarChar, 13).Value = vInput.Phone;
                        command.Parameters.Add("@Country", SqlDbType.DateTime, 10).Value = vCountry;
                        command.Parameters.Add("@Zip", SqlDbType.VarChar, 12).Value = vInput.Zip;
                        command.Parameters.Add("@Fax", SqlDbType.VarChar, 13).Value = vInput.Fax;
                        command.Parameters.Add("@UserSOEID", SqlDbType.DateTime, 50).Value = vInput.UserSOEID;
                        //command.Parameters.Add("@ResStateAbv", SqlDbType.DateTime, 2).Value = vInput.ResStateAbv;

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ExecuteEmploymentInsert(IputAgentInsert vInput, int vEmployeeId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("[uspAgentEmploymentInsert]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //Employment
                        command.Parameters.Add("@EmployeeID", SqlDbType.VarChar, 25).Value = vEmployeeId;
                        command.Parameters.Add("@EmployeeStatus", SqlDbType.VarChar, 25).Value = vInput.EmployeeStatus;
                        command.Parameters.Add("@CompanyID", SqlDbType.Int, 0).Value = vInput.CompanyID;
                        command.Parameters.Add("@WorkPhone", SqlDbType.VarChar, 13).Value = vInput.WorkPhone;
                        command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = vInput.Email;
                        command.Parameters.Add("@LicenseIncentive", SqlDbType.VarChar, 25).Value = vInput.LicenseIncentive;
                        command.Parameters.Add("@LicenseLevel", SqlDbType.VarChar, 25).Value = vInput.LicenseLevel;

                        //EmploymentHistory
                        command.Parameters.Add("@HireDate", SqlDbType.DateTime, 10).Value = vInput.HireDate;
                        command.Parameters.Add("@BackgroundCheckStatus", SqlDbType.VarChar, 50).Value = vInput.BackgroundCheckStatus;
                        command.Parameters.Add("@BackgroundCheckNote", SqlDbType.VarChar, 500).Value = vInput.BackgroundCheckNote;

                        //TransferHistory
                        command.Parameters.Add("@BranchCode", SqlDbType.DateTime, 50).Value = vInput.BranchCode;
                        command.Parameters.Add("@ResStateAbv", SqlDbType.DateTime, 2).Value = vInput.ResStateAbv;
                        command.Parameters.Add("@WorkStateAbv", SqlDbType.DateTime, 2).Value = vInput.WorkStateAbv;
                        command.Parameters.Add("@UserSOEID", SqlDbType.DateTime, 50).Value = vInput.UserSOEID;

                        //EmploymentJobTitle
                        command.Parameters.Add("@JobTitleID", SqlDbType.DateTime, 50).Value = vInput.JobTitleID;
                        command.Parameters.Add("@JobTitleDate", SqlDbType.DateTime, 50).Value = vInput.JobTitleDate;

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private List<OputAgentLicenseAppointments> FillAgentLicenseAppointment(int vEmploymentID)
        {
            var queryEmployeeLicenses = from employeeLicense in _db.EmployeeLicenses
                                        join license in _db.Licenses on employeeLicense.LicenseId equals license.LicenseId
                                        join lineOfAuthority in _db.LineOfAuthorities on license.LineOfAuthorityId equals lineOfAuthority.LineOfAuthorityId
                                        where employeeLicense.EmploymentId == vEmploymentID
                                        select new OputAgentLicenseAppointments
                                        {
                                            EmployeeLicenseId = (int)employeeLicense.EmployeeLicenseId == 0 ? 0 : employeeLicense.EmployeeLicenseId,
                                            LicenseState = license.StateProvinceAbv,
                                            LineOfAuthority = lineOfAuthority.LineOfAuthorityAbv,
                                            LicenseStatus = employeeLicense.LicenseStatus,
                                            EmploymentID = employeeLicense.EmploymentId,
                                            LicenseName = license.LicenseName,
                                            LicenseNumber = employeeLicense.LicenseNumber,
                                            //ResNoneRes = (bool)employeeLicense.NonResident ? "Res" : "None",
                                            OriginalIssueDate = employeeLicense.LicenseIssueDate,
                                            LineOfAuthIssueDate = employeeLicense.LineOfAuthorityIssueDate,
                                            LicenseEffectiveDate = employeeLicense.LicenseEffectiveDate,
                                            LicenseExpirationDate = employeeLicense.LicenseExpireDate
                                        };

            var licenses = queryEmployeeLicenses.AsNoTracking().ToList();

            var employeeLicenses = _db.EmployeeLicenses
                   .Where(x => x.EmploymentId == vEmploymentID)
                   .Select(x => x.EmployeeLicenseId)
                   .ToList();

            var queryAppointments = from appointment in _db.EmployeeAppointments
                                    where employeeLicenses.Contains((int)appointment.EmployeeLicenseId)
                                    select new OputAgentAppointments
                                    {
                                        LicenseID = (int)appointment.EmployeeLicenseId,
                                        EmployeeAppointmentID = appointment.EmployeeAppointmentId,
                                        EmployeeLicenseID = (int)appointment.EmployeeLicenseId,
                                        AppointmentEffectiveDate = appointment.AppointmentEffectiveDate,
                                        AppointmentStatus = appointment.AppointmentStatus,
                                        CarrierDate = appointment.CarrierDate,
                                        AppointmentExpireDate = appointment.AppointmentExpireDate,
                                        AppointmentTerminationDate = appointment.AppointmentTerminationDate,
                                        CompanyID = appointment.CompanyId,
                                        RetentionDate = appointment.RetentionDate
                                    };

            var appointments = queryAppointments.AsNoTracking().ToList();

            var licenseAppointments = new List<OputAgentLicenseAppointments>();
            foreach (var license in licenses)
            {
                foreach (var appointment in appointments)
                {
                    if (appointment.EmployeeLicenseID == license.EmployeeLicenseId)
                    {
                        license.LicenseAppointments.Add(appointment);
                    }
                }
                licenseAppointments.Add(license);
            }

            return licenseAppointments;
        }
        private OputAgentEmploymentTranserHistory GetEmploymentHistoryInfo(int vEmploymentID)
        {
            var employmentTransferHistory = new OputAgentEmploymentTranserHistory();

            var queryEmplymentHistories = _db.EmploymentHistories
                                        .Where(h => h.EmploymentId == vEmploymentID)
                                        .OrderByDescending(h => h.HireDate)
                                        .Select(h => new AgentEmploymentItem
                                        {
                                            EmploymentHistoryID = h.EmploymentHistoryId,
                                            HireDate = h.HireDate,
                                            RehireDate = h.RehireDate,
                                            NotifiedTermDate = h.NotifiedTermDate,
                                            HRTermDate = h.HrtermDate,
                                            HRTermCode = h.HrtermCode,
                                            IsForCause = h.ForCause,
                                            BackgroundCheckStatus = h.BackgroundCheckStatus,
                                            BackGroundCheckNotes = h.BackGroundCheckNotes,
                                            IsCurrent = h.IsCurrent
                                        });


            var emplymentHistories = queryEmplymentHistories.AsNoTracking().ToList();
            employmentTransferHistory.AgentEmploymentItems = emplymentHistories;

            var queryTransferHistories = from th in _db.TransferHistories
                                         join m in _db.Employments on th.EmploymentId equals m.EmploymentId
                                         join e in _db.Employees on m.EmployeeId equals e.EmployeeId
                                         join a in _db.Addresses on e.AddressId equals a.AddressId into ea
                                         from a in ea.DefaultIfEmpty()
                                         where (th.EmploymentId == vEmploymentID || vEmploymentID == 0)
                                         orderby th.TransferHistoryId descending
                                         select new AgentTransferItem
                                         {
                                             TransferHistoryID = th.TransferHistoryId,
                                             BranchCode = th.BranchCode,
                                             WorkStateAbv = th.WorkStateAbv,
                                             ResStateAbv = th.ResStateAbv ?? a.State,
                                             TransferDate = th.TransferDate,
                                             IsCurrent = th.IsCurrent,
                                             //State = a != null ? a.State : null // Handling the case where 'a' might be null due to the left outer join
                                         };

            var transferHistories = queryTransferHistories.AsNoTracking().ToList();
            employmentTransferHistory.AgentTransferItems = transferHistories;

            var queryCompanyRequirementHistories = _db.EmploymentCompanyRequirements
                                                .Where(ecr => ecr.EmploymentId == vEmploymentID)
                                                .Select(ecr => new CompayRequirementsItem
                                                {
                                                    EmploymentCompanyRequirementID = ecr.EmploymentCompanyRequirementId,
                                                    AssetIdString = ecr.AssetId,
                                                    LearningProgramStatus = ecr.LearningProgramStatus,
                                                    LearningProgramEnrollmentDate = ecr.LearningProgramEnrollmentDate,
                                                    LearningProgramCompletionDate = ecr.LearningProgramCompletionDate
                                                });

            var companyRequirementHistories = queryCompanyRequirementHistories.AsNoTracking().ToList();
            employmentTransferHistory.CompayRequirementsItems = companyRequirementHistories;

            var queryJobTitleHistories = from ej in _db.EmploymentJobTitles
                                         join j in _db.JobTitles on ej.JobTitleId equals j.JobTitleId
                                         where (ej.EmploymentId == vEmploymentID || vEmploymentID == 0)
                                         orderby ej.IsCurrent descending, ej.JobTitleDate descending
                                         select new EmploymentJobTitleItem
                                         {
                                             EmploymentJobTitleID = ej.EmploymentJobTitleId,
                                             EmploymentID = ej.EmploymentId,
                                             JobTitleDate = ej.JobTitleDate,
                                             JobCode = j.JobCode,
                                             JobTitle = j.JobTitle1,
                                             IsCurrent = ej.IsCurrent
                                         };

            var jobTitleHistories = queryJobTitleHistories.AsNoTracking().ToList();
            employmentTransferHistory.EmploymentJobTitleItems = jobTitleHistories;

            return employmentTransferHistory;
        }
        #endregion
    }
}
