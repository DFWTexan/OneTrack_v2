using DataModel.Response;
using OneTrack_v2.DbData;

namespace OneTrak_v2.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDataContext _db;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        private readonly UtilityService.Utility _utility;
        //private readonly ILogger _logger;

        public AdminService(AppDataContext db, IConfiguration config, IWebHostEnvironment env, UtilityService.Utility utility)
        {
            _db = db;
            _config = config;
            _env = env;
            _utility = utility;
            //_logger = logger;
        }

        public ReturnResult GetCompanyTypes()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.Comp/*a*/nyTypes.ToList();
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
        
        public ReturnResult GetCompanyByType(int vCompanyTypeID)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.Companies.Where(x => x.CompanyTypeID == vCompanyTypeID).ToList();
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

        public ReturnResult GetLicenseTypes()
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.LicenseTypes.ToList();
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

        public ReturnResult GetConEduLicenses(string vState, int LicenesTypeID)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.ConEduLicenses.Where(x => x.State == vState && x.LicenseTypeID == LicenesTypeID).ToList();
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

        public ReturnResult GetCompanyRequirements(string vWorkState, string vResState)
        {
            ReturnResult result = new ReturnResult();
            try
            {
                //result.ObjData = _db.CompanyRequirements.Where(x => x.WorkState == vWorkState && x.ResState == vResState).ToList();
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
                //result.ObjData = _db.DropdownListTypes.ToList();
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
                //result.ObjData = _db.DropdownLists.Where(x => x.LkpField == vLkpField).ToList();
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
