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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Data.SqlTypes;
using System.Collections.Generic;

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
            //_connectionString = _config.GetConnectionString(name: "DefaultConnection");
            var environment = _config["Environment"];
            if (string.IsNullOrEmpty(environment))
            {
                throw new InvalidOperationException("Environment is not specified in the configuration.");
            }

            // Get the connection string for the designated environment
            _connectionString = _config.GetSection($"EnvironmentSettings:{environment}:DefaultConnection").Value;

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException($"Connection string for environment '{environment}' is not configured.");
            }
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
                            //let diary = _db.Diaries.Where(d => d.DiaryName == "Agent" && d.EmploymentId == employment.EmploymentId).OrderByDescending(d => d.DiaryDate).FirstOrDefault()
                            let diary = (from d in _db.Diaries
                                         join lt in _db.LicenseTeches on d.Soeid equals lt.Soeid
                                         where d.DiaryName == "Agent" && d.EmploymentId == employment.EmploymentId
                                         orderby d.DiaryDate descending
                                         select new
                                         {
                                             Diary = d,
                                             DiaryName = d.DiaryName,
                                             DiaryDate = d.DiaryDate,
                                             Notes = d.Notes,
                                             Soeid = d.Soeid,
                                             TechName = lt.FirstName + " " + lt.LastName
                                         }).FirstOrDefault()
                            let licenseTech = _db.LicenseTeches.Where(lt => lt.TeamNum == employee.Geid).FirstOrDefault()
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
                                Suffix = employee.Suffix,
                                Alias = employee.Alias,
                                MiddleName = employee.MiddleName,
                                JobTitle = jobTitle.JobTitle1,
                                JobDate = employmentJobTitle.JobTitleDate,
                                EmployeeSSN = employeeSSN != null ? employeeSSN.EmployeeSsn1 : "",
                                Soeid = licenseTech.Soeid,
                                Address1 = address.Address1,
                                Address2 = address.Address2,
                                City = address.City,
                                State = address.State,
                                Zip = address.Zip,
                                HomePhone = address.Phone,
                                FaxPhone = address.Fax,
                                WorkPhone = employment.WorkPhone,
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
                                DiaryTechName = diary != null ? diary.TechName : null,
                                BranchCode = transferHistory != null ? transferHistory.BranchCode : null,
                                WorkStateAbv = transferHistory != null ? transferHistory.WorkStateAbv : null,
                                ResStateAbv = transferHistory != null ? transferHistory.ResStateAbv : null
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
                        FROM [dbo].[Employment] m
                        INNER JOIN [dbo].[Employment] m1 ON m.H4EmploymentID = m1.EmploymentID AND m.EmploymentID = @EmploymentID
                        INNER JOIN [dbo].[EmploymentJobTitle] ej ON m1.EmploymentID = ej.EmploymentID AND ej.[IsCurrent] = 1
                        INNER JOIN [dbo].[JobTitles] j ON ej.JobTitleID = j.JobTitleID
                        INNER JOIN [dbo].[Employee] e1 ON m1.EmployeeID = e1.EmployeeID 
                        INNER JOIN [dbo].[TransferHistory] h1 ON m1.EmploymentID = h1.EmploymentID AND h1.IsCurrent = 1
                        LEFT OUTER JOIN [dbo].[BIF] b1 ON RIGHT(h1.BranchCode,8) = RIGHT(b1.HR_Department_ID,8)

                        UNION

                        SELECT TOP 1 5 AS Hierarchy,
                        rtrim(e1.lastname) + ', ' + e1.FirstName AS MgrName, j.JobTitle AS MgrTitle,
                        h1.BranchCode AS MgrDeptCode, b1.Name AS MgrDeptName, b1.Address1 AS MgrDeptAddress1, b1.Address2 AS MgrDeptAddress2, 
                        b1.City AS MgrDeptCity, b1.State AS MgrDeptState, b1.Zip_Code AS MgrDeptZip, b1.Phone AS MgrDeptPhone, b1.Fax AS MgrDeptFax, m1.Email AS MgrEmail
                        FROM [dbo].[Employment] m
                        INNER JOIN [dbo].[Employment] m1 ON m.H5EmploymentID = m1.EmploymentID AND m.EmploymentID = @EmploymentID
                        INNER JOIN [dbo].[EmploymentJobTitle] ej ON m1.EmploymentID = ej.EmploymentID AND ej.[IsCurrent] = 1
                        INNER JOIN [dbo].[JobTitles] j ON ej.JobTitleID = j.JobTitleID
                        INNER JOIN [dbo].[Employee] e1 ON m1.EmployeeID = e1.EmployeeID 
                        INNER JOIN [dbo].[TransferHistory] h1 ON m1.EmploymentID = h1.EmploymentID AND h1.IsCurrent = 1
                        LEFT OUTER JOIN [dbo].[BIF] b1 ON RIGHT(h1.BranchCode,8) = RIGHT(b1.HR_Department_ID,8)

                        UNION

                        SELECT TOP 1 6 AS Hierarchy,
                        rtrim(e1.lastname) + ', ' + e1.FirstName AS MgrName, j.JobTitle AS MgrTitle,
                        h1.BranchCode AS MgrDeptCode, b1.Name AS MgrDeptName, b1.Address1 AS MgrDeptAddress1, b1.Address2 AS MgrDeptAddress2, 
                        b1.City AS MgrDeptCity, b1.State AS MgrDeptState, b1.Zip_Code AS MgrDeptZip, b1.Phone AS MgrDeptPhone, b1.Fax AS MgrDeptFax, m1.Email AS MgrEmail
                        FROM [dbo].[Employment] m
                        INNER JOIN [dbo].[Employment] m1 ON m.H6EmploymentID = m1.EmploymentID AND m.EmploymentID = @EmploymentID
                        INNER JOIN [dbo].[EmploymentJobTitle] ej ON m1.EmploymentID = ej.EmploymentID AND ej.[IsCurrent] = 1
                        INNER JOIN [dbo].[JobTitles] j ON ej.JobTitleID = j.JobTitleID
                        INNER JOIN [dbo].[Employee] e1 ON m1.EmployeeID = e1.EmployeeID 
                        INNER JOIN [dbo].[TransferHistory] h1 ON m1.EmploymentID = h1.EmploymentID AND h1.IsCurrent = 1
                        LEFT OUTER JOIN [dbo].[BIF] b1 ON RIGHT(h1.BranchCode,8) = RIGHT(b1.HR_Department_ID,8)

                        ORDER BY Hierarchy";

                var parameters = new[] { new SqlParameter("@EmploymentID", agent.EmploymentID) };

                // Hierarchy Information
                var queryHierarchyResults = _db.OputAgentHiearchy
                                            .FromSqlRaw(sql, parameters)
                                            .AsNoTracking()
                                            .ToList();

                agent.MgrHiearchy = queryHierarchyResults;
                agent.AgentLicenseAppointments = FillAgentLicenseAppointment(agent.EmploymentID);

                // Branch Information
                if (agent.BranchCode != null && agent.BranchCode != "00000000")
                {
                    var queryBranchInfo = from bif in _db.Bifs
                                          where bif.HrDepartmentId == agent.BranchCode.Substring(agent.BranchCode.Length - 8)
                                          select new
                                          {
                                              HrDeptmentID = bif.HrDepartmentId != null ? bif.HrDepartmentId : "",
                                              OfficeName = bif.Name,
                                              ScoreNumber = bif.ScoreNumber != null ? bif.ScoreNumber : "",
                                              StreetAddress1 = bif.Address1,
                                              StreetAddress2 = bif.Address2,
                                              City = bif.City,
                                              State = bif.State,
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
                    agent.BranchDeptStreetCity = branchInfo.City;
                    agent.BranchDeptStreetState = branchInfo.State;
                    agent.BranchDeptPhone = branchInfo.CustomerPhone;
                    agent.BranchDeptFax = branchInfo.FaxNumber;
                    agent.BranchDeptEmail = branchInfo.Email;
                }

                // Employment History
                var agentEmpTransHistory = GetEmploymentHistoryInfo(agent.EmploymentID);

                agent.EmploymentHistory = agentEmpTransHistory.AgentEmploymentItems;
                agent.TransferHistory = agentEmpTransHistory.AgentTransferItems;
                agent.CompayRequirementsHistory = agentEmpTransHistory.CompayRequirementsItems;
                agent.EmploymentJobTitleHistory = agentEmpTransHistory.EmploymentJobTitleItems;

                // Continuing Education
                var agentContEduRequirement = FillContEducationItems(agent.EmploymentID);

                agent.ContEduRequiredItems = agentContEduRequirement.Item1.ToList();
                agent.ContEduCompletedItems = agentContEduRequirement.Item2.ToList();

                //Diary Information
                var agentDiaryInfo = FillDiaryInfo(agent.EmploymentID);

                agent.DiaryCreatedByItems = agentDiaryInfo.Item1.ToList();
                agent.DiaryItems = agentDiaryInfo.Item2.ToList();

                //Employment Communication
                var queryEmploymentCommunications = (from ec in _db.EmploymentCommunications
                                                     join c in _db.Communications on ec.CommunicationId equals c.CommunicationId
                                                     where ec.EmploymentId == agent.EmploymentID
                                                     orderby ec.EmailCreateDate, c.DocTypeAbv + "-" + c.DocSubType
                                                     select new EmploymentCommunicationItem
                                                     {
                                                         EmploymentCommunicationID = ec.EmploymentCommunicationId,
                                                         LetterName = c.DocTypeAbv + "-" + c.DocSubType,
                                                         EmailCreateDate = ec.EmailCreateDate,
                                                         EmailSentDate = ec.EmailSentDate
                                                     });

                agent.EmploymentCommunicationItems = queryEmploymentCommunications.AsNoTracking().ToList();

                //Agent Licenses
                var agentLicenses = GetLicenses(agent.EmploymentID);
                List<OputAgentLicenses>? oputAgentLicenses = agentLicenses.ObjData == null ? null : agentLicenses.ObjData as List<OputAgentLicenses>;
                if (oputAgentLicenses != null)
                    agent.LicenseItems = oputAgentLicenses;

                //Agent Appointments
                var agentAppointments = GetAppointments(agent.EmploymentID);
                List<OputAgentAppointments>? oputAgentAppointments = agentAppointments.ObjData == null ? null : agentAppointments.ObjData as List<OputAgentAppointments>;
                if (oputAgentAppointments != null)
                    agent.AppointmentItems = oputAgentAppointments;

                //Agent Ticklers
                var agentTicklers = FillTicklerItems(agent.EmploymentID);
                agent.TicklerItems = agentTicklers;

                //Agent Worklist Items
                var agentWorklist = FillWorklistItems(agent.GEID);
                agent.WorklistItems = agentWorklist;

                result.Success = true;
                result.ObjData = agent;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-8807-12007].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            };

            return result;
        }

        //public ReturnResult GetAgentByTMemberID(int vTMemberID)
        //{
        //    var employeeID = _db.Employees.Where(e => e.Geid == vTMemberID.ToString()).Select(e => e.EmployeeId).FirstOrDefault();
        //    return GetAgentByEmployeeID(employeeID);
        //}

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
                            orderby employeeLicense.LicenseStatus, license.StateProvinceAbv, license.LicenseName
                            select new OputAgentLicenses
                            {
                                LicenseID = (int)employeeLicense.LicenseId == 0 ? 0 : employeeLicense.LicenseId,
                                EmployeeLicenseId = employeeLicense.EmployeeLicenseId,
                                LicenseState = license.StateProvinceAbv,
                                LineOfAuthority = lineOfAuthority.LineOfAuthorityAbv,
                                LicenseStatus = employeeLicense.LicenseStatus,
                                EmploymentID = employeeLicense.EmploymentId,
                                LicenseName = license.LicenseName,
                                LicenseNumber = employeeLicense.LicenseNumber,
                                Required = (bool)employeeLicense.Required,
                                Reinstatement = (bool)employeeLicense.Reinstatement,
                                NonResident = (bool)employeeLicense.NonResident,
                                OriginalIssueDate = employeeLicense.LicenseIssueDate,
                                LineOfAuthIssueDate = employeeLicense.LineOfAuthorityIssueDate,
                                LicenseEffectiveDate = employeeLicense.LicenseEffectiveDate,
                                LicenseExpirationDate = employeeLicense.LicenseExpireDate
                            };

                var licenses = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = licenses;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-8807-85005].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
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
                var employeeLicenses = (from employeeLicense in _db.EmployeeLicenses
                                        join license in _db.Licenses on employeeLicense.LicenseId equals license.LicenseId
                                        where employeeLicense.EmploymentId == vEmploymentID
                                        orderby employeeLicense.LicenseStatus, license.StateProvinceAbv, license.LicenseName
                                        select employeeLicense.EmployeeLicenseId)
                                        .ToList();

                var query = from appointment in _db.EmployeeAppointments
                            join employeeLicense in _db.EmployeeLicenses on appointment.EmployeeLicenseId equals employeeLicense.EmployeeLicenseId
                            join license in _db.Licenses on employeeLicense.LicenseId equals license.LicenseId
                            join lineOfAuthority in _db.LineOfAuthorities on license.LineOfAuthorityId equals lineOfAuthority.LineOfAuthorityId
                            join company in _db.Companies on appointment.CompanyId equals company.CompanyId
                            where employeeLicenses.Contains((int)appointment.EmployeeLicenseId)
                            orderby appointment.AppointmentStatus, license.StateProvinceAbv, lineOfAuthority.LineOfAuthorityAbv
                            select new OputAgentAppointments
                            {
                                LicenseID = (int)license.LicenseId,
                                LicenseState = license.StateProvinceAbv ?? "",
                                LineOfAuthority = lineOfAuthority.LineOfAuthorityAbv ?? "",
                                EmployeeAppointmentID = appointment.EmployeeAppointmentId,
                                EmployeeLicenseID = (int)appointment.EmployeeLicenseId,
                                AppointmentEffectiveDate = appointment.AppointmentEffectiveDate,
                                AppointmentStatus = appointment.AppointmentStatus,
                                CarrierDate = appointment.CarrierDate,
                                AppointmentExpireDate = appointment.AppointmentExpireDate,
                                AppointmentTerminationDate = appointment.AppointmentTerminationDate,
                                CompanyID = appointment.CompanyId,
                                CompanyAbv = company.CompanyAbv,
                                RetentionDate = appointment.RetentionDate
                            };

                var appointments = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = appointments;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-8807-85045].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
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

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-8807-82545].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
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

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-8807-27711].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
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

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-8807-27710].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
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

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-8807-29710].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
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

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-8807-29715].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
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

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-7807-79015].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }

        public ReturnResult GetBranchCodes()
        {
            var result = new ReturnResult();
            try
            {
                var query = from b in _db.Bifs
                            select new
                            {
                                BranchCode = b.HrDepartmentId
                            };

                var branches = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = branches;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-9907-79715].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }

        public ReturnResult GetCoRequirementAssetIDs()
        {
            var result = new ReturnResult();
            try
            {
                var query = from typeStatus in _db.LkpTypeStatuses
                            where typeStatus.LkpField == "CompanyRequirement"
                            select new
                            {
                                LkpValue = typeStatus.LkpValue,
                                //SortOrder = typeStatus.SortOrder
                            };

                var coReqAssetIDs = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = coReqAssetIDs;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6168-32485].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }

        public ReturnResult GetCoRequirementStatuses()
        {
            var result = new ReturnResult();
            try
            {
                var query = from typeStatus in _db.LkpTypeStatuses
                            where typeStatus.LkpField == "CompanyRequirementStatus"
                            select new
                            {
                                LkpValue = typeStatus.LkpValue,
                                //SortOrder = typeStatus.SortOrder
                            };

                var coReqStatuses = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = coReqStatuses;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-4168-32021].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }

        public ReturnResult GetLicLevels()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var resultLicLevels = new List<(string LicenseLevel, int SortOrder)>
                                        {
                                            ("Select Lic Level", 0),
                                            ("NoLicense", 1),
                                            ("LicLevel1", 2),
                                            ("LicLevel2", 3),
                                            ("LicLevel3", 4),
                                            ("LicLevel4", 5)
                                        }
                                        .OrderBy(x => x.SortOrder)
                                        .Select(x => new { x.LicenseLevel, x.SortOrder });

                result.ObjData = resultLicLevels;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-5168-32181].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }
            return result;
        }

        public ReturnResult GetLicIncentives()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var resultIncentives = new List<(string LicenseIncentive, int SortOrder)>
                                {
                                    ("Select Incentive", 0),
                                    ("NoIncentive", 1),
                                    ("PLS_Incentive1", 2),
                                    ("Incentive2_Plus", 3),
                                    //("LicIncentive3", 3)
                                }
                                .OrderBy(x => x.SortOrder)
                                .Select(x => new { x.LicenseIncentive, x.SortOrder });


                result.ObjData = resultIncentives;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6068-30081].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }
            return result;
        }

        #region INSERT/UPDATE/DELETE
        public ReturnResult UpsertAgent([FromBody] IputUpsertAgent vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.EmployeeID == 0)
                {
                    // INSERT
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspAgentInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmployeeSSN", SqlDbType.VarChar, 12) { Value = vInput.EmployeeSSN ?? string.Empty });
                            //cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.VarChar, 25) { Value = vInput.EmployeeID });
                            cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 50) { Value = vInput.LastName ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 50) { Value = vInput.FirstName ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@MiddleName", SqlDbType.VarChar, 50) { Value = vInput.MiddleName ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@SOEID", SqlDbType.VarChar, 15) { Value = vInput.SOEID ?? string.Empty });
                            //cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = vInput.DateOfBirth ?? DateTime.MinValue });
                            if (vInput.DateOfBirth != DateTime.MinValue)
                            {
                                cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = vInput.DateOfBirth });
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = DBNull.Value });
                            }
                            cmd.Parameters.Add(new SqlParameter("@NationalProducerNumber", SqlDbType.Int) { Value = vInput.NationalProducerNumber });
                            cmd.Parameters.Add(new SqlParameter("@GEID", SqlDbType.VarChar, 15) { Value = vInput.GEID ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@Alias", SqlDbType.VarChar, 50) { Value = vInput.Alias ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@ExcludeFromRpts", SqlDbType.Bit) { Value = vInput.ExcludeFromRpts });
                            cmd.Parameters.Add(new SqlParameter("@Address1", SqlDbType.VarChar, 50) { Value = vInput.Address1 ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@Address2", SqlDbType.VarChar, 50) { Value = vInput.Address2 ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 50) { Value = vInput.City ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.Char, 2) { Value = vInput.State ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@Zip", SqlDbType.VarChar, 12) { Value = vInput.Zip ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 13) { Value = vInput.Phone ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@Fax", SqlDbType.VarChar, 13) { Value = vInput.Fax ?? string.Empty });
                            //cmd.Parameters.Add(new SqlParameter("@EmploymentID", SqlDbType.Int) { Value = vInput.EmploymentID });
                            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 50) { Value = vInput.Email ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@WorkPhone", SqlDbType.VarChar, 13) { Value = vInput.WorkPhone ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@EmployeeStatus", SqlDbType.VarChar, 25) { Value = vInput.EmployeeStatus ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@CompanyID", SqlDbType.Int) { Value = vInput.CompanyID });
                            //cmd.Parameters.Add(new SqlParameter("@CERequired", SqlDbType.Bit) { Value = vInput.CERequired });
                            cmd.Parameters.Add(new SqlParameter("@LicenseLevel", SqlDbType.VarChar, 25) { Value = vInput.LicenseLevel ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@LicenseIncentive", SqlDbType.VarChar, 25) { Value = vInput.LicenseIncentive ?? string.Empty });
                            //cmd.Parameters.Add(new SqlParameter("@SecondChance", SqlDbType.Bit) { Value = vInput.SecondChance });
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", SqlDbType.VarChar, 50) { Value = vInput.UserSOEID ?? string.Empty });


                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    result = InsertAgent_v2(vInput);
                }
                else
                {
                    // UPDATE
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspAgentDetailsUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmployeeSSN", SqlDbType.VarChar, 12) { Value = vInput.EmployeeSSN ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.VarChar, 25) { Value = vInput.EmployeeID });
                            cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 50) { Value = vInput.LastName ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 50) { Value = vInput.FirstName ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@MiddleName", SqlDbType.VarChar, 50) { Value = vInput.MiddleName ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@SOEID", SqlDbType.VarChar, 15) { Value = vInput.SOEID ?? string.Empty });
                            //cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = vInput.DateOfBirth ?? DateTime.MinValue });
                            if (vInput.DateOfBirth != DateTime.MinValue)
                            {
                                cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = vInput.DateOfBirth });
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = DBNull.Value });
                            }
                            cmd.Parameters.Add(new SqlParameter("@NationalProducerNumber", SqlDbType.Int) { Value = vInput.NationalProducerNumber });
                            cmd.Parameters.Add(new SqlParameter("@GEID", SqlDbType.VarChar, 15) { Value = vInput.GEID ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@Alias", SqlDbType.VarChar, 50) { Value = vInput.Alias ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@ExcludeFromRpts", SqlDbType.Bit) { Value = vInput.ExcludeFromRpts });
                            cmd.Parameters.Add(new SqlParameter("@Address1", SqlDbType.VarChar, 50) { Value = vInput.Address1 ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@Address2", SqlDbType.VarChar, 50) { Value = vInput.Address2 ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 50) { Value = vInput.City ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.Char, 2) { Value = vInput.State ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@Zip", SqlDbType.VarChar, 12) { Value = vInput.Zip ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 13) { Value = vInput.Phone ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@Fax", SqlDbType.VarChar, 13) { Value = vInput.Fax ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@EmploymentID", SqlDbType.Int) { Value = vInput.EmploymentID });
                            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 50) { Value = vInput.Email ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@WorkPhone", SqlDbType.VarChar, 13) { Value = vInput.WorkPhone ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@EmployeeStatus", SqlDbType.VarChar, 25) { Value = vInput.EmployeeStatus ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@CompanyID", SqlDbType.Int) { Value = vInput.CompanyID });
                            cmd.Parameters.Add(new SqlParameter("@CERequired", SqlDbType.Bit) { Value = vInput.CERequired });
                            cmd.Parameters.Add(new SqlParameter("@LicenseLevel", SqlDbType.VarChar, 25) { Value = vInput.LicenseLevel ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@LicenseIncentive", SqlDbType.VarChar, 25) { Value = vInput.LicenseIncentive ?? string.Empty });
                            cmd.Parameters.Add(new SqlParameter("@SecondChance", SqlDbType.Bit) { Value = vInput.SecondChance });
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", SqlDbType.VarChar, 50) { Value = vInput.UserSOEID ?? string.Empty });


                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6509-90422].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult InsertAgent_v2([FromBody] IputUpsertAgent vInput)
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
                        //var isAddressInsert = ExecuteAddressInsert(vInput, country);


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
                            AddressId = InsertAddress(vInput, country),
                            ExcludeFromRpts = vInput.ExcludeFromRpts
                        };
                        context.Employees.Add(employee);
                        context.SaveChanges();

                        // Log audit for Employee INSERT
                        int employeeId = employee.EmployeeId;

                        if (employeeId > 0)
                            _utilityService.LogAudit("Employee", employeeId, vInput.UserSOEID, "INSERT", "FirstName, MiddleName, LastName", null, employee.EmployeeId.ToString());

                        // Employment INSERT 
                        //var isEmploymentInsert = ExecuteEmploymentInsert(vInput, employeeId);
                        var newEmployment = new Employment
                        {
                            EmployeeId = employeeId,
                            CompanyId = vInput.CompanyID,
                            IsRehire = false,
                            EmployeeStatus = vInput.EmployeeStatus,
                            WorkPhone = vInput.WorkPhone,
                            Email = vInput.Email,
                            EmployeeType = null,
                            Cerequired = vInput.CERequired,
                            IsCurrent = true,
                            LicenseIncentive = vInput.LicenseIncentive,
                            LicenseLevel = vInput.LicenseLevel
                        };
                        context.Employments.Add(newEmployment);
                        context.SaveChanges();

                        // Log audit for Employment INSERT
                        int employmentId = newEmployment.EmploymentId;

                        if (employmentId > 0)
                            _utilityService.LogAudit("Employment", employmentId, vInput.UserSOEID, "INSERT", "EmployeeId, CompanyId, EmployeeStatus, WorkPhone, Email, Cerequired, LicenseIncentive, LicenseLevel", null, employmentId.ToString());

                        // INSERT EmploymentHistory record
                        var employmentHistory = new EmploymentHistory
                        {
                            EmploymentId = employmentId,
                            HireDate = vInput.HireDate,
                            BackgroundCheckStatus = vInput.BackgroundCheckStatus,
                            BackGroundCheckNotes = vInput.BackgroundCheckNote,
                        };
                        context.EmploymentHistories.Add(employmentHistory);
                        context.SaveChanges();

                        // Log audit for EmploymentHistory INSERT
                        int employmentHistoryId = employmentHistory.EmploymentHistoryId;

                        if (employmentHistoryId > 0)
                            _utilityService.LogAudit("EmploymentHistory", employmentHistoryId, vInput.UserSOEID, "INSERT", "EmploymentId, HireDate, BackgroundCheckStatus, BackGroundCheckNotes", null, employmentHistoryId.ToString());


                        // INSERT TransferHistory record
                        var transferHistory = new TransferHistory
                        {
                            EmploymentId = employmentId,
                            BranchCode = vInput.BranchCode,
                            ResStateAbv = vInput.ResStateAbv,
                            WorkStateAbv = vInput.WorkStateAbv,
                            TransferDate = transferDate,
                        };
                        context.TransferHistories.Add(transferHistory);
                        context.SaveChanges();

                        // Log audit for TransferHistory INSERT
                        int transferHistoryId = transferHistory.TransferHistoryId;

                        if (transferHistoryId > 0)
                            _utilityService.LogAudit("TransferHistory", transferHistoryId, vInput.UserSOEID, "INSERT", "EmploymentId, BranchCode, ResStateAbv, WorkStateAbv, TransferDate", null, transferHistoryId.ToString());


                        // INSERT EmploymentJobTitle record
                        string command = "INSERT INTO EmploymentJobTitle (EmploymentId, JobTitleId, JobTitleDate, IsCurrent) OUTPUT INSERTED.EmploymentJobTitleId VALUES (@EmploymentId, @JobTitleId, @JobTitleDate, @IsCurrent)";
                        var parameters = new[]
                        {
                            new SqlParameter("@EmploymentId", employmentId),
                            new SqlParameter("@JobTitleId", vInput.JobTitleID),
                            new SqlParameter("@JobTitleDate", vInput.JobTitleDate == DateTime.MinValue ? DBNull.Value : vInput.JobTitleDate),
                            new SqlParameter("@IsCurrent", true)
                        };
                        var newId = context.Database.ExecuteSqlRaw(command, parameters);

                        //command.Parameters.Add("@JobTitleDate", SqlDbType.DateTime, 10).Value = vInput.JobTitleDate == DateTime.MinValue ? DBNull.Value : vInput.JobTitleDate;

                        // Log audit for EmploymentJobTitle INSERT
                        if (newId > 0)
                            _utilityService.LogAudit("EmploymentJobTitle", newId, vInput.UserSOEID, "INSERT", "EmploymentId, JobTitleId, JobTitleDate", null, newId.ToString());

                        transaction.Commit();

                        var empResult = new { EmployeeID = employeeId, Message = "Success" };
                        result.Success = true;
                        result.ObjData = empResult;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        result.StatusCode = 500;
                        result.Success = false;
                        result.ObjData = null;
                        result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6509-90421].";

                        _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
                    }
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6509-90121].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteAgentEmployee(int vEmployeeID, string vUserSOEID)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspEmploymentDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EmployeeID", vEmployeeID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vUserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.StatusCode = 200;
                result.ObjData = new { Message = "Delete Successful" };

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6509-90523].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vUserSOEID);
            }

            return result;
        }
        /* NOTE - THIS STORED PROCEDURE EXECUTES SUCCESSFULLY WITHOUT ACTUALLY UPDATING THE NATIONAL PRODUCER NUMBER. */
        public ReturnResult UpdateAgentDetails([FromBody] IputAgentDetail vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspAgentDetailsUpdate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EmployeeSSN", vInput.EmployeeSSN));
                        cmd.Parameters.Add(new SqlParameter("@EmployeeID", vInput.EmployeeID));
                        cmd.Parameters.Add(new SqlParameter("@LastName", vInput.LastName));
                        cmd.Parameters.Add(new SqlParameter("@FirstName", vInput.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@MiddleName", vInput.MiddleName));
                        cmd.Parameters.Add(new SqlParameter("@SOEID", vInput.SOEID));
                        cmd.Parameters.Add(new SqlParameter("@DateOfBirth", vInput.DateOfBirth)); // replace with your value
                        cmd.Parameters.Add(new SqlParameter("@NationalProducerNumber", vInput.NationalProducerNumber)); // replace with your value
                        cmd.Parameters.Add(new SqlParameter("@GEID", vInput.GEID));
                        cmd.Parameters.Add(new SqlParameter("@Alias", vInput.Alias));
                        cmd.Parameters.Add(new SqlParameter("@ExcludeFromRpts", vInput.ExcludeFromRpts)); // replace with your value
                        cmd.Parameters.Add(new SqlParameter("@Address1", vInput.Address1));
                        cmd.Parameters.Add(new SqlParameter("@Address2", vInput.Address2));
                        cmd.Parameters.Add(new SqlParameter("@City", vInput.City));
                        cmd.Parameters.Add(new SqlParameter("@State", vInput.State));
                        cmd.Parameters.Add(new SqlParameter("@Zip", vInput.Zip));
                        cmd.Parameters.Add(new SqlParameter("@Phone", vInput.Phone));
                        cmd.Parameters.Add(new SqlParameter("@Fax", vInput.Fax));
                        cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID)); // replace with your value
                        cmd.Parameters.Add(new SqlParameter("@Email", vInput.Email));
                        cmd.Parameters.Add(new SqlParameter("@WorkPhone", vInput.WorkPhone));
                        cmd.Parameters.Add(new SqlParameter("@EmployeeStatus", vInput.EmployeeStatus));
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", vInput.CompanyID)); // replace with your value
                        cmd.Parameters.Add(new SqlParameter("@CERequired", vInput.CERequired)); // replace with your value
                        cmd.Parameters.Add(new SqlParameter("@LicenseLevel", vInput.LicenseLevel));
                        cmd.Parameters.Add(new SqlParameter("@LicenseIncentive", vInput.LicenseIncentive));
                        cmd.Parameters.Add(new SqlParameter("@SecondChance", vInput.SecondChance)); // replace with your value
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6509-49334].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpdateAgentNatNumber([FromBody] IputAgentDetail vInput)
        {
            var result = new ReturnResult();
            try
            {
                var employee = _db.Employees
                    .Where(e => e.EmployeeId == vInput.EmployeeID)
                    .FirstOrDefault();

                if (employee != null)
                {
                    employee.NationalProducerNumber = vInput.NationalProducerNumber;
                    _db.SaveChanges();
                }

                result.Success = true;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6509-49334].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertEmploymentHistItem([FromBody] InputEmploymentHistItem vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.EmploymentHistoryID == 0)
                {
                    // INSERT Employment History
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspAgentEmploymentHistInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                            cmd.Parameters.Add(new SqlParameter("@EmployeeID", vInput.EmployeeID));
                            cmd.Parameters.Add(new SqlParameter("@HireDate", vInput.HireDate));
                            cmd.Parameters.Add(new SqlParameter("@RehireDate", vInput.RehireDate));
                            cmd.Parameters.Add(new SqlParameter("@NotifiedTermDate", vInput.NotifiedTermDate));
                            cmd.Parameters.Add(new SqlParameter("@HRTermDate", vInput.HrTermDate));
                            cmd.Parameters.Add(new SqlParameter("@HRTermCode", vInput.HrTermCode));
                            cmd.Parameters.Add(new SqlParameter("@ForCause", vInput.IsForCause));
                            cmd.Parameters.Add(new SqlParameter("@BackgroundCheckStatus", vInput.BackgroundCheckStatus));
                            cmd.Parameters.Add(new SqlParameter("@BackgroundCheckNote", vInput.BackGroundCheckNotes));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));
                            cmd.Parameters.Add(new SqlParameter("@IsCurrent", vInput.IsCurrent));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE Employment History
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspAgentEmploymentHistUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                            cmd.Parameters.Add(new SqlParameter("@EmploymentHistoryID", vInput.EmploymentHistoryID));
                            cmd.Parameters.Add(new SqlParameter("@HireDate", vInput.HireDate));
                            cmd.Parameters.Add(new SqlParameter("@RehireDate", vInput.RehireDate));
                            cmd.Parameters.Add(new SqlParameter("@NotifiedTermDate", vInput.NotifiedTermDate));
                            cmd.Parameters.Add(new SqlParameter("@HRTermDate", vInput.HrTermDate));
                            cmd.Parameters.Add(new SqlParameter("@HRTermCode", vInput.HrTermCode));
                            cmd.Parameters.Add(new SqlParameter("@ForCause", vInput.IsForCause));
                            cmd.Parameters.Add(new SqlParameter("@BackgroundStatus", vInput.BackgroundCheckStatus));
                            cmd.Parameters.Add(new SqlParameter("@BackgroundNote", vInput.BackGroundCheckNotes));
                            cmd.Parameters.Add(new SqlParameter("@IsCurrent", vInput.IsCurrent));
                            cmd.Parameters.Add(new SqlParameter("@EmployeeID", vInput.EmployeeID));
                            cmd.Parameters.Add(new SqlParameter("@Email", DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@WorkPhone", DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@EmployeeStatus", DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@CompanyID", DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@CERequired", DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Employment History Item Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6509-69493].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteEmploymentHistItem([FromBody] IputDeleteEmploymentHistoryItem vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspEmploymentHistoryDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                        cmd.Parameters.Add(new SqlParameter("@EmploymentHistoryID", vInput.EmploymentHistoryID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Employment History Item Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6509-70513].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertTranserHistItem([FromBody] IputTransferHistoryItem vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.TransferHistoryID == 0)
                {
                    // INSERT Transfer History
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspAgentTransferHistInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmployeeID", vInput.EmploymentID));
                            cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                            cmd.Parameters.Add(new SqlParameter("@BranchCode", vInput.BranchCode));
                            cmd.Parameters.Add(new SqlParameter("@ResStateAbv", vInput.ResStateAbv));
                            cmd.Parameters.Add(new SqlParameter("@WorkStateAbv", vInput.WorkStateAbv));
                            cmd.Parameters.Add(new SqlParameter("@TransferDate", vInput.TransferDate));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));
                            cmd.Parameters.Add(new SqlParameter("@IsCurrent", vInput.IsCurrent));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE Transfer History
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspAgentTransferHistUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@TransferHistoryID", vInput.TransferHistoryID));
                            cmd.Parameters.Add(new SqlParameter("@BranchCode", vInput.BranchCode));
                            cmd.Parameters.Add(new SqlParameter("@ResStateAbv", vInput.ResStateAbv));
                            cmd.Parameters.Add(new SqlParameter("@WorkStateAbv", vInput.WorkStateAbv));
                            cmd.Parameters.Add(new SqlParameter("@TransferDate", vInput.TransferDate));
                            cmd.Parameters.Add(new SqlParameter("@IsCurrent", vInput.IsCurrent));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Transfer History Item Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ObjData = null;
                result.StatusCode = 500;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6509-73602].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteTransferHistItem([FromBody] IputDeleteTransferHisttoryItem vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspTransferHistoryDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                        cmd.Parameters.Add(new SqlParameter("@TransferHistoryID", vInput.TransferHistoryID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Transfer History Item Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6168-32581].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertCoRequirementItem([FromBody] IputCoRequirementItem vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.CompanyRequirementID == 0)
                {
                    // INSERT CoRequirement
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspEmploymentCompanyRequirementInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;


                            cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                            cmd.Parameters.Add(new SqlParameter("@asset_sk", vInput.AssetSk));
                            cmd.Parameters.Add(new SqlParameter("@asset_id", vInput.AssetIdString));
                            cmd.Parameters.Add(new SqlParameter("@learning_program_status", vInput.LearningProgramStatus));
                            cmd.Parameters.Add(new SqlParameter("@learning_program_enrollment_date", vInput.LearningProgramEnrollmentDate));
                            cmd.Parameters.Add(new SqlParameter("@learning_program_completion_date", vInput.LearningProgramCompletionDate));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE CoRequirement
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspEmploymentCompanyRequirementUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmploymentCompanyRequirementID", vInput.CompanyRequirementID));
                            cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                            cmd.Parameters.Add(new SqlParameter("@asset_sk", vInput.AssetSk));
                            cmd.Parameters.Add(new SqlParameter("@asset_id", vInput.AssetIdString));
                            cmd.Parameters.Add(new SqlParameter("@learning_program_status", vInput.LearningProgramStatus));
                            cmd.Parameters.Add(new SqlParameter("@learning_program_enrollment_date", vInput.LearningProgramEnrollmentDate));
                            cmd.Parameters.Add(new SqlParameter("@learning_program_completion_date", vInput.LearningProgramCompletionDate));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "CoRequirement Item Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6168-12981].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;

        }
        public ReturnResult DeleteCoRequirementItem([FromBody] IputDeleteCoRequirementItem vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspEmploymentCompanyRequirementDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                        cmd.Parameters.Add(new SqlParameter("@EmploymentCompanyRequirementID", vInput.CompanyRequirementID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Transfer History Item Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6168-32189].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertEmploymentJobTitleItem([FromBody] IputEmploymentJobTitleItem vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.EmploymentJobTitleID == 0)
                {
                    // INSERT Employment Job Title
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspEmploymentJobTitleInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                            cmd.Parameters.Add(new SqlParameter("@JobTitleID", (int)vInput.JobTitleID));
                            cmd.Parameters.Add(new SqlParameter("@JobTitleDate", vInput.JobTitleDate));
                            cmd.Parameters.Add(new SqlParameter("@IsCurrent", vInput.IsCurrent));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE Employment Job Title
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspEmploymentJobTitleUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmploymentJobTitleID", vInput.EmploymentJobTitleID));
                            cmd.Parameters.Add(new SqlParameter("@JobTitleID", vInput.JobTitleID));
                            cmd.Parameters.Add(new SqlParameter("@JobTitleDate", vInput.JobTitleDate));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Employment Job Title Item Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6168-32081].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteEmploymentJobTitleItem([FromBody] IputDeleteEmploymentJobTitle vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspEmploymentJobTitleDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                        cmd.Parameters.Add(new SqlParameter("@EmploymentJobTitleID", vInput.EmploymentJobTitleID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Employment Job Title Item Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6168-22081].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertAgentLicense([FromBody] IputUpsertAgentLicense vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.EmployeeLicenseID == 0)
                {
                    // INSERT Agent License
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspAgentLicenseInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@EmployeeID", vInput.EmployeeID));
                            cmd.Parameters.Add(new SqlParameter("@AscEmployeeLicenseID", vInput.AscEmployeeLicenseID ?? 0));
                            cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID ?? 0));
                            cmd.Parameters.Add(new SqlParameter("@LicenseExpireDate", vInput.LicenseExpireDate ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@LicenseStatus", vInput.LicenseStatus ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@LicenseNumber", vInput.LicenseNumber ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@Reinstatement", vInput.Reinstatement ?? false));
                            cmd.Parameters.Add(new SqlParameter("@Required", vInput.Required ?? false));
                            cmd.Parameters.Add(new SqlParameter("@NonResident", vInput.NonResident ?? false));
                            cmd.Parameters.Add(new SqlParameter("@LicenseEffectiveDate", vInput.LicenseEffectiveDate ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                            cmd.Parameters.Add(new SqlParameter("@LicenseIssueDate", vInput.LicenseIssueDate ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@LineOfAuthorityIssueDate", vInput.LineOfAuthorityIssueDate ?? (object)DBNull.Value));
                            //cmd.Parameters.Add(new SqlParameter("@SentToAgentDate", vInput.SentToAgentDate));
                            cmd.Parameters.Add(new SqlParameter("@SentToAgentDate",
                                string.IsNullOrEmpty(vInput.SentToAgentDate?.ToString()) ? (object)DBNull.Value : vInput.SentToAgentDate));

                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));
                            cmd.Parameters.Add(new SqlParameter("@LicenseNote", vInput.LicenseNote ?? (object)DBNull.Value));
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    //using (var context = new AppDataContext())
                    //using (var transaction = context.Database.BeginTransaction())
                    //{

                    //    // INSERT into EmployeeLicense and log audit
                    //    var employeeLicense = new EmployeeLicense
                    //    {
                    //        EmploymentId = vInput.EmploymentID,
                    //        AscEmployeeLicenseId = vInput.AscEmployeeLicenseID,
                    //        LicenseId = vInput.LicenseID,
                    //        LicenseExpireDate = vInput.LicenseExpireDate,
                    //        LicenseStatus = vInput.LicenseStatus,
                    //        LicenseNumber = vInput.LicenseNumber,
                    //        Reinstatement = vInput.Reinstatement,
                    //        Required = vInput.Required,
                    //        NonResident = vInput.NonResident,
                    //        LicenseEffectiveDate = vInput.LicenseEffectiveDate,
                    //        LicenseIssueDate = vInput.LicenseIssueDate,
                    //        LineOfAuthorityIssueDate = vInput.LineOfAuthorityIssueDate,
                    //        LicenseNote = vInput.LicenseNote,

                    //    };
                    //    context.EmployeeLicenses.Add(employeeLicense);
                    //    context.SaveChanges();

                    //    // Log audit for EmployeeSSN insertion
                    //    int employeeLicenseId = employeeLicense.EmployeeLicenseId;

                    //    if (employeeLicenseId > 0)
                    //        _utilityService.LogAudit("EmployeeLicense", employeeLicenseId, vInput.UserSOEID, "INSERT", "EmployeeLicense", null, employeeLicenseId.ToString());

                    //    transaction.Commit();

                    //    result.Success = true;
                    //    result.ObjData = employeeLicense;

                    //}

                }
                else
                {
                    // UPDATE Agent License
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspAgentLicenseUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                            cmd.Parameters.Add(new SqlParameter("@AscEmployeeLicenseID", vInput.AscEmployeeLicenseID));
                            cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID));
                            cmd.Parameters.Add(new SqlParameter("@LicenseExpireDate", vInput.LicenseExpireDate ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@LicenseStatus", vInput.LicenseStatus));
                            cmd.Parameters.Add(new SqlParameter("@LicenseNumber", vInput.LicenseNumber));
                            cmd.Parameters.Add(new SqlParameter("@Reinstatement", vInput.Reinstatement));
                            cmd.Parameters.Add(new SqlParameter("@Required", vInput.Required));
                            cmd.Parameters.Add(new SqlParameter("@NonResident", vInput.NonResident));
                            cmd.Parameters.Add(new SqlParameter("@LicenseEffectiveDate", vInput.LicenseEffectiveDate ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@LicenseIssueDate", vInput.LicenseIssueDate ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@LineOfAuthorityIssueDate", vInput.LineOfAuthorityIssueDate ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@LicenseNote", vInput.LicenseNote ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@AppointmentStatus", vInput.AppointmentStatus ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@CompanyID", vInput.CompanyID));
                            cmd.Parameters.Add(new SqlParameter("@CarrierDate", vInput.CarrierDate ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@AppointmentEffectiveDate", vInput.AppointmentEffectiveDate ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@AppointmentExpireDate", vInput.AppointmentExpireDate ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@AppointmentTerminationDate", vInput.AppointmentTerminationDate ?? (object)DBNull.Value));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Employment Agent Lincense Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6158-72181].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }
            return result;
        }
        public ReturnResult DeleteAgentLicense([FromBody] IputDeleteAgentLincense vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspEmployeeLicenseDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                        cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Employment Agent License Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6158-79451].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertConEduTaken([FromBody] IputUpsertConEduTaken vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.EmployeeEducationID == 0)
                {
                    var contEducationID = _db.EmployeeContEducations
                                        .Where(q => q.ContEducationRequirementId == vInput.ContEducationRequirementID)
                                        .Select(q => q.ContEducationId)
                                        .FirstOrDefault();

                    // INSERT Continuing Education Taken
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspAgentContEducationTakenInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ContEducationID", 1));
                            cmd.Parameters.Add(new SqlParameter("@ContEducationRequirementID", vInput.ContEducationRequirementID));
                            cmd.Parameters.Add(new SqlParameter("@ContEducationTakenDate", vInput.ContEducationTakenDate));
                            cmd.Parameters.Add(new SqlParameter("@CreditHoursTaken", vInput.CreditHoursTaken));
                            cmd.Parameters.Add(new SqlParameter("@AdditionalNotes", vInput.AdditionalNotes));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    var contEducationID = _db.EmployeeContEducations
                                        .Where(q => q.EmployeeEducationId == vInput.EmployeeEducationID)
                                        .Select(q => q.ContEducationId)
                                        .FirstOrDefault();

                    // UPDATE Continuing Education Taken
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspAgentContEducationTakenUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmployeeEducationID", vInput.EmployeeEducationID));
                            cmd.Parameters.Add(new SqlParameter("@ContEducationID", contEducationID));
                            cmd.Parameters.Add(new SqlParameter("@ContEducationTakenDate", vInput.ContEducationTakenDate));
                            cmd.Parameters.Add(new SqlParameter("@CreditHoursTaken", vInput.CreditHoursTaken));
                            cmd.Parameters.Add(new SqlParameter("@AdditionalNotes", vInput.AdditionalNotes));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Continuing Education Taken Item Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6158-79881].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteConEduTaken([FromBody] IputDeleteConEduTaken vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspEmployeeContEducationDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EmployeeEducationID", vInput.EmployeeEducationID));
                        cmd.Parameters.Add(new SqlParameter("@ContEducationRequirementID", vInput.ContEducationRequirementID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Continuing Education Taken Item Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6168-09881].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertDiaryItem([FromBody] IputUpsertDiaryItem vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.DiaryID == 0)
                {
                    // INSERT Diary
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspAgentDiaryInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                            cmd.Parameters.Add(new SqlParameter("@SOEID", vInput.SOEID));
                            cmd.Parameters.Add(new SqlParameter("@DiaryDate", DateTime.Now));
                            cmd.Parameters.Add(new SqlParameter("@DiaryName", vInput.DiaryName));
                            cmd.Parameters.Add(new SqlParameter("@Notes", vInput.Notes));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE Diary
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspAgentDiaryUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DiaryID", vInput.DiaryID.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@Notes", vInput.Notes));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Diary Item Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6168-98881].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteDiaryItem([FromBody] IputDeleteDiaryItem vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspDiaryDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@DiaryID", vInput.DiaryID));
                        cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Diary Item Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6168-098591].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult CloseWorklistItem([FromBody] IputAgentWorklistItem vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspWorkListDataUpdate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@WorkListDataID", vInput.WorkListDataID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Worklist Item Closed Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# AGNT-6368-79081].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }
            return result;
        }
        #endregion


        #region Private Methods
        private bool ExecuteAddressInsert(IputUpsertAgent vInput, string? vCountry = null)
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
        public int InsertAddress(IputUpsertAgent vInput, string? vCountry = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("uspAddressInsert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@AddressType", SqlDbType.VarChar, 20).Value = "Agent";
                    cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 50).Value = vInput.Address1;
                    cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 50).Value = vInput.Address2;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = vInput.City;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = vInput.State;
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 13).Value = vInput.Phone;
                    cmd.Parameters.Add("@Country", SqlDbType.DateTime, 10).Value = vCountry;
                    cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 12).Value = vInput.Zip;
                    cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 13).Value = vInput.Fax;
                    cmd.Parameters.Add("@UserSOEID", SqlDbType.VarChar, 50).Value = vInput.UserSOEID;

                    SqlParameter addressIdParam = new SqlParameter("@AddressID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(addressIdParam);

                    cmd.ExecuteNonQuery();

                    return (int)addressIdParam.Value;
                }
            }
        }

        //private bool ExecuteEmploymentInsert(IputUpsertAgent vInput, int vEmployeeId)
        //{
        //    try
        //    {
        //        using (var connection = new SqlConnection(_connectionString))
        //        {
        //            using (var command = new SqlCommand("[uspAgentEmploymentInsert]", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                //Employment
        //                command.Parameters.Add("@EmployeeID", SqlDbType.Int, 0).Value = vEmployeeId;
        //                command.Parameters.Add("@EmployeeStatus", SqlDbType.VarChar, 25).Value = vInput.EmployeeStatus ?? null;
        //                command.Parameters.Add("@CompanyID", SqlDbType.Int, 0).Value = vInput.CompanyID;
        //                command.Parameters.Add("@WorkPhone", SqlDbType.VarChar, 13).Value = vInput.WorkPhone ?? null;
        //                command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = vInput.Email ?? null;
        //                command.Parameters.Add("@LicenseIncentive", SqlDbType.VarChar, 25).Value = vInput.LicenseIncentive ?? null;
        //                command.Parameters.Add("@LicenseLevel", SqlDbType.VarChar, 25).Value = vInput.LicenseLevel ?? null;

        //                //EmploymentHistory
        //                command.Parameters.Add("@HireDate", SqlDbType.DateTime, 10).Value = vInput.HireDate.Date == DateTime.MinValue ? DBNull.Value : vInput.HireDate;
        //                //command.Parameters.Add("@HireDate", SqlDbType.DateTime, 10).Value = vInput.HireDate.Date == DateTime.MinValue ? (object)DBNull.Value : vInput.HireDate.Date;
        //                command.Parameters.Add("@BackgroundCheckStatus", SqlDbType.VarChar, 50).Value = vInput.BackgroundCheckStatus ?? null;
        //                command.Parameters.Add("@BackgroundCheckNote", SqlDbType.VarChar, 500).Value = vInput.BackgroundCheckNote ?? null;

        //                //TransferHistory
        //                command.Parameters.Add("@BranchCode", SqlDbType.VarChar, 50).Value = vInput.BranchCode ?? null;
        //                command.Parameters.Add("@ResStateAbv", SqlDbType.VarChar, 2).Value = vInput.ResStateAbv ?? null;
        //                command.Parameters.Add("@WorkStateAbv", SqlDbType.VarChar, 2).Value = vInput.WorkStateAbv ?? null;
        //                command.Parameters.Add("@UserSOEID", SqlDbType.VarChar, 50).Value = vInput.UserSOEID;

        //                //EmploymentJobTitle
        //                command.Parameters.Add("@JobTitleID", SqlDbType.Int, 0).Value = vInput.JobTitleID;
        //                command.Parameters.Add("@JobTitleDate", SqlDbType.DateTime, 10).Value = vInput.JobTitleDate == DateTime.MinValue ? DBNull.Value : vInput.JobTitleDate;

        //                connection.Open();
        //                command.ExecuteNonQuery();
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}
        private List<OputAgentLicenseAppointments> FillAgentLicenseAppointment(int vEmploymentID)
        {
            var queryEmployeeLicenses = from employeeLicense in _db.EmployeeLicenses
                                        join license in _db.Licenses on employeeLicense.LicenseId equals license.LicenseId
                                        join ascLicense in _db.EmployeeLicenses on employeeLicense.AscEmployeeLicenseId equals ascLicense.EmployeeLicenseId into ascLicense
                                        join lineOfAuthority in _db.LineOfAuthorities on license.LineOfAuthorityId equals lineOfAuthority.LineOfAuthorityId
                                        where employeeLicense.EmploymentId == vEmploymentID
                                        orderby employeeLicense.LicenseStatus ascending, license.StateProvinceAbv ascending
                                        select new OputAgentLicenseAppointments
                                        {
                                            EmployeeLicenseId = (int)employeeLicense.EmployeeLicenseId == 0 ? 0 : employeeLicense.EmployeeLicenseId,
                                            LicenseState = license.StateProvinceAbv,
                                            LineOfAuthority = lineOfAuthority.LineOfAuthorityAbv,
                                            LicenseStatus = employeeLicense.LicenseStatus,
                                            EmploymentID = employeeLicense.EmploymentId,
                                            LicenseID = license.LicenseId,
                                            LicenseName = license.LicenseName,
                                            LicenseNumber = employeeLicense.LicenseNumber,
                                            OriginalIssueDate = employeeLicense.LicenseIssueDate,
                                            LineOfAuthIssueDate = employeeLicense.LineOfAuthorityIssueDate,
                                            LicenseEffectiveDate = employeeLicense.LicenseEffectiveDate,
                                            LicenseExpirationDate = employeeLicense.LicenseExpireDate,
                                            LicenseNote = employeeLicense.LicenseNote,
                                            Reinstatement = employeeLicense.Reinstatement,
                                            Required = employeeLicense.Required,
                                            NonResident = employeeLicense.NonResident,
                                            AscEmployeeLicenseID = employeeLicense.AscEmployeeLicenseId,
                                            AscLicenseName = ascLicense.FirstOrDefault().License.LicenseName,
                                        };

            var licenses = queryEmployeeLicenses.AsNoTracking().ToList();

            var employeeLicenses = _db.EmployeeLicenses
                   .Where(x => x.EmploymentId == vEmploymentID)
                   .Select(x => x.EmployeeLicenseId)
                   .ToList();

            var queryAppointments = from appointment in _db.EmployeeAppointments
                                    join companies in _db.Companies on appointment.CompanyId equals companies.CompanyId
                                    join employeeLicense in _db.EmployeeLicenses on appointment.EmployeeLicenseId equals employeeLicense.EmployeeLicenseId
                                    join license in _db.Licenses on employeeLicense.LicenseId equals license.LicenseId
                                    where employeeLicenses.Contains((int)appointment.EmployeeLicenseId)
                                    orderby appointment.AppointmentStatus ascending, appointment.AppointmentEffectiveDate descending
                                    select new OputAgentAppointments
                                    {
                                        LicenseID = (int)license.LicenseId, // Get LicenseID from Licenses table
                                        EmployeeAppointmentID = appointment.EmployeeAppointmentId,
                                        EmployeeLicenseID = (int)appointment.EmployeeLicenseId,
                                        AppointmentEffectiveDate = appointment.AppointmentEffectiveDate,
                                        AppointmentStatus = appointment.AppointmentStatus,
                                        CarrierDate = appointment.CarrierDate,
                                        AppointmentExpireDate = appointment.AppointmentExpireDate,
                                        AppointmentTerminationDate = appointment.AppointmentTerminationDate,
                                        CompanyID = appointment.CompanyId,
                                        CompanyAbv = companies.CompanyAbv,
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
                                            BackgroundCheckStatus = h.BackgroundCheckStatus == "0" ? "N/A" : h.BackgroundCheckStatus,
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
                                                    CompanyRequirementID = ecr.EmploymentCompanyRequirementId,
                                                    EmploymentCompanyRequirementID = ecr.EmploymentCompanyRequirementId,
                                                    AssetIdString = ecr.AssetId,
                                                    AssetSk = ecr.AssetSk,
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
                                             JobTitleID = j.JobTitleId,
                                             JobTitle = j.JobTitle1,
                                             IsCurrent = ej.IsCurrent
                                         };

            var jobTitleHistories = queryJobTitleHistories.AsNoTracking().ToList();
            employmentTransferHistory.EmploymentJobTitleItems = jobTitleHistories;

            return employmentTransferHistory;
        }
        private (AgentContEduRequiredItem[], AgentContEduCompletedItem[]) FillContEducationItems(int vEmploymentID)
        {
            List<AgentContEduRequiredItem> _requiredItems = new List<AgentContEduRequiredItem>();
            List<AgentContEduCompletedItem> _completedItems = new List<AgentContEduCompletedItem>();

            var defaultDate = new DateTime(1900, 1, 1);
            var requiredQuery = (from ce in _db.ContEducationRequirements
                                 join e in _db.Employments on ce.EmploymentId equals e.EmploymentId
                                 where (ce.EmploymentId ?? 0) == vEmploymentID
                                 && ce.IsExempt == false
                                 && e.Cerequired == true
                                 // Uncomment the following line if you want to filter out records where EducationStartDate is the default date
                                 // && (ce.EducationStartDate ?? defaultDate) != defaultDate
                                 orderby ce.EducationStartDate descending
                                 select new
                                 {
                                     ce.ContEducationRequirementId,
                                     EducationStartDate = ce.EducationStartDate ?? defaultDate,
                                     EducationEndDate = ce.EducationEndDate ?? defaultDate,
                                     ce.RequiredCreditHours,
                                     ce.IsExempt,
                                     EmploymentId = ce.EmploymentId
                                 });

            if (requiredQuery.Any())
            {
                foreach (var item in requiredQuery)
                {
                    _requiredItems.Add(new AgentContEduRequiredItem
                    {
                        ContEducationRequirementID = item.ContEducationRequirementId,
                        EducationStartDate = item.EducationStartDate,
                        EducationEndDate = item.EducationEndDate,
                        RequiredCreditHours = item.RequiredCreditHours,
                        IsExempt = item.IsExempt
                    });
                }
            }
            else
            {
                _requiredItems.Add(new AgentContEduRequiredItem
                {
                    ContEducationRequirementID = 0,
                    EducationStartDate = defaultDate,
                    EducationEndDate = defaultDate,
                    RequiredCreditHours = 0,
                    IsExempt = false
                });
            }

            var requiredIDs = requiredQuery.Select(r => r.ContEducationRequirementId).ToList();

            var takenQuery = (from ece in _db.EmployeeContEducations
                              join ce in _db.ContEducations on ece.ContEducationId equals ce.ContEducationId
                              orderby ece.ContEducationTakenDate descending
                              select new
                              {
                                  ece.EmployeeEducationId,
                                  ce.EducationName,
                                  ece.ContEducationRequirementId,
                                  ece.ContEducationTakenDate,
                                  ece.CreditHoursTaken,
                                  ece.AdditionalNotes
                              });

            foreach (var item in requiredIDs)
            {
                var completedItems = takenQuery.Where(x => x.ContEducationRequirementId == item).ToList();
                foreach (var completedItem in completedItems)
                {
                    _completedItems.Add(new AgentContEduCompletedItem
                    {
                        EmployeeEducationID = completedItem.EmployeeEducationId,
                        EducationName = completedItem.EducationName,
                        ContEducationRequirementID = completedItem.ContEducationRequirementId,
                        ContEducationTakenDate = completedItem.ContEducationTakenDate,
                        CreditHoursTaken = completedItem.CreditHoursTaken,
                        AdditionalNotes = completedItem.AdditionalNotes
                    });
                }
            }

            return (_requiredItems.ToArray(), _completedItems.ToArray());
        }
        protected (DiaryCreatedByItem[], DiaryItem[]) FillDiaryInfo(int vEmploymentID)
        {
            List<DiaryCreatedByItem> _diaryCreatedByItems = new List<DiaryCreatedByItem>();

            var queryCreatedBy = (from diary in _db.Diaries
                                  join lt in _db.LicenseTeches on diary.Soeid equals lt.Soeid into ltGroup
                                  from lt in ltGroup.DefaultIfEmpty()
                                  where diary.EmploymentId == vEmploymentID
                                  select new DiaryCreatedByItem
                                  {
                                      SOEID = diary.Soeid.Trim(),
                                      TechName = lt != null ? lt.FirstName + " " + lt.LastName : diary.Soeid
                                  }).Distinct();

            _diaryCreatedByItems.Add(new DiaryCreatedByItem { SOEID = "{All}", TechName = "ALL" });

            foreach (var item in queryCreatedBy)
            {
                _diaryCreatedByItems.Add(new DiaryCreatedByItem
                {
                    SOEID = item.SOEID,
                    TechName = item.TechName
                });
            }

            var entryItems = (from diary in _db.Diaries
                              join lt in _db.LicenseTeches on diary.Soeid equals lt.Soeid into ltGroup
                              from lt in ltGroup.DefaultIfEmpty()
                              where diary.EmploymentId == vEmploymentID
                              select new DiaryItem
                              {
                                  DiaryID = diary.DiaryId,
                                  SOEID = diary.Soeid,
                                  DiaryName = diary.DiaryName,
                                  DiaryDate = diary.DiaryDate,
                                  //TechName = lt != null ? lt.FirstName + " " + lt.LastName : diary.Soeid,
                                  TechName = lt != null ? lt.FirstName.Substring(0, 1) + ". " + lt.LastName : diary.Soeid,
                                  Notes = diary.Notes
                              })
                              .Distinct()
                              .OrderByDescending(d => d.DiaryDate)
                              .ToList();

            return (_diaryCreatedByItems.ToArray(), entryItems.ToArray());
        }
        private List<TicklerItem> FillTicklerItems(int vEmploymentID)
        {
            var query = from t in _db.Ticklers
                        where t.EmploymentId == vEmploymentID && t.TicklerCloseByLicenseTechId == null
                        join lt in _db.LicenseTeches on t.LicenseTechId equals lt.LicenseTechId
                        join m in _db.Employments on t.EmploymentId equals m.EmploymentId into mGroup
                        from m in mGroup.DefaultIfEmpty()
                        join e in _db.Employees on m.EmployeeId equals e.EmployeeId into eGroup
                        from e in eGroup.DefaultIfEmpty()
                        join el in _db.EmployeeLicenses on t.EmployeeLicenseId equals el.EmployeeLicenseId into elGroup
                        from el in elGroup.DefaultIfEmpty()
                        join l in _db.Licenses on el.LicenseId equals l.LicenseId into lGroup
                        from l in lGroup.DefaultIfEmpty()
                        join loa in _db.LineOfAuthorities on l.LineOfAuthorityId equals loa.LineOfAuthorityId into loaGroup
                        from loa in loaGroup.DefaultIfEmpty()
                        orderby t.TicklerDate descending
                        select new TicklerItem
                        {
                            TicklerId = t.TicklerId,
                            TicklerDate = t.TicklerDate,
                            TicklerDueDate = t.TicklerDueDate,
                            LicenseTechId = t.LicenseTechId,
                            EmploymentId = t.EmploymentId,
                            EmployeeLicenseId = t.EmployeeLicenseId,
                            EmployeeId = e.EmployeeId,
                            LineOfAuthorityName = loa.LineOfAuthorityAbv,
                            TeamMemberName = e.LastName + ", " + e.FirstName,
                            Geid = e.Geid,
                            //Message = t.Message,
                            Message = (e.FirstName + " " + e.LastName + "\r\n" + "TM-" + e.Geid + "\r\n" ?? "") +
                                                      (loa.LineOfAuthorityName + "\r\n" ?? "") +
                                                      (t.LkpValue == "Other" ? t.Message : t.LkpValue),
                            LkpValue = t.LkpValue,
                        };

            var ticklerItems = query.AsNoTracking().ToList();

            return ticklerItems;
        }
        private List<WorklistItem> FillWorklistItems(string vTmNumber)
        {
            //var result = _db.WorkListData
            //        .AsEnumerable() // Switch to in-memory processing
            //        .Where(w => w.WorkListData != null &&
            //                    w.WorkListData.Split('|').ElementAtOrDefault(1) == vTmNumber && 
            //                    w.ProcessDate == null &&
            //                    w.ProcessedBy == null)
            //        .Select(w => new WorklistItem
            //        {
            //            WorkListDataID = w.WorkListDataId,
            //            WorkListName = w.WorkListName,
            //            CreateDate = w.CreateDate,
            //            LicenseTech = w.LicenseTech
            //        })
            //        .ToList();
            var result = _db.WorkListData
                        .Where(w => w.WorkListData != null && w.ProcessDate == null && w.ProcessedBy == null)
                        .AsEnumerable() // Switch to in-memory processing
                        .Where(w => w.WorkListData.Split('|').ElementAtOrDefault(1) == vTmNumber ||
                                    w.WorkListData.Split('|').ElementAtOrDefault(1) == "T" + vTmNumber)
                        .Select(w => new WorklistItem
                        {
                            WorkListDataID = w.WorkListDataId,
                            WorkListName = w.WorkListName,
                            CreateDate = w.CreateDate,
                            LicenseTech = w.LicenseTech
                        })
                        .ToList();

            return result;
        }
        #endregion
    }
}
