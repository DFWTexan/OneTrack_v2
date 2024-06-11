using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DbData;
using OneTrack_v2.Services;
using OneTrak_v2.DataModel;
using OneTrak_v2.Server.DbData.DataModel.LincenseInfo;
using System.Data;

namespace OneTrak_v2.Services
{
    public class LicenseInfoService : ILicenseInfoService
    {
        private readonly IConfiguration _config;
        private readonly AppDataContext _db;
        private readonly IUtilityHelpService _utilityService;
        private readonly string? _connectionString;

        public LicenseInfoService(AppDataContext db, IUtilityHelpService utilityHelpService, IConfiguration config)
        {
            _db = db;
            _config = config;
            _utilityService = utilityHelpService;
            _connectionString = _config.GetConnectionString(name: "DefaultConnection");
        }

        public ReturnResult GetIncentiveInfo(int vEmployeelicenseID)
        {
            var result = new ReturnResult();
            try
            {
                var sql = @"
                       SELECT  DISTINCT
							m.EmployeeID,
							m.EmploymentID,
							m.Email, 
							m.LicenseIncentive,
							e.Alias, 
							el.EmployeeLicenseID,
							el.LicenseStatus,
							License.StateProvinceAbv,
							License.LicenseID,
							License.LicenseName,
							el.LicenseNumber,
							ISNULL(CONVERT(VARCHAR(10), el.LicenseIssueDate, 101), null) as LicenseIssueDate,
							ISNULL(CONVERT(VARCHAR(10), el.LineOfAuthorityIssueDate, 101), null) as LineOfAuthorityIssueDate,
							ISNULL(CONVERT(VARCHAR(10), el.LicenseEffectiveDate, 101), null) as LicenseEffectiveDate,
							ISNULL(CONVERT(VARCHAR(10), el.LicenseExpireDate, 101), null) as LicenseExpireDate,
							el.Reinstatement,
							el.Required,
							ISNULL(el.NonResident, 0) as NonResident,
							th.BranchCode,
							y.AscLicense,
							el.AscEmployeeLicenseID,
							el.LicenseNote,
							ISNULL(eli.EmploymentLicenseIncentiveID, 0) as EmploymentLicenseIncentiveID,
							eli.RollOutGroup,
							(ISNULL(e1.LastName, '') + CASE WHEN e1.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(e1.FirstName, '') + ' ' + ISNULL(e1.MiddleName, '') ) AS DMMgrName,
							j1.JobTitle AS DMMgrJobTitle,										
							--e1.GEID AS Mgr1TMNum,
							(ISNULL(e2.LastName, '') + CASE WHEN e2.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(e2.FirstName, '') + ' ' + ISNULL(e2.MiddleName, '') ) AS CCdBRMgrName,
							j2.JobTitle AS CCdBRMgrJobTitle,
							--e2.GEID AS Mgr1TMNum,
							ISNULL((ISNULL(lt.LastName, '') + CASE WHEN lt.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(lt.FirstName, '') + ' ' ),'UNKNOWN') AS DMSentBy,
							ISNULL(CONVERT(VARCHAR(10), eli.DMSentDate,101),NULL) AS DMSentDate,
							ISNULL(CONVERT(VARCHAR(10), eli.DMApprovalDate,101),NULL) AS DMApprovalDate,
							ISNULL(CONVERT(VARCHAR(10), eli.DMDeclinedDate,101),NULL) AS DMDeclinedDate,

							CASE 
							   WHEN eli.DMApprovalDate IS NOT NULL AND eli.DMDeclinedDate IS NULL THEN 'A' 
							   WHEN eli.DMApprovalDate IS NULL AND eli.DMDeclinedDate IS NOT NULL THEN 'D' 
							   WHEN eli.DMApprovalDate IS NULL AND eli.DMDeclinedDate IS NULL THEN 'P'
							   ELSE 'N/A'
							END 
							AS DMApprovalStatus,
							'DM Followup By ' + ISNULL(CONVERT(VARCHAR(10), DATEADD(Day,10,eli.DMSentDate),101),'N/A') AS DM10DayFollowUp,
							ISNULL(CONVERT(VARCHAR(10), eli.DM10DaySentDate,101),NULL) AS DM10DaySentDate,
							ISNULL((ISNULL(lt2.LastName, '') + CASE WHEN lt2.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(lt2.FirstName, '') + ' ' ),'UNKNOWN') AS DM10DaySentBy,

							'DM Followup By ' + ISNULL(CONVERT(VARCHAR(10), DATEADD(Day,10,eli.DM10DaySentDate),101),'N/A') AS DM20DayFollowUp,
							ISNULL(CONVERT(VARCHAR(10), eli.DM20DaySentDate,101),NULL) AS DM20DaySentDate,
							ISNULL((ISNULL(lt3.LastName, '') + CASE WHEN lt3.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(lt3.FirstName, '') + ' ' ),'UNKNOWN') AS DM20DaySentBy,


							--eli.DMComment,

							CASE 
							   WHEN eli.DMApprovalDate IS NOT NULL AND eli.DMDeclinedDate IS NULL THEN 'Y' 
							   ELSE 'N'
							END 
							AS TMProgramEligable,

							(ISNULL(e3.LastName, '') + CASE WHEN e3.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(e3.FirstName, '') + ' ' + ISNULL(e3.MiddleName, '') ) AS CCd2BRMgrName,
							j3.JobTitle AS CCd2BRMgrJobTitle,


							ISNULL((ISNULL(lt4.LastName, '') + CASE WHEN lt4.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(lt4.FirstName, '') + ' ' ),'UNKNOWN') AS TMSentBy,

							ISNULL(CONVERT(VARCHAR(10), eli.TMSentDate,101),NULL) AS TMSentDate,
							ISNULL(CONVERT(VARCHAR(10), eli.TMApprovalDate,101),NULL) AS TMApprovalDate,
							ISNULL(CONVERT(VARCHAR(10), eli.TMDeclinedDate,101),NULL) AS TMDeclinedDate,

							CASE 
							   WHEN eli.TMSentDate IS NOT NULL AND eli.TMApprovalDate IS NOT NULL AND eli.TMDeclinedDate IS NULL THEN 'A' 
							   WHEN eli.TMSentDate IS NOT NULL AND eli.TMApprovalDate IS NULL AND eli.TMDeclinedDate IS NOT NULL THEN 'D' 
							   WHEN (DATEDIFF(DAY,eli.TM10DaySentDate,GETDATE()) <= 10 OR eli.TM10DaySentDate IS NULL)  AND eli.TMApprovalDate IS NULL AND eli.TMDeclinedDate IS NULL THEN 'P'
							   WHEN DATEDIFF(DAY,eli.TM10DaySentDate,GETDATE()) > 10 AND eli.TMApprovalDate IS NULL AND eli.TMDeclinedDate IS NULL THEN 'N'
							   ELSE 'N/A'
							END 
							AS TMApprovalStatus,

							'TM Followup By ' + ISNULL(CONVERT(VARCHAR(10), DATEADD(Day,10,eli.TMSentDate),101),'N/A') AS TM10DayFollowUp,

							ISNULL(CONVERT(VARCHAR(10), eli.TM10DaySentDate,101),NULL) AS TM10DaySentDate,
							ISNULL((ISNULL(lt5.LastName, '') + CASE WHEN lt5.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(lt5.FirstName, '') + ' ' ),'UNKNOWN') AS TM10DaySentBy,

							ISNULL(CONVERT(VARCHAR(10), eli.TM45DaySentDate,101),NULL) AS TM45DaySentDate,
							ISNULL((ISNULL(lt7.LastName, '') + CASE WHEN lt7.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(lt7.FirstName, '') + ' ' ),'UNKNOWN') AS TM45DaySentBy,

							ISNULL(CONVERT(VARCHAR(10), pe.[EducationStartDate],101),NULL) AS EnrollDate,

							eli.TMComment,
							eli.DMComment,
							ISNULL(CONVERT(VARCHAR(10), eli.TMExceptionDate,101),NULL) AS TMExceptionDate,
							eli.TMException,

							ISNULL(CONVERT(VARCHAR(10), 
							CASE 
							   WHEN eli.TMExceptionDate IS NOT NULL THEN 
								ISNULL(CONVERT(VARCHAR(10), DATEADD(Day,90,eli.TMExceptionDate),101),NULL)
							   ELSE 
							   ISNULL(CONVERT(VARCHAR(10), DATEADD(Day,90,pe.[EducationStartDate]),101),NULL)
							END,101),NULL) AS TM90DayExpirationDate,

							ISNULL((ISNULL(lt6.LastName, '') + CASE WHEN lt6.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(lt6.FirstName, '') + ' ' ),'UNKNOWN') AS TMOkToSellSentBy,
							ISNULL(CONVERT(VARCHAR(10), eli.TMOkToSellSentDate,101),NULL) AS TMOkToSellSentDate,

							(ISNULL(e4.LastName, '') + CASE WHEN e4.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(e4.FirstName, '') + ' ' + ISNULL(e4.MiddleName, '') ) AS CCdOkToSellBRMgrName,
							j4.JobTitle AS CCdOkToSellBRMgrJobTitle,
							ISNULL(CONVERT(VARCHAR(10), el.LicenseIssueDate,101),NULL) AS OriginalIssueDate,
							ISNULL(CONVERT(VARCHAR(10), cd.CertificationDate,101),NULL) AS CertificationDate,
							ISNULL(CONVERT(VARCHAR(10), md.Mod2Date,101),NULL) AS Mod2Date,

							CASE
								WHEN ap.AppointmentEffectiveDate <=
								CASE 
							   WHEN eli.TMExceptionDate IS NOT NULL THEN 
							   ISNULL(CONVERT(VARCHAR(10), DATEADD(Day,90,eli.TMExceptionDate),101),NULL)
							   ELSE 
							   ISNULL(CONVERT(VARCHAR(10), DATEADD(Day,90,pe.[EducationStartDate]),101),NULL)
							END THEN 'Y'
							ELSE 'N' END AS CompletedIn90Days,


							ISNULL(CONVERT(VARCHAR(10), ap.AppointmentEffectiveDate,101),NULL) AS AppointmentEffectiveDate,
							ISNULL(CONVERT(VARCHAR(10), eli.[TMOMSApprtoSendToHRDate],101),NULL) AS TMOMSApprtoSendToHRDate,
							ISNULL(CONVERT(VARCHAR(10), eli.[TMSentToHRDate],101),NULL) AS TMSentToHRDate,
							ISNULL(CONVERT(VARCHAR(10), FORMAT(eli.[IncetivePeriodDate], 'MMM-yyyy')),NULL) AS IncetivePeriodDate,
							eli.IncentiveStatus,
							eli.Notes
						FROM dbo.Employee e
							 INNER JOIN dbo.Employment m ON e.EmployeeID = m.EmployeeID 
							 INNER JOIN dbo.Address a ON e.AddressID = a.AddressID
							 LEFT OUTER JOIN dbo.TransferHistory th ON m.EmploymentID = th.EmploymentID AND th.IsCurrent = 1
							 INNER JOIN dbo.EmployeeLicense  el ON m.EmploymentID = el.EmploymentID
							 INNER JOIN dbo.License ON el.LicenseID = License.LicenseID
							 LEFT JOIN (SELECT    
											 eel.EmployeeLicenseID,
											 ISNULL(ee.Alias,'') + '-' + 
											 ISNULL(eel.LicenseStatus,'') + '-' + 
											 ISNULL(ll.StateProvinceAbv,'') + '-' + 
											 ISNULL(ll.LicenseName,'') + '-' + 
											 ISNULL(eel.LicenseNumber,'') AS 'AscLicense'
										FROM dbo.Employee ee 
										INNER JOIN dbo.Employment mm ON ee.EmployeeID = mm.EmployeeID 
										INNER JOIN dbo.Address aa ON ee.AddressID = aa.AddressID
										INNER JOIN dbo.EmployeeLicense eel ON mm.EmploymentID = eel.EmploymentID
										INNER JOIN dbo.License  ll ON eel.LicenseID = ll.LicenseID
										) y ON el.AscEmployeeLicenseID = y.EmployeeLicenseID 
							LEFT OUTER JOIN dbo.EmploymentLicenseIncentive eli ON el.EmployeeLicenseID = eli.EmployeeLicenseID
						--DM
							LEFT OUTER JOIN [dbo].[Employment] m1 ON eli.DMEmploymentID = m1.EmploymentID
							LEFT OUTER JOIN [dbo].[EmploymentJobTitle] ej1 ON m1.EmploymentID = ej1.EmploymentID AND ej1.[IsCurrent] = 1
							LEFT OUTER JOIN [dbo].[JobTitles] j1 ON ej1.JobTitleID = j1.JobTitleID
							LEFT OUTER JOIN [dbo].[Employee] e1 ON m1.EmployeeID = e1.EmployeeID
						--CCdBM
							LEFT OUTER JOIN [dbo].[Employment] m2 ON eli.CCdBMEmploymentID = m2.EmploymentID
							LEFT OUTER JOIN [dbo].[EmploymentJobTitle] ej2 ON m2.EmploymentID = ej2.EmploymentID AND ej2.[IsCurrent] = 1
							LEFT OUTER JOIN [dbo].[JobTitles] j2 ON ej2.JobTitleID = j2.JobTitleID
							LEFT OUTER JOIN [dbo].[Employee] e2 ON m2.EmployeeID = e2.EmployeeID
						--CCdBM2
							LEFT OUTER JOIN [dbo].[Employment] m3 ON eli.CCd2BMEmploymentID = m3.EmploymentID
							LEFT OUTER JOIN [dbo].[EmploymentJobTitle] ej3 ON m3.EmploymentID = ej3.EmploymentID AND ej3.[IsCurrent] = 1
							LEFT OUTER JOIN [dbo].[JobTitles] j3 ON ej3.JobTitleID = j3.JobTitleID
							LEFT OUTER JOIN [dbo].[Employee] e3 ON m3.EmployeeID = e3.EmployeeID
						--CCdBMOkToSell
							LEFT OUTER JOIN [dbo].[Employment] m4 ON eli.CCOkToSellBMEmploymentID = m4.EmploymentID
							LEFT OUTER JOIN [dbo].[EmploymentJobTitle] ej4 ON m4.EmploymentID = ej4.EmploymentID AND ej4.[IsCurrent] = 1
							LEFT OUTER JOIN [dbo].[JobTitles] j4 ON ej4.JobTitleID = j4.JobTitleID
							LEFT OUTER JOIN [dbo].[Employee] e4 ON m4.EmployeeID = e4.EmployeeID

							LEFT OUTER JOIN [dbo].[LicenseTech] lt ON eli.DMSentBySOEID = lt.SOEID
							LEFT OUTER JOIN [dbo].[LicenseTech] lt2 ON eli.DM10DaySentBySOEID = lt2.SOEID
							LEFT OUTER JOIN [dbo].[LicenseTech] lt3 ON eli.DM20DaySentBySOEID = lt3.SOEID
							LEFT OUTER JOIN [dbo].[LicenseTech] lt4 ON eli.TMSentBySOEID = lt4.SOEID
							LEFT OUTER JOIN [dbo].[LicenseTech] lt5 ON eli.TM10DaySentBySOEID = lt5.SOEID
							LEFT OUTER JOIN [dbo].[LicenseTech] lt6 ON eli.TMOkToSellSentBySOEID = lt6.SOEID
							LEFT OUTER JOIN [dbo].[LicenseTech] lt7 ON eli.TM45DaySentBySOEID = lt7.SOEID

							LEFT OUTER JOIN (SELECT 
												[EmployeeLicenseID], MIN([EducationStartDate]) AS [EducationStartDate]
											FROM [dbo].[EmployeeLicenseePreEducation]
												GROUP BY [EmployeeLicenseID]
											) pe ON eli.EmployeeLicenseID = pe.EmployeeLicenseID
							LEFT OUTER JOIN (SELECT 
												[EmployeeLicenseID], MIN([AppointmentEffectiveDate]) AS AppointmentEffectiveDate
											FROM [dbo].[EmployeeAppointment]
												GROUP BY[EmployeeLicenseID]
											) ap ON eli.EmployeeLicenseID = ap.EmployeeLicenseID

							LEFT OUTER JOIN (SELECT 
												[EmploymentID]
												, [asset_id]
												, [learning_program_status]
												, MIN([learning_program_completion_date]) AS Mod2Date
											FROM [License].[dbo].[EmploymentCompanyRequirements]
											WHERE [learning_program_status] = 'Completed' 
												AND (UPPER([asset_id]) LIKE '%MODULE2%' 
												OR UPPER([asset_id]) LIKE '%INS_NONCREDIT_PRODUCTS%' 
												OR UPPER([asset_id]) LIKE UPPER('%Optional_Products_-_Non-Credit_Insurance%'))
											GROUP BY [EmploymentID], [asset_id], [learning_program_status]
											) md ON m.EmploymentID = md.EmploymentID
							LEFT OUTER JOIN (SELECT 
												[EmploymentID]
												, [asset_id]
												, [learning_program_status]
												, MIN([learning_program_completion_date]) AS CertificationDate
											FROM [License].[dbo].[EmploymentCompanyRequirements]
											WHERE [learning_program_status] = 'Completed' 
												AND UPPER([asset_id]) LIKE '%Compliance Certification%'
											GROUP BY [EmploymentID], [asset_id], [learning_program_status]
											) cd ON m.EmploymentID = cd.EmploymentID
						WHERE el.EmployeeLicenseID = @EmployeeLicenseID";

                var parameters = new[] { new SqlParameter("@EmployeeLicenseID", vEmployeelicenseID) };

                var queryLicenseInfoResults = _db.OputLicenseIncentiveInfo
                                            .FromSqlRaw(sql, parameters)
                                            .AsNoTracking()
                                            .FirstOrDefault();

                result.Success = true;
                result.ObjData = queryLicenseInfoResults;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "EMFTEST-Source", new { }, null);
            }

