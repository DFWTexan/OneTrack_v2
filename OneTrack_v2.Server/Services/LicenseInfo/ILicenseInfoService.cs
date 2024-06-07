using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using OneTrak_v2.DataModel;

namespace OneTrak_v2.Services
{
    public interface ILicenseInfoService
    {
        public ReturnResult GetIncentiveInfo(int vEmployeelicenseID);
        public ReturnResult GetIncentiveRolloutGroups();
        public ReturnResult GetIncentiveBMMgrs();
        public ReturnResult GetIncentiveDMMrgs();
        public ReturnResult GetIncentiveTechNames();
        public ReturnResult GetAffiliatedLicenses(string vStateProvinceAbv, int vLicenseID);
        public ReturnResult AddLicenseAppointment([FromBody] IputAddLicenseAppointment vInput);
        public ReturnResult UpdateLicenseAppointment([FromBody] IputUpdateLicenseAppointment vInput);
    }
}
