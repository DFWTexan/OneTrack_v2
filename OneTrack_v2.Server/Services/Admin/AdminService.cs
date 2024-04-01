using DataModel.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DbData;
using OneTrack_v2.Services;

namespace OneTrak_v2.Services
{
    public class AdminService : IAdminService
    {
        private readonly IConfiguration _config;
        private readonly AppDataContext _db;
        private readonly IUtilityHelpService _utilityService;
        private readonly string? _connectionString;

        public AdminService(AppDataContext db, IConfiguration config, IUtilityHelpService utilityHelpService)
        {
            _db = db;
            _config = config;
            _connectionString = _config.GetConnectionString(name: "DefaultConnection");
            _utilityService = utilityHelpService;
        }

        public ReturnResult GetCompanyTypes()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var resCompanyTypes = _db.LkpTypeStatuses
                                    .Where(x => x.LkpField == "CompanyType")
                                    .Select(x => x.LkpValue)
                                    //.Union(new List<string> { "CompanyType" })
                                    .OrderBy(x => x)
                                    .AsNoTracking()
                                    .ToList();

                result.ObjData = resCompanyTypes;
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
        
        public ReturnResult GetCompaniesByType(string? vCompanyType = null)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var resCompanies = (from c in _db.Companies
                                  join a in _db.Addresses on c.AddressId equals a.AddressId into ca
                                  from a in ca.DefaultIfEmpty()
                                  where vCompanyType == null || c.CompanyType == vCompanyType
                                  group new { c, a } by new
                                  {
                                      c.CompanyId,
                                      c.CompanyAbv,
                                      c.CompanyType,
                                      c.CompanyName,
                                      c.Tin,
                                      c.Naicnumber,
                                      a.AddressId,
                                      a.Address1,
                                      a.Address2,
                                      a.City,
                                      a.State,
                                      a.Phone,
                                      a.Country,
                                      a.Zip,
                                      a.Fax
                                  } into g
                                  orderby g.Key.CompanyName
                                  select g.Key).AsNoTracking().ToList();

                result.ObjData = resCompanies;
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

        public ReturnResult GetLicenseTypes(string? vStateAbv = null)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var query = from l in _db.Licenses
                            where vStateAbv == null || l.StateProvinceAbv == vStateAbv
                            select l.LicenseName;

                var resLicenses = query.Distinct().AsNoTracking().ToList();

                result.ObjData = resLicenses;
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


        public ReturnResult GetConEducationRules(string? vStateAbv = null, string? vLicenseType = null)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var sql = @"SELECT 
                                ER.RuleNumber, 
                                ER.StateProvince, 
                                ER.LicenseType, 
                                ER.RequiredCreditHours, 
                                ER.EducationStartDateID,
                                ERC1.Description AS EducationStartDate,
                                ER.EducationEndDateID, 
                                ERC2.Description AS EducationEndDate,
                                ER.ExceptionID, 
                                ER.ExemptionID,
                                ER.IsActive
                            FROM dbo.EducationRule ER 
                            LEFT OUTER JOIN dbo.EducationRuleCriteria ERC1 ON ERC1.EducationCriteriaID = ER.EducationStartDateID
                                AND ERC1.UsageType = 'EducationStartDate'
                                AND ERC1.IsActive = 1
                            LEFT OUTER JOIN dbo.EducationRuleCriteria ERC2 ON ERC2.EducationCriteriaID = ER.EducationEndDateID
                                AND ERC2.UsageType = 'EducationEndDate'
                                AND ERC2.IsActive = 1
                            WHERE (@StateProvince IS NULL OR ER.StateProvince = @StateProvince)
                                AND (@LicenseType IS NULL OR ER.LicenseType LIKE '%' + @LicenseType + '%')
                                AND ER.IsActive = 1 ";

                var parameters = new[]
                            {
                                new SqlParameter("@StateProvince", vStateAbv ?? (object)DBNull.Value),
                                new SqlParameter("@LicenseType", vLicenseType ?? (object)DBNull.Value)
                            };

                var queryEduRulesResults = _db.OputEducationRules
                                            .FromSqlRaw(sql, parameters)
                                            .AsNoTracking()
                                            .ToList();

                result.ObjData = queryEduRulesResults;
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

        public ReturnResult GetCompanyRequirements(string vWorkState, string? vResState = null)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var sql = @"
                       SELECT  
                            R.CompanyRequirementID,
                            R.WorkStateAbv, 
                            R.ResStateAbv, 
                            R.RequirementType, 
                            R.[LicLevel1],
                            R.[LicLevel2],
                            R.[LicLevel3],
                            R.[LicLevel4],
                            R.StartAfterDate,
                            R.[Document]
                        FROM 
                            dbo.CompanyRequirements R
                        WHERE
                            (@vWorkState IS NULL OR R.WorkStateAbv = @vWorkState) AND 
                            (@vResState IS NULL OR R.ResStateAbv = @vResState) ";

                var parameters = new[]
                            {
                                new SqlParameter("@vWorkState", vWorkState),
                                new SqlParameter("@vResState", vResState ?? (object)DBNull.Value)
                            };

                var queryCoRequirementsResults = _db.OputCompanyRequirements
                                            .FromSqlRaw(sql, parameters)
                                            .AsNoTracking()
                                            .OrderBy(r => r.WorkStateAbv)
                                            .ThenBy(r => r.ResStateAbv)
                                            .ToList();

                result.ObjData = queryCoRequirementsResults;
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

        public ReturnResult GetExamByState(string vState)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.Exams.Where(x => x.State == vState).ToList();
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

        public ReturnResult GetJobTitleLicLevel()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.JobTitleLicLevels.ToList();
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

        public ReturnResult GetJobTitlelicIncentive()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.JobTitleLicIncentives.ToList();
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

        public ReturnResult GetJobTitlelicensed()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.JobTitleLicenseds.ToList();
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

        public ReturnResult GetLicenseEditList()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.LicenseEditLists.ToList();
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

        public ReturnResult GetLicenseEditByID(int vLicenseID)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.LicenseEditLists.ToList();
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

        public ReturnResult GetLicTechList()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.LicTechLists.ToList();
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

        public ReturnResult GetPreEduEditByState(string vState)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.PreEduEdits.Where(x => x.State == vState).ToList();
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

        public ReturnResult GetProductEditList()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.ProductEditLists.ToList();
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

        public ReturnResult GetStateLicRequirementList(string vWorkState, string vResState)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.StateLicRequirements.Where(x => x.WorkState == vWorkState && x.ResState == vResState).ToList();
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

        public ReturnResult GetStateProvinceList()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.StateProvinces.ToList();
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

        public ReturnResult GetXBorderBranchList()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.XBorderBranches.ToList();
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

        public ReturnResult GetXBorderBranchByCode(int vBranchCode)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.XBorderBranches.Where(x => x.BranchCode == vBranchCode).ToList();
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
    }
}
