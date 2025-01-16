using DataModel.Response;
using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DataModel;
using OneTrack_v2.DbData;

namespace OneTrack_v2.Services
{
    public class MiscService : IMiscService
    {
        private readonly AppDataContext _db;
        private readonly IUtilityHelpService _utilityService;

        public MiscService(AppDataContext db, IUtilityHelpService utilityHelpService)
        {
            _db = db;
            _utilityService = utilityHelpService;
        }

        public ReturnResult GetStateProvinces()
        {
            var result = new ReturnResult();
            try
            {
                var stateProvincses = _db.StateProvinces
                                    .OrderBy(_db => _db.StateProvinceName)
                                    .Select(_db => new OputVarDropDownList_v2
                                    {
                                        Value = _db.StateProvinceAbv,
                                        Label = _db.StateProvinceAbv
                                    });

                result.Success = true;
                result.StatusCode = 200;
                result.ObjData = stateProvincses;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetStateProvinces", new { }, null);
            }

            return result;
        }
        public ReturnResult GetBranches()
        {
            var result = new ReturnResult();
            try
            {

                var sql = @"SELECT 
                                BranchCode as [Value]
                                , TRIM(ISNULL(b.Name,CONCAT('ZZ', BranchCode))) AS [Label]
                            FROM 
                                (SELECT h.EmploymentID, BranchCode 
                                 FROM dbo.TransferHistory h
                                 INNER JOIN (
                                     SELECT h.EmploymentID, MAX(TransferDate) AS 'MaxDate'
                                     FROM dbo.TransferHistory h
                                     INNER JOIN dbo.Employment m ON h.EmploymentID = m.EmploymentID
                                     GROUP BY h.EmploymentID
                                 ) M ON h.EmploymentID = M.EmploymentID
                            WHERE BranchCode IS NOT NULL ) X
                            LEFT OUTER JOIN [dbo].[BIF] b ON RIGHT(x.BranchCode, 8) = RIGHT(b.HR_Department_ID, 8)
                            GROUP BY 
                                BranchCode, b.Name
                            ORDER BY 
                                TRIM(ISNULL(b.Name,CONCAT('ZZ', BranchCode)))";

                var queryResult = _db.Set<OputVarDropDownList_v2>()
                                      .FromSqlRaw(sql)
                                      .AsNoTracking()
                                      .ToList();

                result.ObjData = queryResult;
                result.Success = true;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetBranches", new { }, null);
            }

            return result;
        }
        public ReturnResult GetScoreNumbers()
        {
            var result = new ReturnResult();
            try
            {
                var sql = @"SELECT 
                                 ISNULL(ScoreNumber,'0000') AS [Value]
                                 , ISNULL(ScoreNumber,'0000') AS [Label]
                            FROM 
                                 (SELECT h.EmploymentID, BranchCode 
                                  FROM dbo.TransferHistory h
                                  INNER JOIN (
                                    SELECT h.EmploymentID, MAX(TransferDate) AS 'MaxDate'
                                    FROM dbo.TransferHistory h
                                    INNER JOIN dbo.Employment m ON h.EmploymentID = m.EmploymentID
                                    GROUP BY h.EmploymentID
                                 ) M ON h.EmploymentID = M.EmploymentID
                            WHERE BranchCode IS NOT NULL ) X
	                        Left OUTER JOIN [dbo].[BIF] b ON RIGHT(x.BranchCode,8) = RIGHT(b.HR_Department_ID,8)
                            GROUP BY 
                                  ScoreNumber
                            ORDER BY 
                                 ScoreNumber";

                var queryResult = _db.Set<OputVarDropDownList_v2>()
                                      .FromSqlRaw(sql)
                                      .AsNoTracking()
                                      .ToList();

                result.ObjData = queryResult;
                result.Success = true;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetScoreNumbers", new { }, null);
            }

            return result;
        }
        public ReturnResult GetEmployerAgencies()
        {
            var result = new ReturnResult();
            try
            {
                var resultEmpAgencies = _db.Companies
                                    .Where(c => c.CompanyType == "Employer" || c.CompanyType == "Agency")
                                    .Select(c => new { Value = c.CompanyId, Label = c.CompanyName })
                                    .AsNoTracking()
                                    .ToList();

                result.Success = true;
                result.ObjData = resultEmpAgencies;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetEmployerAgencies", new { }, null);
            }

            return result;
        }
        public ReturnResult GetLicenseStatuses()
        {
            var result = new ReturnResult();
            try
            {
                var resultLicStatuses = _db.LkpTypeStatuses
                            .Where(l => l.LkpField == "LicenseStatusSearch")
                            .OrderBy(l => l.SortOrder)
                            .Select(l => new { Value = l.LkpValue, Label = l.LkpValue })
                            .AsNoTracking()
                            .ToList();

                result.Success = true;
                result.ObjData = resultLicStatuses;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetLicenseStatuses", new { }, null);
            }

            return result;
        }
        public ReturnResult GetLicenseNames()
        {
            var result = new ReturnResult();
            try
            {
                var resultLicNames = _db.Licenses
                                .Join(_db.StateProvinces,
                                    license => license.StateProvinceAbv,
                                    stateProvince => stateProvince.StateProvinceAbv,
                                    (license, stateProvince) => new { License = license, StateProvince = stateProvince })
                                .GroupBy(l => l.License.LicenseName)
                                .AsNoTracking()
                                .Select(g => new { Value = g.Key, Label = g.Key })
                                .ToList();

                result.Success = true;
                result.ObjData = resultLicNames;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetLicenseNames", new { }, null);
            }

            return result;
        }
        public ReturnResult GetLicenseNumericNames(string vStateAbv)
        {
            var result = new ReturnResult();
            try
            {
                var resultLicNames = _db.Licenses
                                .Where(license => license.StateProvinceAbv == vStateAbv)
                                .GroupBy(l => new { l.LicenseId, l.LicenseName })
                                .AsNoTracking()
                                .Select(g => new { Value = g.Key.LicenseId, Label = g.Key.LicenseName })
                                .ToList();

                result.Success = true;
                result.ObjData = resultLicNames;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetLicenseNumericNames", new { }, null);
            }

            return result;
        }
        public ReturnResult GetEmailTemplates()
        {
            var result = new ReturnResult();
            try
            {
                // TBD: Placeholder method to return a list of email templates

                result.Success = true;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetEmailTemplates", new { }, null);
            }

            return result;
        }
        public ReturnResult GetTicklerMessageTypes()
        {
            var result = new ReturnResult();
            try
            {
                // TBD: Placeholder method to return a list of tickler message types

                result.Success = true;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetTicklerMessageTypes", new { }, null);
            }

            return result;
        }
        public ReturnResult WorkListNames()
        {
            var result = new ReturnResult();
            try
            {

                var activeWorkLists = _db.WorkLists
                                    .Where(wl => wl.IsActive == true)
                                    .OrderBy(wl => wl.WorkListName)
                                    .Select(wl => wl.WorkListName)
                                    .AsNoTracking()
                                    .ToList();

                result.Success = true;
                result.ObjData = activeWorkLists;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-WorkListNames", new { }, null);
            }

            return result;
        }
        public ReturnResult GetLicenseTeches()
        {
            var result = new ReturnResult();
            try
            {

                var query = from l in _db.LicenseTeches
                            where l.IsActive == true
                            select new
                            {
                                l.LicenseTechId,
                                l.Soeid,
                                l.FirstName,
                                l.LastName,
                                l.IsActive,
                                l.TeamNum,
                                l.LicenseTechPhone,
                                l.LicenseTechFax,
                                l.LicenseTechEmail,
                                TechName = l.FirstName + " " + l.LastName
                            };

                var resultLicTechs = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultLicTechs;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetLicenseTeches", new { }, null);
            }
            return result;
        }
        public ReturnResult GetLicenseTechByID(int vLicenseTechID)
        {

            var result = new ReturnResult();
            try
            {
                var query = from l in _db.LicenseTeches
                            where l.LicenseTechId == vLicenseTechID
                            select new
                            {
                                l.LicenseTechId,
                                l.Soeid,
                                l.FirstName,
                                l.LastName,
                                l.IsActive,
                                l.TeamNum,
                                l.LicenseTechPhone,
                                l.LicenseTechFax,
                                l.LicenseTechEmail,
                                TechName = l.FirstName + " " + l.LastName
                            };

                var resultLicTech = query.AsNoTracking().FirstOrDefault();

                result.Success = true;
                result.ObjData = resultLicTech;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetLicenseTechByID", new { }, null);
            }

            return result;
        }
        public ReturnResult GetLicenseTechBySOEID(string vSOEID)
        {

            var result = new ReturnResult();
            try
            {
                var query = from l in _db.LicenseTeches
                            where l.Soeid == vSOEID
                            select new
                            {
                                l.LicenseTechId,
                                l.Soeid,
                                l.FirstName,
                                l.LastName,
                                l.IsActive,
                                l.TeamNum,
                                l.LicenseTechPhone,
                                l.LicenseTechFax,
                                l.LicenseTechEmail,
                                TechName = l.FirstName + " " + l.LastName
                            };

                var resultLicTech = query.AsNoTracking().FirstOrDefault();

                result.Success = true;
                result.ObjData = resultLicTech;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetLicenseTechBySOEID", new { }, null);
            }

            return result;
        }
        public ReturnResult GetBackgroundStatuses()
        {
            var result = new ReturnResult();
            try
            {

                var query = from typeStatus in _db.LkpTypeStatuses
                            where typeStatus.LkpField == "BackgroundStatus"
                            orderby typeStatus.SortOrder, typeStatus.LkpValue
                            select new
                            {
                                LkpValue = typeStatus.LkpValue,
                                //SortOrder = typeStatus.SortOrder
                            };

                var resultBackgroundStatuses = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultBackgroundStatuses;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetBackgroundStatuses", new { }, null);
            }

            return result;
        }
        public ReturnResult GetJobTitles()
        {
            var result = new ReturnResult();
            try
            {

                var query = from j in _db.JobTitles
                            where j.IsActive == true
                            orderby j.JobTitle1
                            select new
                            {
                                Value = j.JobTitleId,
                                Label = j.JobTitle1
                            };

                var resultTitles = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultTitles;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetJobTitles", new { }, null);
            }

            return result;
        }
        public ReturnResult GetCoAbvByLicenseID(int vLicenseID)
        {
            var result = new ReturnResult();
            try
            {
                var query = from c in _db.Companies
                            join p in _db.LicenseCompanies on c.CompanyId equals p.CompanyId
                            where c.CompanyType == "InsuranceCo" && p.IsActive == true && p.LicenseId == vLicenseID
                            select new { value = c.CompanyId, label = c.CompanyAbv };

                var resultCompanies = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultCompanies;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetCoAbvByLicenseID", new { }, null);
            }

            return result;
        }
        public ReturnResult GetPreEducationByStateAbv(string vStateAbv)
        {
            var result = new ReturnResult();
            try
            {
                var query = from e in _db.PreEducations
                            where e.StateProvinceAbv == vStateAbv
                            select new { value = e.PreEducationId, label = string.Format("{0}{1}", e.EducationName, e.DeliveryMethod == "Online" ? " - Online" : null) };

                var resultPreEducations = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultPreEducations;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetPreEducationByStateAbv", new { }, null);
            }

            return result;
        }
        public ReturnResult GetPreExamByStateAbv(string vStateAbv)
        {
            var result = new ReturnResult();
            try
            {
                var query = from e in _db.Exams
                            where e.StateProvinceAbv == vStateAbv
                            select new { value = e.ExamId, label = e.ExamName };

                var resultPreEducations = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultPreEducations;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetPreExamByStateAbv", new { }, null);
            }

            return result;
        }
        public ReturnResult GetAgentStautes()
        {
            var result = new ReturnResult();
            try
            {
                var query = from s in _db.LkpTypeStatuses
                            where s.LkpField == "AgentStatus"
                            orderby s.SortOrder, s.LkpValue
                            select new { value = s.LkpValue, label = s.LkpValue };

                var resultAgentStatuses = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultAgentStatuses;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetAgentStautes", new { }, null);
            }

            return result;
        }
        public ReturnResult GetAppointmentStatuses()
        {
            var result = new ReturnResult();
            try
            {
                var query = from s in _db.LkpTypeStatuses
                            where s.LkpField == "AppointmentStatus"
                            orderby s.SortOrder, s.LkpValue
                            select new { value = s.LkpValue, label = s.LkpValue };

                var resultAppointmentStatuses = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultAppointmentStatuses;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetAppointmentStatuses", new { }, null);
            }

            return result;
        }
        public ReturnResult GetApplicationsStatuses()
        {
            var result = new ReturnResult();
            try
            {
                var query = from s in _db.LkpTypeStatuses
                            where s.LkpField == "ApplicationStatus"
                            orderby s.SortOrder, s.LkpValue
                            select new { value = s.LkpValue, label = s.LkpValue };

                var resultApplicationStatuses = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultApplicationStatuses;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetApplicationsStatuses", new { }, null);
            }

            return result;
        }
        public ReturnResult GetPreEducationStatuses()
        {
            var result = new ReturnResult();
            try
            {
                var query = from s in _db.LkpTypeStatuses
                            where s.LkpField == "PreEdStatus"
                            orderby s.SortOrder, s.LkpValue
                            select new { value = s.LkpValue, label = s.LkpValue };

                var resultPreEducationStatuses = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultPreEducationStatuses;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetPreEducationStatuses", new { }, null);
            }

            return result;
        }
        public ReturnResult GetPreExamStatuses()
        {
            var result = new ReturnResult();
            try
            {
                var query = from s in _db.LkpTypeStatuses
                            where s.LkpField == "ExamStatus"
                            orderby s.SortOrder, s.LkpValue
                            select new { value = s.LkpValue, label = s.LkpValue };

                var resultPreExamStatuses = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultPreExamStatuses;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetPreExamStatuses", new { }, null);
            }

            return result;
        }
        public ReturnResult GetRenewalMethods()
        {
            var result = new ReturnResult();
            try
            {
                var query = from s in _db.LkpTypeStatuses
                            where s.LkpField == "RenewalMethod"
                            orderby s.SortOrder, s.LkpValue
                            select new { value = s.LkpValue, label = s.LkpValue };

                var resultRenewalMethods = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultRenewalMethods;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetRenewalMethods", new { }, null);
            }

            return result;
        }
        public ReturnResult GetRollOutGroups()
        {
            var result = new ReturnResult();
            try
            {
                var query = from s in _db.LkpTypeStatuses
                            where s.LkpField == "RollOutGroup"
                            orderby s.SortOrder, s.LkpValue
                            select new { value = s.LkpValue, label = s.LkpValue };

                var resultRollOutGroups = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultRollOutGroups;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-RollOutGroups", new { }, null);
            }

            return result;
        }
        public ReturnResult GetContEduInfo(string vUsageType)
        {

            var result = new ReturnResult();
            try
            {
                var query = from erc in _db.EducationRuleCriteria
                            where erc.UsageType == vUsageType && erc.IsActive == true
                            orderby erc.EducationCriteriaId
                            select new { value = erc.EducationCriteriaId, label = erc.Description };

                var resultContEduInfo = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultContEduInfo;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-ContEduInfo", new { }, null);
            }

            return result;

        }
        public ReturnResult GetDropdownListTypes()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var query = _db.LkpTypeStatuses
                            .Select(x => x.LkpField)
                            .Distinct()
                            .OrderBy(x => x)
                            .AsNoTracking()
                            .ToList();

