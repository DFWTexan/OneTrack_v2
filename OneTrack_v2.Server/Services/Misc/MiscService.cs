using DataModel.Response;
using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DataModel;
using OneTrack_v2.DbData;

namespace OneTrack_v2.Services
{
    public class MiscService : IMiscService
    {
        private readonly AppDataContext _db;

        public MiscService(AppDataContext db) { _db = db;}

        public ReturnResult GetStateProvinces()
        {
            var result = new ReturnResult();
            try
            {
                var stateProvincses = _db.StateProvinces
                                    .OrderBy(_db => _db.StateProvinceName)
                                    .Select(_db => new OputVarDropDownList_v2
                                    {
                                        Key = _db.StateProvinceAbv,
                                        Value = _db.StateProvinceAbv
                                    });

                result.Success = true;
                result.StatusCode = 200;
                result.ObjData = stateProvincses;
                
                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }
        }
        public ReturnResult GetBranches()
        {             var result = new ReturnResult();
            try
            {

                var sql = @"SELECT 
                                BranchCode as [Key]
                                , TRIM(ISNULL(b.Name,CONCAT('ZZ', BranchCode))) AS Value
                            FROM 
                                (SELECT h.EmploymentID, BranchCode 
                                 FROM dbo.TransferHistory h
                                 INNER JOIN (
                                     SELECT h.EmploymentID, MAX(TransferDate) AS 'MaxDate'
                                     FROM dbo.TransferHistory h
                                     INNER JOIN dbo.Employment m ON h.EmploymentID = m.EmploymentID
                                     GROUP BY h.EmploymentID
                                 ) M ON h.EmploymentID = M.EmploymentID
                            WHERE BranchCode IS NOT NULL ) X
                            LEFT OUTER JOIN [dbo].[BIF] b ON RIGHT(x.BranchCode, 8) = RIGHT(b.HR_Department_ID, 8)
                            GROUP BY 
                                BranchCode, b.Name
                            ORDER BY 
                                TRIM(ISNULL(b.Name,CONCAT('ZZ', BranchCode)))";

                var queryResult = _db.Set<OputVarDropDownList_v2>()
                                      .FromSqlRaw(sql)
                                      .ToList();

                result.ObjData = queryResult;
                result.Success = true;
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
        public ReturnResult GetScoreNumbers()
        {
            var result = new ReturnResult();
            try
            {
                var sql = @"SELECT 
                                 ISNULL(ScoreNumber,'0000') AS [Key]
                                 , ISNULL(ScoreNumber,'0000') AS Value
                            FROM 
                                 (SELECT h.EmploymentID, BranchCode 
                                  FROM dbo.TransferHistory h
                                  INNER JOIN (
                                    SELECT h.EmploymentID, MAX(TransferDate) AS 'MaxDate'
                                    FROM dbo.TransferHistory h
                                    INNER JOIN dbo.Employment m ON h.EmploymentID = m.EmploymentID
                                    GROUP BY h.EmploymentID
                                 ) M ON h.EmploymentID = M.EmploymentID
                            WHERE BranchCode IS NOT NULL ) X
	                        Left OUTER JOIN [dbo].[BIF] b ON RIGHT(x.BranchCode,8) = RIGHT(b.HR_Department_ID,8)
                            GROUP BY 
                                  ScoreNumber
                            ORDER BY 
                                 ScoreNumber";

                var queryResult = _db.Set<OputVarDropDownList_v2>()
                                      .FromSqlRaw(sql)
                                      .ToList();

                result.ObjData = queryResult;
                result.Success = true;
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
        public ReturnResult GetEmployerAgencies()
        {
            var result = new ReturnResult();
            try
            {
                // TBD: Placeholder method to return a list of employer agencies

                result.Success = true;
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
        public ReturnResult GetLicenseStatuses()
        {
            var result = new ReturnResult();
            try
            {
                // TBD: Placeholder method to return a list of license statuses

                result.Success = true;
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
        public ReturnResult GetLicenseNames()
        {
            var result = new ReturnResult();
            try
            {
                // TBD: Placeholder method to return a list of license names

                result.Success = true;
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
        public ReturnResult GetEmailTemplates()
        {
            var result = new ReturnResult();
            try
            {
                // TBD: Placeholder method to return a list of email templates

                result.Success = true;
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
        public ReturnResult GetTicklerMessageTypes()
        {
            var result = new ReturnResult();
            try
            {
                // TBD: Placeholder method to return a list of tickler message types

                result.Success = true;
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
