using OneTrack_v2.DbData;

namespace AdminService
{
    public class Admin
    {
        private readonly AppDataContext _db;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        private readonly UtilityService.Utility _utility;
        //private readonly ILogger _logger;

        public Admin(AppDataContext db, IConfiguration config, IWebHostEnvironment env, UtilityService.Utility utility)
        {
            _db = db;
            _config = config;
            _env = env;
            _utility = utility;
            //_logger = logger;
        }

        public DataModel.Response.ReturnResult ContinuingEducation(int vStateProvinceCode, string vEmployeeSSN, string vGEID, int vNationalProducerNumber, string vLastName, string vFirstName,
               string vAgentStatus, string vResState, string vWrkState, string vBranchCode, int vEmployeeLicenseID, string vLicStatus, string vLicState, string vLicenseName, int vEmploymentID)
        {
            var result = new DataModel.Response.ReturnResult();
            try
            {
                //var employees = _db.Employees.Where(x => x.FirstName.Contains(vFirstName) || x.LastName.Contains(vLastName)).ToList();
                //result.ObjData = employees;
                //result.Success = true;



            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrMessage = ex.Message;
                //_logger.LogError(ex, "Error in EmployeeService.SearchEmployee()");
            }
            return result;
        }

    }
}