                result.ObjData = query;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
            }
            return result;
        }
        public ReturnResult GetDropdownByType(string vLkpField)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var query = _db.LkpTypeStatuses
                        .Where(x => vLkpField == null || x.LkpField == vLkpField)
                        .OrderBy(x => x.LkpField)
                        .ThenBy(x => x.SortOrder)
                        .ThenBy(x => x.LkpValue)
                        .Select(x => new { x.LkpField, x.LkpValue, x.SortOrder })
                        .AsNoTracking()
                        .ToList();

                result.ObjData = query;
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
            }
            return result;
        }
        public ReturnResult GetExamProviders()
        {
            var result = new ReturnResult();
            try
            {
                var query = from c in _db.Companies
                            where c.CompanyType == "ExamProvider" && c.XxxIsActive == true
                            orderby c.CompanyName
                            select new { value = c.CompanyId, label = c.CompanyName };

                var resultExamProviders = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultExamProviders;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetExamProviders", new { }, null);
            }

            return result;
        }
        public ReturnResult GetLicenseLineOfAuthority()
        {
            var result = new ReturnResult();
            try
            {
                var query = from l in _db.LineOfAuthorities
                            orderby l.LineOfAuthorityName
                            select new { value = l.LineOfAuthorityId, label = l.LineOfAuthorityName };

                var resultLineOfAuthorities = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultLineOfAuthorities;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetLicenseLineOfAuthority", new { }, null);
            }

            return result;
        }
        public ReturnResult GetDocumentTypes()
        {
            var result = new ReturnResult();
            try
            {
                var query = _db.Communications
                               .AsNoTracking()
                               .Where(c => c.IsActive == true)
                               .Select(c => c.DocType)
                               .Distinct()
                               .ToList();

                result.Success = true;
                result.ObjData = query;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetDocumentTypes", new { }, null);
            }

            return result;
        }
        public ReturnResult GetDocumentSubTypes(string vType)
        {
            var result = new ReturnResult();
            try
            {
                var query = _db.Communications
                               .AsNoTracking()
                               .Where(c => c.IsActive == true && c.DocType == vType)
                               .Select(c => c.DocSubType)
                               .Distinct()
                               .ToList();

                result.Success = true;
                result.ObjData = query;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "MISC-GetDocumentSubTypes", new { }, null);
            }

            return result;
        }
    }
}