            return result;
        }
        public ReturnResult GetIncentiveRolloutGroups()
        {
            var result = new ReturnResult();
            try
            {
                var sql = @"SELECT  LkpField,	LkpValue,	SortOrder  
							FROM [License].[dbo].[lkp_TypeStatus]
							WHERE [LkpField] = 'RolloutGroup'
							UNION
							SELECT 'RolloutGroup' AS LkpField,'RolloutGroup' AS LkpValue, '0' AS SortOrder
							ORDER BY SortOrder";

                var queryRolloutGrops = _db.OputIncentiveRolloutGroup
                                       .FromSqlRaw(sql)
                                       .AsNoTracking()
                                       .ToList();

                result.Success = true;
                result.ObjData = queryRolloutGrops;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "EMFTEST-Source", new { }, null);
            }

            return result;
        }
        public ReturnResult GetIncentiveBMMgrs()
        {
            var result = new ReturnResult();
            try
            {
                var sql = @"SELECT    
								m2.EmploymentID AS BMMgrEmploymentID,
								(ISNULL(e2.LastName, '') + CASE WHEN e2.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(e2.FirstName, '') + ' ' + ISNULL(e2.MiddleName, '') ) AS BMMgrName
							FROM dbo.Employee e 
							INNER JOIN dbo.Employment m ON e.EmployeeID = m.EmployeeID 
						--CCdBM
							LEFT OUTER JOIN [dbo].[Employment] m2 ON m.H1EmploymentID = m2.EmploymentID
							LEFT OUTER JOIN [dbo].[EmploymentJobTitle] j2 ON m2.EmploymentID = j2.EmploymentID 
								AND j2.[IsCurrent] = 1
							LEFT OUTER JOIN [dbo].[Employee] e2 ON m2.EmployeeID = e2.EmployeeID
							WHERE 
								m2.EmploymentID IS NOT NULL

							UNION

							SELECT    
								m2.EmploymentID AS BMMgrEmploymentID,
								(ISNULL(e2.LastName, '') + CASE WHEN e2.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(e2.FirstName, '') + ' ' + ISNULL(e2.MiddleName, '') ) AS BMMgrName
							FROM dbo.EmploymentLicenseIncentive eli
						--CCdBM
							LEFT OUTER JOIN [dbo].[Employment] m2 ON eli.CCdBMEmploymentID = m2.EmploymentID 
								OR eli.CCd2BMEmploymentID = m2.EmploymentID 
								OR eli.CCOkToSellBMEmploymentID = m2.EmploymentID
							LEFT OUTER JOIN [dbo].[EmploymentJobTitle] j2 ON m2.EmploymentID = j2.EmploymentID 
								AND j2.[IsCurrent] = 1
							LEFT OUTER JOIN [dbo].[Employee] e2 ON m2.EmployeeID = e2.EmployeeID
							WHERE 
								m2.EmploymentID IS NOT NULL
							GROUP BY
								m2.EmploymentID,
								(ISNULL(e2.LastName, '') + CASE WHEN e2.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(e2.FirstName, '') + ' ' + ISNULL(e2.MiddleName, '') ) 

							UNION

							SELECT '0' AS BMMgrEmploymentID, 'BMMgrName'

							ORDER BY
								(ISNULL(e2.LastName, '') + CASE WHEN e2.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(e2.FirstName, '') + ' ' + ISNULL(e2.MiddleName, '') ) ";

                var queryBMMgrs = _db.OputIncentiveBMMgr
                                       .FromSqlRaw(sql)
                                       .AsNoTracking()
                                       .ToList();

                result.Success = true;
                result.ObjData = queryBMMgrs;
                result.StatusCode = 200;
			
			}
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "EMFTEST-Source", new { }, null);
            }

            return result;
        }
        public ReturnResult GetIncentiveDMMrgs()
        {
            var result = new ReturnResult();
            try
            {
                var sql = @"SELECT    
								 m1.EmploymentID AS DMMgrEmploymentID,
								(ISNULL(e1.LastName, '') + CASE WHEN e1.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(e1.FirstName, '') + ' ' + ISNULL(e1.MiddleName, '') ) AS DMMgrName
							FROM dbo.Employee e
							INNER JOIN dbo.Employment m ON e.EmployeeID = m.EmployeeID 
						--DM
							LEFT OUTER JOIN [dbo].[Employment] m1 ON m.H2EmploymentID = m1.EmploymentID
							LEFT OUTER JOIN [dbo].[EmploymentJobTitle] j1 ON m1.EmploymentID = j1.EmploymentID 
								AND j1.[IsCurrent] = 1
							LEFT OUTER JOIN [dbo].[Employee] e1 ON m1.EmployeeID = e1.EmployeeID
							WHERE 
								m1.EmploymentID IS NOT NULL

							UNION

							SELECT    
								 m1.EmploymentID AS DMMgrEmploymentID,
								(ISNULL(e1.LastName, '') + CASE WHEN e1.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(e1.FirstName, '') + ' ' + ISNULL(e1.MiddleName, '') ) AS DMMgrName
							FROM dbo.EmploymentLicenseIncentive eli
						--DM
							LEFT OUTER JOIN [dbo].[Employment] m1 ON eli.DMEmploymentID = m1.EmploymentID
							LEFT OUTER JOIN [dbo].[EmploymentJobTitle] j1 ON m1.EmploymentID = j1.EmploymentID 
								AND j1.[IsCurrent] = 1
							LEFT OUTER JOIN [dbo].[Employee] e1 ON m1.EmployeeID = e1.EmployeeID
							WHERE 
								m1.EmploymentID IS NOT NULL
							GROUP BY
								 m1.EmploymentID,
								(ISNULL(e1.LastName, '') + CASE WHEN e1.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(e1.FirstName, '') + ' ' + ISNULL(e1.MiddleName, '') )

							UNION

							SELECT '0' AS DMMgrEmploymentID, 'DMMgrName'

							ORDER BY
								(ISNULL(e1.LastName, '') + CASE WHEN e1.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(e1.FirstName, '') + ' ' + ISNULL(e1.MiddleName, '') ) ";

                var queryDMMrgs = _db.OputIncentiveDMMgr
                                       .FromSqlRaw(sql)
                                       .AsNoTracking()
                                       .ToList();

                result.Success = true;
                result.ObjData = queryDMMrgs;
                result.StatusCode = 200;
			}
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "EMFTEST-Source", new { }, null);
            }

            return result;
        }
        public ReturnResult GetIncentiveTechNames()
        {
            var result = new ReturnResult();
            try
            {
                var sql = @"SELECT [SOEID], ISNULL((ISNULL(lt.LastName, '') + CASE WHEN lt.LastName IS NULL THEN NULL ELSE ', ' END +  ISNULL(lt.FirstName, '') + ' ' ),'UNKNOWN') AS TechName
							FROM [dbo].[LicenseTech] lt
							WHERE [IsActive] = 1
							UNION
							SELECT '0' AS SOEID, 'TechName' AS TechName
							UNION
							SELECT 'OneTrak' AS SOEID, 'OneTrak' AS TechName
							ORDER BY TechName ";

                var queryTechNames = _db.OputIncentiveTechName
                                       .FromSqlRaw(sql)
                                       .AsNoTracking()
                                       .ToList();

                result.Success = true;
                result.ObjData = queryTechNames;
                result.StatusCode = 200;
			}
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "EMFTEST-Source", new { }, null);
            }

            return result;
        }
        public ReturnResult GetAffiliatedLicenses(string vStateProvinceAbv, int vLicenseID)
        {
            var result = new ReturnResult();
            try
            {
                var query = from employee in _db.Employees
                            join employment in _db.Employments on employee.EmployeeId equals employment.EmployeeId
                            join address in _db.Addresses on employee.AddressId equals address.AddressId
                            join employeeLicense in _db.EmployeeLicenses on employment.EmploymentId equals employeeLicense.EmploymentId
                            join license in _db.Licenses on employeeLicense.LicenseId equals license.LicenseId
                            where employeeLicense.LicenseStatus == "Active"
                                  && license.StateProvinceAbv == vStateProvinceAbv
                                  && license.LicenseId == vLicenseID
                            orderby (employee.Alias ?? "") + "-" +
                                    (employeeLicense.LicenseStatus ?? "") + "-" +
                                    (license.StateProvinceAbv ?? "") + "-" +
                                    (license.LicenseName ?? "") + "-" +
                                    (employeeLicense.LicenseNumber ?? "")
                            select new
                            {
                                EmployeeLicenseID = employeeLicense.EmployeeLicenseId,
                                AscLicense = (employee.Alias ?? "") + "-" +
                                             (employeeLicense.LicenseStatus ?? "") + "-" +
                                             (license.StateProvinceAbv ?? "") + "-" +
                                             (license.LicenseName ?? "") + "-" +
                                             (employeeLicense.LicenseNumber ?? "")
                            };

				var affiliatedLicenses = query.AsNoTracking().ToList();	

                result.Success = true;
				result.ObjData = affiliatedLicenses;
                result.StatusCode = 200;
			}
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "EMFTEST-Source", new { }, null);
            }

            return result;

        }
        
		#region INSERT/UPDATE/DELETE
        public ReturnResult AddLicenseAppointment([FromBody] IputAddLicenseAppointment vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspAppointmentInsert", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EmployeeID", vInput.EmployeeID));
                        cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", vInput.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@CarrierDate", vInput.CarrierDate));
                        cmd.Parameters.Add(new SqlParameter("@AppointmentEffectiveDate", vInput.AppointmentEffectiveDate));
                        cmd.Parameters.Add(new SqlParameter("@AppointmentExpireDate", vInput.AppointmentExpireDate));
                        cmd.Parameters.Add(new SqlParameter("@AppointmentTerminationDate", vInput.AppointmentTerminationDate));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "License Appointment Added Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# LINFO-6128-198791].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpdateLicenseAppointment([FromBody] IputUpdateLicenseAppointment vInput)
		{
            var result = new ReturnResult();
            try
			{
                using (SqlConnection conn = new SqlConnection(_connectionString))
				{
                    using (SqlCommand cmd = new SqlCommand("uspAppointmentUpdate", conn))
					{
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EmployeeAppointmentID", vInput.EmployeeAppointmentID));
                        cmd.Parameters.Add(new SqlParameter("@AppointmentStatus", vInput.AppointmentStatus));
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", vInput.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@CarrierDate", vInput.CarrierDate));
                        cmd.Parameters.Add(new SqlParameter("@AppointmentEffectiveDate", vInput.AppointmentEffectiveDate));
                        cmd.Parameters.Add(new SqlParameter("@AppointmentExpireDate", vInput.AppointmentExpireDate));
                        cmd.Parameters.Add(new SqlParameter("@AppointmentTerminationDate", vInput.AppointmentTerminationDate));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "License Appointment Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
			{
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# LINFO-6128-192719].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteLicenseAppointment([FromBody] IputDeleteLicenseAppointment vInput)
		{
			var result = new ReturnResult();
            try
			{
                using (SqlConnection conn = new SqlConnection(_connectionString))
				{
                    using (SqlCommand cmd = new SqlCommand("uspEmployeeAppointmentDelete", conn))
					{
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EmployeeAppointmentID", vInput.EmployeeAppointmentID));
                        cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "License Appointment Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
			{
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# LINFO-6128-198733].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
		}
        public ReturnResult UpsertLicenseApplication([FromBody] IputUpsertLicenseApplication vInput)
		{
            var result = new ReturnResult();
            try
            {
                if (vInput.LicenseApplicationID == 0)
                {
                   // INSERT Continuing Education Taken
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspLicenseApplicationsInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                            cmd.Parameters.Add(new SqlParameter("@ApplicationStatus", vInput.ApplicationStatus));
                            cmd.Parameters.Add(new SqlParameter("@ApplicationType", vInput.ApplicationType));
                            cmd.Parameters.Add(new SqlParameter("@RenewalDate", vInput.RenewalDate));
                            cmd.Parameters.Add(new SqlParameter("@RenewalMethod", vInput.RenewalMethod));
                            cmd.Parameters.Add(new SqlParameter("@SentToAgentDate", vInput.SentToAgentDate));
                            cmd.Parameters.Add(new SqlParameter("@RecFromAgentDate", vInput.RecFromAgentDate));
                            cmd.Parameters.Add(new SqlParameter("@SentToStateDate", vInput.SentToStateDate));
                            cmd.Parameters.Add(new SqlParameter("@RecFromStateDate", vInput.RecFromStateDate));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE Continuing Education Taken
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspLicenseApplicationUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@LicenseApplicationID", vInput.LicenseApplicationID));
                            cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                            cmd.Parameters.Add(new SqlParameter("@ApplicationStatus", vInput.ApplicationStatus));
                            cmd.Parameters.Add(new SqlParameter("@ApplicationType", vInput.ApplicationType));
                            cmd.Parameters.Add(new SqlParameter("@RenewalDate", vInput.RenewalDate));
                            cmd.Parameters.Add(new SqlParameter("@RenewalMethod", vInput.RenewalMethod));
                            cmd.Parameters.Add(new SqlParameter("@SentToAgentDate", vInput.SentToAgentDate));
                            cmd.Parameters.Add(new SqlParameter("@RecFromAgentDate", vInput.RecFromAgentDate));
                            cmd.Parameters.Add(new SqlParameter("@SentToStateDate", vInput.SentToStateDate));
                            cmd.Parameters.Add(new SqlParameter("@RecFromStateDate", vInput.RecFromStateDate));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "License Application Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# LINFO-6158-39882].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;

        }
        public ReturnResult DeleteLicenseApplication([FromBody] IputDeleteLicenseApplication vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspEmployeeAppointmentDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicenseApplicationID", vInput.LicenseApplicationID));
                        cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "License Application Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# LINFO-6328-19743].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertLicensePreEducation([FromBody] IputUpsertLicensePreEducation vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.EmployeeLicensePreEducationID == 0)
                {
                    // INSERT Continuing Pre Education
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspLicensePreEducationInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                            cmd.Parameters.Add(new SqlParameter("@Status", vInput.Status));
                            cmd.Parameters.Add(new SqlParameter("@EducationEndDate", vInput.EducationEndDate));
                            cmd.Parameters.Add(new SqlParameter("@PreEducationID", vInput.PreEducationID));
                            cmd.Parameters.Add(new SqlParameter("@EducationStartDate", vInput.EducationStartDate));
                            cmd.Parameters.Add(new SqlParameter("@AdditionalNotes", vInput.AdditionalNotes));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE Continuing Pre Education
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspEmployeeLicensePreEducationUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmployeeLicensePreEducationID", vInput.EmployeeLicensePreEducationID));
                            cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                            cmd.Parameters.Add(new SqlParameter("@Status", vInput.Status));
                            cmd.Parameters.Add(new SqlParameter("@EducationEndDate", vInput.EducationEndDate));
                            cmd.Parameters.Add(new SqlParameter("@PreEducationID", vInput.PreEducationID));
                            cmd.Parameters.Add(new SqlParameter("@EducationStartDate", vInput.EducationStartDate));
                            cmd.Parameters.Add(new SqlParameter("@AdditionalNotes", vInput.AdditionalNotes));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "License Pre-Education Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# LINFO-6158-79812].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;  
        }
        public ReturnResult DeleteLicensePreEducation([FromBody] IputDeleteLicensePreEducation vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspEmployeeLicenseePreEducationDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EmployeeLicensePreEducationID", vInput.EmployeeLicensePreEducationID));
                        cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "License Pre-Education Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# LINFO-6328-19049].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertLicensePreExam([FromBody] IputUpsertLicApplPreExam vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.EmployeeLicensePreExamID == 0)
                {
                    // INSERT Continuing Pre Exam
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspLicenseExamStatusInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                            cmd.Parameters.Add(new SqlParameter("@ExamID", vInput.ExamID));
                            cmd.Parameters.Add(new SqlParameter("@ExamTakenDate", vInput.ExamTakenDate));
                            cmd.Parameters.Add(new SqlParameter("@Status", vInput.Status));
                            cmd.Parameters.Add(new SqlParameter("@ExamScheduleDate", vInput.ExamScheduleDate));
                            cmd.Parameters.Add(new SqlParameter("@AdditionalNotes", vInput.AdditionalNotes));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE Continuing Pre Exam
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspLicenseExamStatusUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EmployeeLicensePreExamID", vInput.EmployeeLicensePreExamID));
                            cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                            cmd.Parameters.Add(new SqlParameter("@ExamTakenDate", vInput.ExamTakenDate));
                            cmd.Parameters.Add(new SqlParameter("@Status", vInput.Status));
                            cmd.Parameters.Add(new SqlParameter("@ExamID", vInput.ExamID));
                            cmd.Parameters.Add(new SqlParameter("@ExamScheduleDate", vInput.ExamScheduleDate));
                            cmd.Parameters.Add(new SqlParameter("@AdditionalNotes", vInput.AdditionalNotes));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "License Pre-Exam Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# LINFO-6358-19812].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteLicensePreExam([FromBody] IputDeleteLicApplPreExam vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspEmployeeLicenseExamStatusDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                        cmd.Parameters.Add(new SqlParameter("@EmployeeLicensePreExamID", vInput.EmployeeLicensePreExamID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "License Pre-Exam Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ObjData = null;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# LINFO-6328-19740].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        #endregion
    }
}
