using DataModel.Response;

namespace OneTrack_v2.Services
{
    public interface ILicenseInfo
    {
        public ReturnResult GetIncentiveInfo(int vEmployeelicenseID);
    }
}
