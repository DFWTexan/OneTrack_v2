using DataModel.Response;
using OneTrack_v2.DbData;

namespace OneTrak_v2.Services
{
    public class WorklistService : IWorklistService
    {
        private readonly AppDataContext _db;

        public WorklistService(AppDataContext db)
        {
            _db = db;
        }
        public ReturnResult GetWorklistData(string? vWorklistName = null, string? vWorklistDate = null, string? vLicenseTech = null)
        {
            var result = new ReturnResult();
            try
            {

                var query1 = _db.WorkLists
                            .Join(_db.WorkListData,
                                workList => workList.WorkListName,
                                workListData => workListData.WorkListName,
                                (workList, workListData) => new { WorkList = workList, WorkListData = workListData })
                            .Where(x => x.WorkList.IsActive == true &&
                                        x.WorkListData.ProcessDate == null &&
                                        x.WorkList.WorkListName == vWorklistName &&
                                        x.WorkListData.LicenseTech == vLicenseTech)
                            .Select(x => "WorkListDataID|CreateDate|" + x.WorkList.Fieldlist)
                            .Distinct()
                            .FirstOrDefault();

                var query2 = _db.WorkLists
                            .Join(_db.WorkListData,
                                workList => workList.WorkListName,
                                workListData => workListData.WorkListName,
                                (workList, workListData) => new { WorkList = workList, WorkListData = workListData })
                            .Where(x => x.WorkList.IsActive == true &&
                                        x.WorkListData.ProcessDate == null &&
                                        x.WorkList.WorkListName == vWorklistName &&
                                        x.WorkListData.LicenseTech == vLicenseTech &&
                                        x.WorkListData.WorkListDataId != null)
                            .Select(x => x.WorkListData.WorkListDataId.ToString() + "|" +
                                         x.WorkListData.CreateDate.ToString() + "|" +
                                         x.WorkListData.WorkListData)
                            .ToList();

                if (query1 != null)
                {
                    query2 = query2.Prepend(query1).ToList();
                }

                var resultJoin = query2;

                result.Success = true;
                result.ObjData = resultJoin;
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }
        }
    }
}
