using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using OneTrak_v2.DataModel;
using OneTrak_v2.Server.DbData.DataModel.Admin;

namespace OneTrak_v2.Services
{
    public interface ITicklerMgmtService
    {
        public ReturnResult GetTicklerInfo(int vTicklerID, int vLicenseTechID, int vEmploymentID);
        public ReturnResult GetStockTickler();
        public ReturnResult GetLicenseTech(int vLicenseTechID, string vSOEID);
        public ReturnResult UpsertTickler([FromBody] IputUpsertTicklerMgmt vInput);
        public ReturnResult CloseTickler([FromBody] IputCloseTicklerMgmt vInput);
        public ReturnResult DeleteTickler([FromBody] IputDeleteTicklerMgmt vInput);
    }
}
