using DataModel.Response;

namespace OneTrak_v2.Services
{
    public interface ILicenseInfo
    {
        public ReturnResult GetIncentiveInfo(int vEmployeelicenseID);
        public ReturnResult GetIncentiveRolloutGroups();
        public ReturnResult GetIncentiveBMMgrs();
        public ReturnResult GetIncentiveDMMrgs();
        public ReturnResult GetIncentiveTechNames();
    }
}
