using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DbData;
using OneTrack_v2.DbData.Models;
using OneTrack_v2.Services;
using OneTrak_v2.DataModel;
using OneTrak_v2.Server.DbData.DataModel.Admin;
using System.Data;

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

        public ReturnResult GetLicenseByStateProv(string? vStateProv = null)
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
                            LicenseID = vLicenseID,
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
                            LicenseID = vLicenseID,
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
                            LicenseID = vLicenseID,
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
                            LicenseID = vLicenseID,
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

        public ReturnResult GetPreEduEditByState(string vStateProvinceAbv)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var query = from p in _db.PreEducations
                            join c in _db.Companies on p.EducationProviderId equals c.CompanyId into pc
                            from subCompany in pc.DefaultIfEmpty()
                            where p.StateProvinceAbv == vStateProvinceAbv
                            select new
                            {
                                p.PreEducationId,
                                p.EducationName,
                                p.StateProvinceAbv,
                                CompanyID = subCompany != null ? subCompany.CompanyId : 0,
                                CompanyName = subCompany != null ? subCompany.CompanyName : null,
                                p.CreditHours,
                                p.DeliveryMethod
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

        public ReturnResult GetProductEdits()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var query = from le in _db.LicenseProducts
                            join e in _db.Products on le.ProductId equals e.ProductId
                            group new { le, e } by new { le.IsActive, le.ProductId, e.ProductName, e.ProductAbv, e.EffectiveDate, e.ExpireDate } into g
                            orderby g.Key.ProductName
                            select new
                            {
                                g.Key.ProductId,
                                g.Key.ProductName,
                                g.Key.ProductAbv,
                                g.Key.IsActive,
                                g.Key.EffectiveDate,
                                g.Key.ExpireDate
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

        public ReturnResult GetStateLicRequirements(string? vWorkStateAbv = null, string? vResStateAbv = null, string? vBranchCode = null)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var query = from r in _db.RequiredLicenses
                            join l in _db.Licenses on r.LicenseId equals l.LicenseId
                            where (vWorkStateAbv == null || r.WorkStateAbv == vWorkStateAbv) &&
                                  (vResStateAbv == null || r.ResStateAbv == vResStateAbv) &&
                                  (vBranchCode == null || r.BranchCode == vBranchCode)
                            orderby r.WorkStateAbv, r.ResStateAbv, l.StateProvinceAbv
                            select new
                            {
                                r.RequiredLicenseId,
                                r.WorkStateAbv,
                                r.ResStateAbv,
                                r.LicenseId,
                                r.BranchCode,
                                r.RequirementType,
                                LicLevel1 = r.LicLevel1,
                                LicLevel2 = r.LicLevel2,
                                LicLevel3 = r.LicLevel3,
                                LicLevel4 = r.LicLevel4,
                                PLS_Incentive1 = r.PlsIncentive1,
                                Incentive2_Plus = r.Incentive2Plus,
                                LicIncentive3 = r.LicIncentive3,
                                LicState = l.StateProvinceAbv,
                                l.LicenseName,
                                StartDocument = r.StartDocument,
                                RenewalDocument = r.RenewalDocument
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

        public ReturnResult GetStateProvinceList()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var sql = @"SELECT 
	                            S.StateProvinceCode, 
                                S.StateProvinceName, 
	                            S.Country, 
                                S.StateProvinceAbv, 
	                            S.DOIAddressID, 
                                ISNULL(S.LicenseTechID, 0) as LicenseTechID, 
	                            S.IsActive, 
                                S.DOIName, 
                                T.TeamNum, 
                                T.FirstName + ' ' + T.LastName AS TechName,
	                            A.AddressType, 
                                A.Address1, 
	                            A.Address2, 
                                A.City, 
	                            A.State, 
                                A.Phone, 
	                            A.Zip, 
                                A.Fax
                            FROM dbo.StateProvince S 
                            LEFT OUTER JOIN dbo.LicenseTech T ON S.LicenseTechID = T.LicenseTechID
                            LEFT OUTER JOIN dbo.Address A ON S.DOIAddressID = A.AddressID";

                var queryStateProvResults = _db.OputStateProvince
                                            .FromSqlRaw(sql)
                                            .AsNoTracking()
                                            .OrderBy(r => r.StateProvinceAbv)
                                            .ToList();

                result.ObjData = queryStateProvResults;
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

        public ReturnResult GetXBorderBranchCodes()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var query = (from r in _db.RequiredLicenses
                             where r.RequirementType != "Regular"
                             select r.BranchCode).Distinct();

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

        public ReturnResult GetXBorLicRequirements(string vBranchCode)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                var query = from r in _db.RequiredLicenses
                            join l in _db.Licenses on r.LicenseId equals l.LicenseId
                            where (r.BranchCode == vBranchCode)
                            orderby r.WorkStateAbv, r.ResStateAbv, l.StateProvinceAbv
                            select new
                            {
                                r.RequiredLicenseId,
                                r.WorkStateAbv,
                                r.ResStateAbv,
                                r.LicenseId,
                                r.BranchCode,
                                r.RequirementType,
                                LicLevel1 = r.LicLevel1,
                                LicLevel2 = r.LicLevel2,
                                LicLevel3 = r.LicLevel3,
                                LicLevel4 = r.LicLevel4,
                                PLS_Incentive1 = r.PlsIncentive1,
                                Incentive2_Plus = r.Incentive2Plus,
                                LicIncentive3 = r.LicIncentive3,
                                LicState = l.StateProvinceAbv,
                                l.LicenseName,
                                StartDocument = r.StartDocument,
                                RenewalDocument = r.RenewalDocument
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

        #region INSERT/UPDATE/DELETE
        public ReturnResult UpsertCompany([FromBody] IputUpsertCompany vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.CompanyId == 0)
                {
                    // INSERT Company
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspCompanyInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@CompanyID", vInput.CompanyId));
                            cmd.Parameters.Add(new SqlParameter("@CompanyAbv", vInput.CompanyAbv));
                            cmd.Parameters.Add(new SqlParameter("@CompanyType", vInput.CompanyType));
                            cmd.Parameters.Add(new SqlParameter("@CompanyName", vInput.CompanyName));
                            cmd.Parameters.Add(new SqlParameter("@TIN", vInput.Tin));
                            cmd.Parameters.Add(new SqlParameter("@NAICNumber", vInput.Naicnumber));
                            cmd.Parameters.Add(new SqlParameter("@AddressID", vInput.AddressId));
                            cmd.Parameters.Add(new SqlParameter("@Address1", vInput.Address1));
                            cmd.Parameters.Add(new SqlParameter("@Address2", vInput.Address2));
                            cmd.Parameters.Add(new SqlParameter("@City", vInput.City));
                            cmd.Parameters.Add(new SqlParameter("@State", vInput.State));
                            cmd.Parameters.Add(new SqlParameter("@Phone", vInput.Phone));
                            cmd.Parameters.Add(new SqlParameter("@Zip", vInput.Zip));
                            cmd.Parameters.Add(new SqlParameter("@Fax", vInput.Fax));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE Company
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspCompanyUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@CompanyID", vInput.CompanyId));
                            cmd.Parameters.Add(new SqlParameter("@CompanyAbv", vInput.CompanyAbv));
                            cmd.Parameters.Add(new SqlParameter("@CompanyType", vInput.CompanyType));
                            cmd.Parameters.Add(new SqlParameter("@CompanyName", vInput.CompanyName));
                            cmd.Parameters.Add(new SqlParameter("@TIN", vInput.Tin));
                            cmd.Parameters.Add(new SqlParameter("@NAICNumber", vInput.Naicnumber));
                            cmd.Parameters.Add(new SqlParameter("@AddressID", vInput.AddressId));
                            cmd.Parameters.Add(new SqlParameter("@Address1", vInput.Address1));
                            cmd.Parameters.Add(new SqlParameter("@Address2", vInput.Address2));
                            cmd.Parameters.Add(new SqlParameter("@City", vInput.City));
                            cmd.Parameters.Add(new SqlParameter("@State", vInput.State));
                            cmd.Parameters.Add(new SqlParameter("@Phone", vInput.Phone));
                            cmd.Parameters.Add(new SqlParameter("@Zip", vInput.Zip));
                            cmd.Parameters.Add(new SqlParameter("@Fax", vInput.Fax));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Company Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1509-49597].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteCompany([FromBody] IputDeleteCompany vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspCompanyDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@CompanyID", vInput.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@AddressID", vInput.AddressID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Company Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1509-59517].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertEducationRule([FromBody] IputUpsertEducationRule vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.RuleNumber == 0)
                {
                    // INSERT Education Rule
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspEducationRuleInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@StateProvince", vInput.StateProvince));
                            cmd.Parameters.Add(new SqlParameter("@LicenseType", vInput.LicenseType));
                            cmd.Parameters.Add(new SqlParameter("@EducationStartDateID", vInput.EducationStartDateID));
                            cmd.Parameters.Add(new SqlParameter("@EducationEndDateID", vInput.EducationEndDateID));
                            cmd.Parameters.Add(new SqlParameter("@RequiredCreditHours", vInput.RequiredCreditHours));
                            cmd.Parameters.Add(new SqlParameter("@ExceptionID", vInput.ExceptionID));
                            cmd.Parameters.Add(new SqlParameter("@ExemptionID", vInput.ExemptionID));
                            //cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE Education Rule
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspEducationRuleUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@RuleNumber", vInput.RuleNumber));
                            cmd.Parameters.Add(new SqlParameter("@StateProvince", vInput.StateProvince));
                            cmd.Parameters.Add(new SqlParameter("@LicenseType", vInput.LicenseType));
                            cmd.Parameters.Add(new SqlParameter("@EducationStartDateID", vInput.EducationStartDateID));
                            cmd.Parameters.Add(new SqlParameter("@EducationEndDateID", vInput.EducationEndDateID));
                            cmd.Parameters.Add(new SqlParameter("@RequiredCreditHours", vInput.RequiredCreditHours));
                            cmd.Parameters.Add(new SqlParameter("@ExceptionID", vInput.ExceptionID));
                            cmd.Parameters.Add(new SqlParameter("@ExemptionID", vInput.ExemptionID));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Education Rule Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1509-49597].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DisableEducationRule([FromBody] IputDisableEducationRule vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspEducationRuleDisable", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@RuleNumber", vInput.RuleNumber));
                        cmd.Parameters.Add(new SqlParameter("@StateProvince", vInput.StateProvince));
                        cmd.Parameters.Add(new SqlParameter("@GEID", vInput.GEID));
                        cmd.Parameters.Add(new SqlParameter("@UserName", vInput.UserName));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Education Rule Disabled Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1509-49597].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }
        public ReturnResult UpsertLkpType([FromBody] IputUpsertLkpType vInput)
        {

            var result = new ReturnResult();
            try
            {
                if (vInput.LkpField == null || vInput.LkpValue == null)
                {
                    result.Success = false;
                    result.ObjData = null;
                    result.ErrMessage = "Invalid Input - Please provide LkpField and LkpValue.";

                    return result;
                }

                if (vInput.UpsertType == "INSERT")
                {
                    // INSERT LkpType
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("usplkp_TypeStatusInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@LkpField", vInput.LkpField));
                            cmd.Parameters.Add(new SqlParameter("@LkpValue", vInput.LkpValue));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE LkpType
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("usplkp_TypeStatusUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@LkpField", vInput.LkpField));
                            cmd.Parameters.Add(new SqlParameter("@LkpValue", vInput.LkpValue));
                            cmd.Parameters.Add(new SqlParameter("@SortOrder", vInput.SortOrder));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "LkpType Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1509-49577].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteLkpType([FromBody] IputDeleteLkpType vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usplkp_TypeStatusDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LkpField", vInput.LkpField));
                        cmd.Parameters.Add(new SqlParameter("@LkpValue", vInput.LkpValue));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "LkpType Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1509-9590].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertExam([FromBody] IputUpsertExam vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.ExamID == 0)
                {
                    // INSERT Exam
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspExamInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ExamName", vInput.ExamName));
                            cmd.Parameters.Add(new SqlParameter("@ExamFees", vInput.ExamFees));
                            cmd.Parameters.Add(new SqlParameter("@StateProvinceAbv", vInput.StateProvinceAbv));
                            cmd.Parameters.Add(new SqlParameter("@ExamProviderID", vInput.ExamProviderID));
                            cmd.Parameters.Add(new SqlParameter("@DeliveryMethod", vInput.DeliveryMethod));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE Exam
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspExamUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ExamID", vInput.ExamID));
                            cmd.Parameters.Add(new SqlParameter("@ExamName", vInput.ExamName));
                            cmd.Parameters.Add(new SqlParameter("@ExamFees", vInput.ExamFees));
                            cmd.Parameters.Add(new SqlParameter("@StateProvinceAbv", vInput.StateProvinceAbv));
                            cmd.Parameters.Add(new SqlParameter("@ExamProviderID", vInput.ExamProviderID));
                            cmd.Parameters.Add(new SqlParameter("@DeliveryMethod", vInput.DeliveryMethod));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Exam Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1509-49397].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
            #endregion
        }
        public ReturnResult DeleteExam([FromBody] IputDeleteExam vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspExamDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@ExamID", vInput.ExamID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Exam Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1509-49198].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertJobTitle([FromBody] IputUpsertJobTitle vInput)
        {

            var result = new ReturnResult();
            try
            {
                if (vInput.JobTitleID == 0)
                {
                    // INSERT JobTitle
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspJobTitleInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@JobTitle", vInput.JobTitle));
                            cmd.Parameters.Add(new SqlParameter("@LicenseLevel", vInput.LicenseLevel));
                            cmd.Parameters.Add(new SqlParameter("@LicenseIncentive", vInput.LicenseIncentive));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE JobTitle
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspJobTitleUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@JobTitleID", vInput.JobTitleID));
                            cmd.Parameters.Add(new SqlParameter("@LicenseLevel", vInput.LicenseLevel));
                            cmd.Parameters.Add(new SqlParameter("@LicenseIncentive", vInput.LicenseIncentive));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "JobTitle Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1509-41197].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertLicense([FromBody] IputUpsertLicense vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.LicenseID == 0)
                {
                    // INSERT License
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspLicenseInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@LicenseName", vInput.LicenseName));
                            cmd.Parameters.Add(new SqlParameter("@LicenseAbv", vInput.LicenseAbv));
                            cmd.Parameters.Add(new SqlParameter("@StateProvinceAbv", vInput.StateProvinceAbv));
                            cmd.Parameters.Add(new SqlParameter("@LineOfAuthorityID", vInput.LineOfAuthorityID));
                            cmd.Parameters.Add(new SqlParameter("@PLS_Incentive1TMPay", vInput.PLS_Incentive1TMPay));
                            cmd.Parameters.Add(new SqlParameter("@PLS_Incentive1MRPay", vInput.PLS_Incentive1MRPay));
                            cmd.Parameters.Add(new SqlParameter("@Incentive2_PlusTMPay", vInput.Incentive2_PlusTMPay));
                            cmd.Parameters.Add(new SqlParameter("@Incentive2_PlusMRPay", vInput.Incentive2_PlusMRPay));
                            cmd.Parameters.Add(new SqlParameter("@LicIncentive3Tmpay", vInput.LicIncentive3TMPay));
                            cmd.Parameters.Add(new SqlParameter("@LicIncentive3Mrpay", vInput.LicIncentive3TMPay));
                            cmd.Parameters.Add(new SqlParameter("@IsActive", vInput.IsActive));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE License
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspLicenseUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID));
                            cmd.Parameters.Add(new SqlParameter("@LicenseName", vInput.LicenseName));
                            cmd.Parameters.Add(new SqlParameter("@LicenseAbv", vInput.LicenseAbv));
                            cmd.Parameters.Add(new SqlParameter("@StateProvinceAbv", vInput.StateProvinceAbv));
                            cmd.Parameters.Add(new SqlParameter("@LineOfAuthorityID", vInput.LineOfAuthorityID));
                            cmd.Parameters.Add(new SqlParameter("@PLS_Incentive1TMPay", vInput.PLS_Incentive1TMPay));
                            cmd.Parameters.Add(new SqlParameter("@PLS_Incentive1MRPay", vInput.PLS_Incentive1MRPay));
                            cmd.Parameters.Add(new SqlParameter("@Incentive2_PlusTMPay", vInput.Incentive2_PlusTMPay));
                            cmd.Parameters.Add(new SqlParameter("@Incentive2_PlusMRPay", vInput.Incentive2_PlusMRPay));
                            cmd.Parameters.Add(new SqlParameter("@LicIncentive3Tmpay", vInput.LicIncentive3TMPay));
                            cmd.Parameters.Add(new SqlParameter("@LicIncentive3Mrpay", vInput.LicIncentive3TMPay));
                            cmd.Parameters.Add(new SqlParameter("@IsActive", vInput.IsActive));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();

                        }
                    }
                }
                result.Success = true;
                result.ObjData = new { Message = "JobTitle Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1509-41148].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteLicense([FromBody] IputDeleteLicense vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspLicenseDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "License Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1509-44190].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult AddLicenseCompany([FromBody] IputAddLicenseCompany vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspLicenseCompanyInsert", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID));
                        cmd.Parameters.Add(new SqlParameter("@CompanyID", vInput.CompanyID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Company Added to License Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1539-79597].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;

        }
        public ReturnResult UpdateLicenseCompany([FromBody] IputUpdateLicenseCompany vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspLicenseCompanyUpdate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicenseCompanyID", vInput.LicenseCompanyID));
                        cmd.Parameters.Add(new SqlParameter("@IsActive", vInput.IsActive));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Company Updated to License Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1539-72591].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteLicenseCompany([FromBody] IputDeleteLicenseCompany vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspLicenseCompanyDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicenseCompanyID", vInput.LicenseCompanyID));
                        cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Company Deleted from License Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-2539-72501].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult AddLicenseExam([FromBody] IputAddLicenseExam vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspAddLicenseExamInsert", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID));
                        cmd.Parameters.Add(new SqlParameter("@ExamID", vInput.ExamID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Exam Added to License Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1539-29535].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpdateLicenseExam([FromBody] IputUpdateLicenseExam vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspLicenseExamUpdate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicenseExamID", vInput.LicenseExamID));
                        cmd.Parameters.Add(new SqlParameter("@IsActive", vInput.IsActive));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Exam Updated to License Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1539-72591].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteLicenseExam([FromBody] IputDeleteLicenseExam vInput)
        { 
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspLicenseExamDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicenseExamID", vInput.LicenseExamID));
                        cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Exam Deleted from License Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-2539-72501].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult AddLicensePreEducation([FromBody] IputAddLicensePreEducation vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspAddLicensePreEducationInsert", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID));
                        cmd.Parameters.Add(new SqlParameter("@PreEducationID", vInput.PreEducationID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "PreEducation Added to License Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1539-21574].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpdateLicensePreEducation([FromBody] IputUpdateLicensePreEducation vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspLicensePreEducationUpdate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicensePreEducationID", vInput.LicensePreEducationID));
                        cmd.Parameters.Add(new SqlParameter("@IsActive", vInput.IsActive));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "PreEducation Updated to License Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1539-21574].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteLicensePreEducation([FromBody] IputDeleteLicensePreEdu vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspLicensePreEducationDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicensePreEducationID", vInput.LicensePreEducationID));
                        cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "PreEducation Deleted from License Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-2539-21501].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult AddLicenseProduct([FromBody] IputAddLicenseProduct vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspAddLicenseProductInsert", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID));
                        cmd.Parameters.Add(new SqlParameter("@ProductID", vInput.ProductID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Product Added to License Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1539-21574].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpdateLicenseProduct([FromBody] IputUpdateLicenseProduct vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspLicenseProductUpdate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicenseProductID", vInput.LicenseProductID));
                        cmd.Parameters.Add(new SqlParameter("@IsActive", vInput.IsActive));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Product Updated to License Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1539-21574].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteLicenseProduct([FromBody] IputDeleteLicenseProduct vInput)
        { 
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspLicenseProductDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicenseProductID", vInput.LicenseProductID));
                        cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Product Deleted from License Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-2539-21501].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertLicenseTech([FromBody] IputUpsertLicenseTech vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.LicenseTechID == 0)
                {
                    // INSERT LicenseTech
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspLicenseTechInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@SOEID", vInput.TeamNum));
                            cmd.Parameters.Add(new SqlParameter("@FirstName", vInput.FirstName));
                            cmd.Parameters.Add(new SqlParameter("@LastName", vInput.LastName));
                            cmd.Parameters.Add(new SqlParameter("@TeamNum", vInput.TeamNum));
                            cmd.Parameters.Add(new SqlParameter("@LicenseTechPhone", vInput.LicenseTechPhone));
                            cmd.Parameters.Add(new SqlParameter("@LicenseTechFax", vInput.LicenseTechFax));
                            cmd.Parameters.Add(new SqlParameter("@LicenseTechEmail", vInput.LicenseTechEmail));
                            cmd.Parameters.Add(new SqlParameter("@IsActive", vInput.IsActive));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE LicenseTech
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspLicenseTechUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@LicenseTechID", vInput.LicenseTechID));
                            cmd.Parameters.Add(new SqlParameter("@SOEID", vInput.TeamNum));
                            cmd.Parameters.Add(new SqlParameter("@FirstName", vInput.FirstName));
                            cmd.Parameters.Add(new SqlParameter("@LastName", vInput.LastName));
                            cmd.Parameters.Add(new SqlParameter("@TeamNum", vInput.TeamNum));
                            cmd.Parameters.Add(new SqlParameter("@LicenseTechPhone", vInput.LicenseTechPhone));
                            cmd.Parameters.Add(new SqlParameter("@LicenseTechFax", vInput.LicenseTechFax));
                            cmd.Parameters.Add(new SqlParameter("@LicenseTechEmail", vInput.LicenseTechEmail));
                            cmd.Parameters.Add(new SqlParameter("@IsActive", vInput.IsActive));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "LicenseTech Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1539-21574].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteLicenseTech([FromBody] IputDeleteLicenseTech vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspLicenseTechDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@LicenseTechID", vInput.LicenseTechID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "LicenseTech Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1579-25574].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertPreEducation([FromBody] IputUpsertPreEducation vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.PreEducationID == 0)
                {
                    // INSERT PreEducation
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspPreEducationInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@EducationName", vInput.EducationName));
                            cmd.Parameters.Add(new SqlParameter("@CreditHours", vInput.CreditHours));
                            cmd.Parameters.Add(new SqlParameter("@DeliveryMethod", vInput.DeliveryMethod));
                            cmd.Parameters.Add(new SqlParameter("@Fees", vInput.Fees));
                            cmd.Parameters.Add(new SqlParameter("@EducationProviderID", vInput.EducationProviderID));
                            cmd.Parameters.Add(new SqlParameter("@StateProvinceAbv", vInput.StateProvinceAbv));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE PreEducation
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspPreEducationUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@PreEducationID", vInput.PreEducationID));
                            cmd.Parameters.Add(new SqlParameter("@EducationName", vInput.EducationName));
                            cmd.Parameters.Add(new SqlParameter("@CreditHours", vInput.CreditHours));
                            cmd.Parameters.Add(new SqlParameter("@DeliveryMethod", vInput.DeliveryMethod));
                            cmd.Parameters.Add(new SqlParameter("@Fees", vInput.Fees));
                            cmd.Parameters.Add(new SqlParameter("@EducationProviderID", vInput.EducationProviderID));
                            cmd.Parameters.Add(new SqlParameter("@StateProvinceAbv", vInput.StateProvinceAbv));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "PreEducation Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1579-25470].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeletePreEducation([FromBody] IputDeletePreEducation vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspPreEducationDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@PreEducationID", vInput.PreEducationID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "PreEducation Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1579-75371].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertProduct([FromBody] IputUpsertProduct vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.ProductID == 0)
                {
                    // INSERT Product
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspProductInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ProductName", vInput.ProductName));
                            cmd.Parameters.Add(new SqlParameter("@ProductAbv", vInput.ProductAbv));
                            cmd.Parameters.Add(new SqlParameter("@EffectiveDate", vInput.EffectiveDate));
                            cmd.Parameters.Add(new SqlParameter("@ExpireDate", vInput.ExpireDate));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE Product
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspProductUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ProductID", vInput.ProductID));
                            cmd.Parameters.Add(new SqlParameter("@ProductName", vInput.ProductName));
                            cmd.Parameters.Add(new SqlParameter("@ProductAbv", vInput.ProductAbv));
                            cmd.Parameters.Add(new SqlParameter("@EffectiveDate", vInput.EffectiveDate));
                            cmd.Parameters.Add(new SqlParameter("@ExpireDate", vInput.ExpireDate));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Product Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1579-23410].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteProduct([FromBody] IputDeleteProduct vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspProductDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@ProductID", vInput.ProductID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Product Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1579-23410].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertRequiredLicense([FromBody] IputUpsertRequiredLicense vInput)
        {

            var result = new ReturnResult();
            try
            {
                if (vInput.RequiredLicenseID == 0)
                {
                    // INSERT RequiredLicense
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspRequiredLicenseInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@WorkStateAbv", vInput.LicenseID));
                            cmd.Parameters.Add(new SqlParameter("@ResStateAbv", vInput.ResStateAbv));
                            cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID));
                            cmd.Parameters.Add(new SqlParameter("@BranchCode", vInput.BranchCode));
                            cmd.Parameters.Add(new SqlParameter("@RequirementType", vInput.RequirementType));
                            cmd.Parameters.Add(new SqlParameter("@LicLevel1", vInput.LicLevel1));
                            cmd.Parameters.Add(new SqlParameter("@LicLevel2", vInput.LicLevel2));
                            cmd.Parameters.Add(new SqlParameter("@LicLevel3", vInput.LicLevel3));
                            cmd.Parameters.Add(new SqlParameter("@LicLevel4", vInput.LicLevel4));
                            cmd.Parameters.Add(new SqlParameter("@PLS_Incentive1", vInput.PLS_Incentive1));
                            cmd.Parameters.Add(new SqlParameter("@Incentive2_Plus", vInput.Incentive2_Plus));
                            cmd.Parameters.Add(new SqlParameter("@LicIncentive3", vInput.LicIncentive3));
                            cmd.Parameters.Add(new SqlParameter("@StartDocument", vInput.StartDocument));
                            cmd.Parameters.Add(new SqlParameter("@RenewalDocument", vInput.RenewalDocument));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE RequiredLicense
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspRequiredLicenseUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@RequiredLicenseID", vInput.RequiredLicenseID));
                            cmd.Parameters.Add(new SqlParameter("@WorkStateAbv", vInput.LicenseID));
                            cmd.Parameters.Add(new SqlParameter("@ResStateAbv", vInput.ResStateAbv));
                            cmd.Parameters.Add(new SqlParameter("@LicenseID", vInput.LicenseID));
                            cmd.Parameters.Add(new SqlParameter("@BranchCode", vInput.BranchCode));
                            cmd.Parameters.Add(new SqlParameter("@RequirementType", vInput.RequirementType));
                            cmd.Parameters.Add(new SqlParameter("@LicLevel1", vInput.LicLevel1));
                            cmd.Parameters.Add(new SqlParameter("@LicLevel2", vInput.LicLevel2));
                            cmd.Parameters.Add(new SqlParameter("@LicLevel3", vInput.LicLevel3));
                            cmd.Parameters.Add(new SqlParameter("@LicLevel4", vInput.LicLevel4));
                            cmd.Parameters.Add(new SqlParameter("@PLS_Incentive1", vInput.PLS_Incentive1));
                            cmd.Parameters.Add(new SqlParameter("@Incentive2_Plus", vInput.Incentive2_Plus));
                            cmd.Parameters.Add(new SqlParameter("@LicIncentive3", vInput.LicIncentive3));
                            cmd.Parameters.Add(new SqlParameter("@StartDocument", vInput.StartDocument));
                            cmd.Parameters.Add(new SqlParameter("@RenewalDocument", vInput.RenewalDocument));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "RequiredLicense Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1579-23420].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteRequiredLicense([FromBody] IputDeleteRequiredLicense vInput)
        {
            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspRequiredLicenseDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@RequiredLicenseID", vInput.RequiredLicenseID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "RequiredLicense Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1579-23110].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult UpsertStateProvince([FromBody] IputUpsertStateProvince vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.UpsertType == "INSERT")
                {
                    // INSERT StateProvince
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspStateProvinceInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@StateProvinceCode", vInput.StateProvinceCode));
                            cmd.Parameters.Add(new SqlParameter("@StateProvinceName", vInput.StateProvinceName));
                            cmd.Parameters.Add(new SqlParameter("@Country", vInput.Country));
                            cmd.Parameters.Add(new SqlParameter("@StateProvinceAbv", vInput.StateProvinceAbv));
                            cmd.Parameters.Add(new SqlParameter("@LicenseTechID", vInput.LicenseTechID));
                            cmd.Parameters.Add(new SqlParameter("@DOIName", vInput.DOIName));
                            cmd.Parameters.Add(new SqlParameter("@IsActive", vInput.IsActive));
                            cmd.Parameters.Add(new SqlParameter("@AddressType", vInput.AddressType));
                            cmd.Parameters.Add(new SqlParameter("@Address1", vInput.Address1));
                            cmd.Parameters.Add(new SqlParameter("@Address2", vInput.Address2));
                            cmd.Parameters.Add(new SqlParameter("@City", vInput.City));
                            cmd.Parameters.Add(new SqlParameter("@State", vInput.State));
                            cmd.Parameters.Add(new SqlParameter("@Phone", vInput.Phone));
                            cmd.Parameters.Add(new SqlParameter("@Zip", vInput.Zip));
                            cmd.Parameters.Add(new SqlParameter("@Fax", vInput.Fax));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE StateProvince
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspStateProvinceUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@StateProvinceCode", vInput.StateProvinceCode));
                            cmd.Parameters.Add(new SqlParameter("@StateProvinceName", vInput.StateProvinceName));
                            cmd.Parameters.Add(new SqlParameter("@Country", vInput.Country));
                            cmd.Parameters.Add(new SqlParameter("@StateProvinceAbv", vInput.StateProvinceAbv));
                            cmd.Parameters.Add(new SqlParameter("@LicenseTechID", vInput.LicenseTechID));
                            cmd.Parameters.Add(new SqlParameter("@DOIName", vInput.DOIName));
                            cmd.Parameters.Add(new SqlParameter("@IsActive", vInput.IsActive));
                            cmd.Parameters.Add(new SqlParameter("@AddressType", vInput.AddressType));
                            cmd.Parameters.Add(new SqlParameter("@Address1", vInput.Address1));
                            cmd.Parameters.Add(new SqlParameter("@Address2", vInput.Address2));
                            cmd.Parameters.Add(new SqlParameter("@City", vInput.City));
                            cmd.Parameters.Add(new SqlParameter("@State", vInput.State));
                            cmd.Parameters.Add(new SqlParameter("@Phone", vInput.Phone));
                            cmd.Parameters.Add(new SqlParameter("@Zip", vInput.Zip));
                            cmd.Parameters.Add(new SqlParameter("@Fax", vInput.Fax));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "StateProvince Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1571-26710].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteStateProvince([FromBody] IputDeleteStateProvince vInput)
        {

            var result = new ReturnResult();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspStateProvinceDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@StateProvinceAbv", vInput.StateProvinceCode));
                        cmd.Parameters.Add(new SqlParameter("@DOIAddressID", vInput.DOIAddressID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "StateProvince Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# ADMN-1571-26710].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
    }
}
