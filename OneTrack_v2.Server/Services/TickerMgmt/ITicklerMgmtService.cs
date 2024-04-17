using DataModel.Response;

namespace OneTrak_v2.Services
{
    public interface ITicklerMgmtService
    {
        public ReturnResult GetTicklerInfo(int vTicklerID, int vLicenseTechID, int vEmploymentID);
        public ReturnResult GetStockTickler();
        public ReturnResult GetLicenseTech(int vLicenseTechID, string vSOEID);
    }
}
