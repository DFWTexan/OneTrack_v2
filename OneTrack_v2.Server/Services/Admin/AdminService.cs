﻿using DataModel.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DbData;
using OneTrack_v2.Services;
using OneTrak_v2.DataModel;

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
                var resultExams = from e in _db.Exams
                                 join c in _db.Companies on e.ExamProviderId equals c.CompanyId into ec
                                 from subCompany in ec.DefaultIfEmpty()
                                 where e.StateProvinceAbv == vState
                                  select new
                                 {
                                     e.ExamId,
                                     e.ExamName,
                                     e.ExamFees,
                                     e.StateProvinceAbv,
                                     e.ExamProviderId,
                                     CompanyName = subCompany != null ? subCompany.CompanyName : null,
                                     e.DeliveryMethod
                                 };

                result.ObjData = resultExams;
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
                var resultLicLevels = new List<(string LicenseLevel, int SortOrder)>
                                        {
                                            ("{NeedsReview}", 0),
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
                result.ErrMessage = ex.Message;
            }
            return result;
        }

        public ReturnResult GetJobTitlelicIncentive()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var resultIncentives = new List<(string LicenseIncentive, int SortOrder)>
                                {
                                    ("NoIncentive", 0),
                                    ("PLS_Incentive1", 1),
                                    ("Incentive2_Plus", 2),
                                    ("LicIncentive3", 3)
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
                result.ErrMessage = ex.Message;
            }
            return result;
        }

        public ReturnResult GetJobTitles()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var resultJobTitles = from jt in _db.JobTitles
                                     orderby jt.IsActive descending, jt.LicenseLevel, jt.JobTitle1
                                     select new
                                     {
                                         jt.JobTitleId,
                                         jt.JobTitle1,
                                         jt.JobCode,
                                         jt.CreatedDate,
                                         jt.Reviewed,
                                         jt.IsActive,
                                         jt.LicenseLevel,
                                         jt.LicenseIncentive,
                                         isDirty = false
                                     };

                result.ObjData = resultJobTitles;
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

        public ReturnResult GetLicenseByStateProv(string vStateProv)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var query = from l in _db.Licenses
                                     join a in _db.LineOfAuthorities on l.LineOfAuthorityId equals a.LineOfAuthorityId
                                     where string.IsNullOrEmpty(vStateProv) || l.StateProvinceAbv == vStateProv
                                     orderby l.LicenseName
                                     select new OputLicenseInfo
                                     {
                                         LicenseId = l.LicenseId,
                                         LicenseName = l.LicenseName,
                                         LicenseAbv = l.LicenseAbv,
                                         StateProvinceAbv = l.StateProvinceAbv,
                                         LineOfAuthorityAbv = a.LineOfAuthorityAbv,
                                         LineOfAuthorityId = a.LineOfAuthorityId,
                                         AgentStateTable = a.AgentStateTable,
                                         PlsIncentive1Tmpay = l.PlsIncentive1Tmpay,
                                         PlsIncentive1Mrpay = l.PlsIncentive1Mrpay,
                                         Incentive2PlusTmpay = l.Incentive2PlusTmpay,
                                         Incentive2PlusMrpay = l.Incentive2PlusMrpay,
                                         LicIncentive3Tmpay = l.LicIncentive3Tmpay,
                                         LicIncentive3Mrpay = l.LicIncentive3Mrpay,
                                         IsActive = l.IsActive,
                                     };

                var licenseInfo = query.AsNoTracking().ToList();

                foreach (var item in licenseInfo)
                {
                    item.CompanyItems = GetLicenseCompanyItems(item.LicenseId);
                    item.PreExamItems = GetLicensePreExamItems(item.LicenseId);
                    item.PreEducationItems = GetLicensePreEduItems(item.LicenseId);
                    item.ProductItems = GetLicenseProductItems(item.LicenseId);
                }

                result.ObjData = licenseInfo;
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
        private List<LicenseCompanyItem> GetLicenseCompanyItems(int vLicenseID)
        {
            var query = from cp in _db.LicenseCompanies
                        join c in _db.Companies on cp.CompanyId equals c.CompanyId
                        join a in _db.Addresses on c.AddressId equals a.AddressId
                        where cp.LicenseId == vLicenseID
                        orderby c.CompanyName
                        select new LicenseCompanyItem
                        {
                            LicenseCompanyId = cp.LicenseCompanyId,
                            CompanyId = c.CompanyId,
                            CompanyAbv = c.CompanyAbv,
                            CompanyType = c.CompanyType,
                            CompanyName = c.CompanyName,
                            TIN = c.Tin.ToString(),
                            NAICNumber = c.Naicnumber.ToString(),
                            IsActive = cp.IsActive,
                            AddressId = a.AddressId,
                            Address1 = a.Address1,
                            Address2 = a.Address2,
                            City = a.City,
                            State = a.State,
                            Zip = a.Zip,
                            Phone = a.Phone,
                            Fax = a.Fax,
                            Country = a.Country
                        };

            return query.ToList();
        }
        private List<LicensePreExamItem> GetLicensePreExamItems(int vLicenseID)
        {
            var query = from le in _db.LicenseExams
                        join e in _db.Exams on le.ExamId equals e.ExamId
                        join c in _db.Companies on e.ExamProviderId equals c.CompanyId into ps
                        from p in ps.DefaultIfEmpty()
                        where le.LicenseId == vLicenseID
                        orderby e.ExamName
                        select new LicensePreExamItem
                        {
                            ExamId = e.ExamId,
                            ExamName = e.ExamName,
                            StateProvinceAbv = e.StateProvinceAbv,
                            CompanyName = p.CompanyName,
                            DeliveryMethod = e.DeliveryMethod,
                            LicenseExamID = le.LicenseExamId,
                            ExamProviderID = e.ExamProviderId.HasValue ? (int)e.ExamProviderId : 0,
                            IsActive = le.IsActive
                        };

            return query.ToList();
        }
        private List<LicensePreEducationItem> GetLicensePreEduItems(int vLicenseID)
        {
            var query = from le in _db.LicensePreEducations
                        join e in _db.PreEducations on le.PreEducationId equals e.PreEducationId
                        join c in _db.Companies on e.EducationProviderId equals c.CompanyId into ps
                        from p in ps.DefaultIfEmpty()
                        where le.LicenseId == vLicenseID
                        orderby e.EducationName
                        select new LicensePreEducationItem
                        {
                            LicensePreEducationID = le.LicensePreEducationId,
                            PreEducationID = e.PreEducationId,
                            EducationName = e.EducationName,
                            StateProvinceAbv = e.StateProvinceAbv,
                            CreditHours = (short)(e.CreditHours.HasValue ? (double)e.CreditHours.Value : 0.00),
                            CompanyID = p.CompanyId,
                            CompanyName = p.CompanyName,
                            DeliveryMethod = e.DeliveryMethod,
                            IsActive = le.IsActive
                        };

            return query.ToList();
        }
        private List<LicenseProductItem> GetLicenseProductItems(int vLicenseID)
        {
            var query = from le in _db.LicenseProducts
                        join e in _db.Products on le.ProductId equals e.ProductId
                        where le.LicenseId == vLicenseID
                        orderby e.ProductName
                        select new LicenseProductItem
                        {
                            LicenseProductID = le.LicenseProductId,
                            ProductID = e.ProductId,
                            ProductName = e.ProductName,
                            ProductAbv = e.ProductAbv,
                            IsActive = le.IsActive
                        };

            return query.ToList();
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
                var query = from l in _db.LicenseTeches
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
