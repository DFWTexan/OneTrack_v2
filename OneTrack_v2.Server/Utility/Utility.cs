using OneTrack_v2.DbData;

namespace UtilityService
{
    public class Utility
    {
        private readonly AppDataContext _db;
        private readonly IConfiguration _config;


        public Utility(IConfiguration config, AppDataContext context)
        {
            _config = config;
            _db = context;
        }

        #region "ReturnResult"
        public DataModel.Response.ReturnResult SuccessReturnResult(Object vObject)
        {
            return new DataModel.Response.ReturnResult() { Success = true, StatusCode = 200, ObjData = vObject };
        }

        public DataModel.Response.ReturnResult ErrorReturnResult(string vMessage)
        {
            return new DataModel.Response.ReturnResult() { Success = false, StatusCode = 500, ErrMessage = vMessage };
        }

        public DataModel.Response.ReturnResult ForbiddenResult(Exception vException)
        {
            return new DataModel.Response.ReturnResult() { Success = false, StatusCode = 403, ErrMessage = vException.Message };
        }

        public DataModel.Response.ReturnResult BadRequestResult (string vMessage)
        {
            return new DataModel.Response.ReturnResult() { Success = false, StatusCode = 400, ErrMessage = vMessage };
        } 

        public DataModel.Response.ReturnResult NotFoundResult(string vMessage)
        {
            return new DataModel.Response.ReturnResult() { Success = false, StatusCode = 404, ErrMessage = vMessage };
        }
        #endregion
    }
}
