using DataModel.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

                var sql = @"
                        SELECT DISTINCT 
                            'WorklistDataID|CreateDate|' + FieldList as WorkListData 
                        FROM dbo.WorkList 
                        INNER JOIN dbo.WorkListData ON WorkList.WorkListName = WorkListData.WorkListName
	                    WHERE WorkList.IsActive = 1
	                        AND ProcessDate IS NULL
	                        AND WorkList.WorkListNAME = @WorkListName
	                        AND LicenseTech = @LicenseTech

	                    Union all

	                    SELECT 
                            CONVERT(VARCHAR,WorkListDataID) + '|' 
	                        + REPLACE(CONVERT(VARCHAR(10), CreateDate, 111), '/', '-')  
	                        + '|' + WorkListData as WorklistData 
	                    FROM dbo.WorkList 
                        INNER JOIN dbo.WorkListData ON WorkList.WorkListName = WorkListData.WorkListName
	                    WHERE WorkList.IsActive = 1
	                        AND ProcessDate IS NULL
	                        AND WorkList.WorkListNAME = @WorkListName
	                        AND LicenseTech = @LicenseTech
	                        AND CONVERT(VARCHAR,WorkListDataID) + '|' 
	                            + REPLACE(CONVERT(VARCHAR(10), CreateDate, 111), '/', '-')  
	                            + '|' + WorkListData IS NOT NULL ";

                var parameters = new[]
                            {
                                new SqlParameter("@WorkListName", vWorklistName),
                                new SqlParameter("@WorkListDate", vWorklistDate ?? (object)DBNull.Value),
                                new SqlParameter("@LicenseTech", vLicenseTech)
                            };

                var queryWorklistDataResults = _db.OputWorkListDataItems
                                            .FromSqlRaw(sql, parameters)
                                            .AsNoTracking()
                                            .ToList();

                result.Success = true;
                result.ObjData = queryWorklistDataResults;
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
        public ReturnResult GetWorklistDataByLicenseTech(string vLicenseTech)
        {
            var result = new ReturnResult();
            try
            {

                var sql = @"
                        SELECT DISTINCT 
                            'WorklistDataID|CreateDate|' + FieldList as WorkListData 
                        FROM dbo.WorkList 
                        INNER JOIN dbo.WorkListData ON WorkList.WorkListName = WorkListData.WorkListName
	                    WHERE WorkList.IsActive = 1
	                        AND ProcessDate IS NULL
	                        AND LicenseTech = @LicenseTech

	                    Union all

	                    SELECT 
                            CONVERT(VARCHAR,WorkListDataID) + '|' 
	                        + REPLACE(CONVERT(VARCHAR(10), CreateDate, 111), '/', '-')  
	                        + '|' + WorkListData as WorklistData 
	                    FROM dbo.WorkList 
                        INNER JOIN dbo.WorkListData ON WorkList.WorkListName = WorkListData.WorkListName
	                    WHERE WorkList.IsActive = 1
	                        AND ProcessDate IS NULL
	                        AND LicenseTech = @LicenseTech
	                        AND CONVERT(VARCHAR,WorkListDataID) + '|' 
	                            + REPLACE(CONVERT(VARCHAR(10), CreateDate, 111), '/', '-')  
	                            + '|' + WorkListData IS NOT NULL ";

                var parameters = new[]
                            {
                                new SqlParameter("@LicenseTech", vLicenseTech)
                            };

                var queryWorklistDataResults = _db.OputWorkListDataItems
                                            .FromSqlRaw(sql, parameters)
                                            .AsNoTracking()
                                            .ToList();

                result.Success = true;
                result.ObjData = queryWorklistDataResults;
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
