using DataModel.Response;
using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DataModel;
using OneTrack_v2.DbData;
using OneTrack_v2.DbData.Models;
using System.Linq.Expressions;

namespace OneTrack_v2.Services
{
    public class MiscService : IMiscService
    {
        private readonly AppDataContext _db;

        public MiscService(AppDataContext db) { _db = db; }

        public ReturnResult GetStateProvinces()
        {
            var result = new ReturnResult();
            try
            {
                var stateProvincses = _db.StateProvinces
                                    .OrderBy(_db => _db.StateProvinceName)
                                    .Select(_db => new OputVarDropDownList_v2
                                    {
                                        Value = _db.StateProvinceAbv,
                                        Label = _db.StateProvinceAbv
                                    });

                result.Success = true;
                result.StatusCode = 200;
                result.ObjData = stateProvincses;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
            }

            return result;
        }
        public ReturnResult GetBranches()
        {
            var result = new ReturnResult();
            try
            {

                var sql = @"SELECT 
                                BranchCode as [Value]
                                , TRIM(ISNULL(b.Name,CONCAT('ZZ', BranchCode))) AS [Label]
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
                                      .AsNoTracking()
                                      .ToList();

                result.ObjData = queryResult;
                result.Success = true;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
            }

            return result;
        }
        public ReturnResult GetScoreNumbers()
        {
            var result = new ReturnResult();
            try
            {
                var sql = @"SELECT 
                                 ISNULL(ScoreNumber,'0000') AS [Value]
                                 , ISNULL(ScoreNumber,'0000') AS [Label]
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
                                      .AsNoTracking()
                                      .ToList();

                result.ObjData = queryResult;
                result.Success = true;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
            }

            return result;
        }
        public ReturnResult GetEmployerAgencies()
        {
            var result = new ReturnResult();
            try
            {
                var resultEmpAgencies = _db.Companies
                                    .Where(c => c.CompanyType == "Employer" || c.CompanyType == "Agency")
                                    .Select(c => new { Value = c.CompanyId, Label = c.CompanyName })
                                    .AsNoTracking()
                                    .ToList();

                result.Success = true;
                result.ObjData = resultEmpAgencies;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
            }

            return result;
        }
        public ReturnResult GetLicenseStatuses()
        {
            var result = new ReturnResult();
            try
            {
                var resultLicStatuses = _db.LkpTypeStatuses
                            .Where(l => l.LkpField == "LicenseStatusSearch")
                            .OrderBy(l => l.SortOrder)
                            .Select(l => new { Value = l.LkpValue, Label = l.LkpValue })
                            .AsNoTracking()
                            .ToList();

                result.Success = true;
                result.ObjData = resultLicStatuses;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
            }

            return result;
        }
        public ReturnResult GetLicenseNames()
        {
            var result = new ReturnResult();
            try
            {
                var resultLicNames = _db.Licenses
                                .Join(_db.StateProvinces,
                                    license => license.StateProvinceAbv,
                                    stateProvince => stateProvince.StateProvinceAbv,
                                    (license, stateProvince) => new { License = license, StateProvince = stateProvince })
                                .GroupBy(l => l.License.LicenseName)
                                .AsNoTracking()
                                .Select(g => new { Value = g.Key, Label = g.Key })
                                .ToList();

                result.Success = true;
                result.ObjData = resultLicNames;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
            }

            return result;
        }
        public ReturnResult GetEmailTemplates()
        {
            var result = new ReturnResult();
            try
            {
                // TBD: Placeholder method to return a list of email templates

                result.Success = true;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
            }

            return result;
        }
        public ReturnResult GetTicklerMessageTypes()
        {
            var result = new ReturnResult();
            try
            {
                // TBD: Placeholder method to return a list of tickler message types

                result.Success = true;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
            }

            return result;
        }
        public ReturnResult WorkListNames()
        {
            var result = new ReturnResult();
            try
            {

                var activeWorkLists = _db.WorkLists
                                    .Where(wl => wl.IsActive == true)
                                    .OrderBy(wl => wl.WorkListName)
                                    .Select(wl => wl.WorkListName)
                                    .AsNoTracking()
                                    .ToList();

                result.Success = true;
                result.ObjData = activeWorkLists;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;

            }

            return result;
        }
        public ReturnResult GetLicenseTeches()
        {
            var result = new ReturnResult();
            try
            {

                var query = from l in _db.LicenseTeches
                            where l.IsActive == true
                            select new
                            {
                                l.LicenseTechId,
                                l.Soeid,
                                l.FirstName,
                                l.LastName,
                                l.IsActive,
                                l.TeamNum,
                                l.LicenseTechPhone,
                                l.LicenseTechFax,
                                l.LicenseTechEmail,
                                TechName = l.FirstName + " " + l.LastName
                            };

                var resultLicTechs = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultLicTechs;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
            }
            return result;
        }
        public ReturnResult GetBackgroundStatuses()
        {
            var result = new ReturnResult();
            try
            {

                var query = from typeStatus in _db.LkpTypeStatuses
                            where typeStatus.LkpField == "BackgroundStatus"
                            orderby typeStatus.SortOrder, typeStatus.LkpValue
                            select new
                            {
                                LkpValue = typeStatus.LkpValue,
                                //SortOrder = typeStatus.SortOrder
                            };

                var resultBackgroundStatuses = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultBackgroundStatuses;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
            }

            return result;
        }
        public ReturnResult GetJobTitles()
        {
            var result = new ReturnResult();
            try
            {

                var query = from j in _db.JobTitles
                            where j.IsActive == true
                            select new
                            {
                                JobTitleID = j.JobTitleId,
                                JobTitle = j.JobTitle1
                            };

                var resultTitles = query.AsNoTracking().ToList();

                result.Success = true;
                result.ObjData = resultTitles;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
            }

            return result;
        }
    }
}
