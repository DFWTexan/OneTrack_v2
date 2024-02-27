using Microsoft.EntityFrameworkCore;
using System.Data;
using OneTrack_v2.DbData;
using OneTrack_v2.DataModel.StoredProcedures;
using DataModel.Response;

namespace OneTrack_v2.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDataContext _db;
        //private readonly UtilityService.Utility _utility;

        //private readonly IConfiguration _config;
        //private readonly IWebHostEnvironment _env;
        //private readonly ILogger _logger;

        public EmployeeService(AppDataContext db)
        {
            _db = db;
        }

        /// <summary>
        ///
        /// STORED PROCEDURE:  uspEmployeeGridSearchNew
        ///
        /// </summary>
        /// <param name="@vchCompanyID" int></param>
        /// <param name="@vchEmployeeSSN" varchar(25)></param>
        /// <param name="@vchGEID" varchar(25)></param>
        /// <param name="@vchSCORENumber" varchar(25)></param>
        /// <param name="@vchNationalProducerNumber" int></param>
        /// <param name="@vchLastName" varchar(50)></param>
        /// <param name="@vchFirstName" varchar(50)></param>
        /// <param name="@vchAgentStatus" varchar(800)></param>
        /// <param name="@vchResState" varchar(10)></param>
        /// <param name="@vchWrkState" varchar(10)></param>
        /// <param name="@vchBranchCode" varchar(25)></param>
        /// <param name="@vchEmployeeLicenseID" int></param>
        /// <param name="@vchLicStatus" varchar(800)></param>
        /// <param name="@vchLicState" varchar(10)></param>
        /// <param name="@vchLicenseName" varchar(25)></param>
        /// <param name="@vchEmploymentID" int></param>
        /// <returns>
        ///     List of Employees matching the search criteria
        /// 
        ///     {
        ///     // Example of a valid return result from the service
        ///     new SPOUT_uspEmployeeGridSearchNew {
        ///         {
        ///            EmployeeID = 49282,
        ///            GEID = "3303627",
        ///            Name = "SMITH, AASSUIMR",
        ///            ResStateAbv = "TX",
        ///            WorkStateAbv = "TX",
        ///            ScoreNumber = "4356",
        ///            BranchName = "MACHESNEY PARK, IL",
        ///            EmploymentID = 49403,
        ///            State = "  "
        ///        }
        /// </returns>
        public async Task<ReturnResult> SearchEmployee(int vCompanyID, string? vEmployeeSSN, string? vGEID, string? vSCORENumber, 
            int vNationalProducerNumber, string? vLastName, string? vFirstName,
            List<string>? vAgentStatus, string? vResState, string? vWrkState, string? vBranchCode, 
            int vEmployeeLicenseID, string? vLicStatus, string? vLicState, string? vLicenseName, int vEmploymentID)
        {

            var result = new ReturnResult();
            try
            {
                var query = (
                        from e in _db.Employees
                        join ss in _db.EmployeeSsns on e.EmployeeSsnid equals ss.EmployeeSsnid into ess
                        from ss in ess.DefaultIfEmpty()
                        join m in _db.Employments on e.EmployeeId equals m.EmployeeId
                        join a in _db.Addresses on e.AddressId equals a.AddressId into ea
                        from a in ea.DefaultIfEmpty()
                        join t in (
                            from th in _db.TransferHistories
                            group th by th.EmploymentId into g
                            select new { EmploymentID = g.Key, TransferHistoryID = g.Max(x => x.TransferHistoryId) }
                        ) on m.EmploymentId equals t.EmploymentID into mt
                        from t in mt.DefaultIfEmpty()
                        join th in _db.TransferHistories on t.TransferHistoryID equals th.TransferHistoryId into tht
                        from th in tht.DefaultIfEmpty()
                        join el in _db.EmployeeLicenses on t.EmploymentID equals el.EmploymentId into elt
                        from el in elt.DefaultIfEmpty()
                        join bdh in _db.Bifs on th.BranchCode.Substring(th.BranchCode.Length - 8, 8) equals bdh.HrDepartmentId.Substring(bdh.HrDepartmentId.Length - 8, 8) into bdht
                        from bdh in bdht.DefaultIfEmpty()
                        join l in _db.Licenses on el.LicenseId equals l.LicenseId into lel
                        from l in lel.DefaultIfEmpty()
                        //let agentStatusValues = StringToTable(vAgentStatus, ',', true) // This assumes you have a method to split strings
                        //let licStatusValues = StringToTable(vLicStatus, ',', true) // This assumes you have a method to split strings
                        where (m.CompanyId == vCompanyID || vCompanyID == 0) &&
                              (ss.EmployeeSsn1 == vEmployeeSSN || vEmployeeSSN == null) &&
                              (e.Geid == vGEID || vGEID == null) &&
                              (th.Scorenumber == vSCORENumber || vSCORENumber == null) &&
                              (e.NationalProducerNumber == vNationalProducerNumber || vNationalProducerNumber == 0) &&
                              (e.LastName == vLastName || vLastName == null) &&
                              (e.FirstName == vFirstName || vFirstName == null) &&
                              (el.EmploymentId == vEmploymentID || vEmploymentID == 0) //&&
                              // ... Continue with other conditions
                              //agentStatusValues.Any(rs => m.EmployeeStatus == vAgentStatus || vAgentStatus == "All") &&
                              //licStatusValues.Any(rs1 => el.LicenseStatus == vLicStatus || vLicStatus == "All")
                        select new
                        {
                            e.EmployeeId,
                            e.Geid,
                            Name = e.LastName + ", " + e.FirstName + " " + (e.MiddleName != null ? e.MiddleName.Substring(0, 1) : ""),
                            th.ResStateAbv,
                            th.WorkStateAbv,
                            ScoreNumber = bdh.ScoreNumber ?? "0000",
                            BranchName = bdh.Name ?? "UNKNOWN",
                            m.EmploymentId,
                            State = a.State
                        })
                        .GroupBy(x => new
                        {
                            x.EmployeeId,
                            x.Geid,
                            x.Name,
                            x.ResStateAbv,
                            x.WorkStateAbv,
                            x.ScoreNumber,
                            x.BranchName,
                            x.EmploymentId,
                            x.State
                        })
                        .Select(x => new SPOUT_uspEmployeeGridSearchNew
                        {
                            EmployeeID = x.Key.EmployeeId,
                            GEID = x.Key.Geid,
                            Name = x.Key.Name,
                            ResStateAbv = x.Key.ResStateAbv,
                            WorkStateAbv = x.Key.WorkStateAbv,
                            ScoreNumber = x.Key.ScoreNumber,
                            BranchName = x.Key.BranchName,
                            EmploymentID = x.Key.EmploymentId,
                            State = x.Key.State
                        })
                        .AsNoTracking()
                        .OrderBy(x => x.Name);

                var queryResult = await query.ToListAsync();

                result.Success = true;
                result.ObjData = queryResult;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                //_logger.LogError(ex, "Error in EmployeeService.SearchEmployee()");
            }
            return result;
        }

        //private static List<string> StringToTable(string input, char delimiter, bool trimSpace)
        //{
        //    var result = new List<string>();

        //    if (input == null) return result;

        //    var elements = input.Split(new[] { delimiter }, StringSplitOptions.None);
        //    foreach (var element in elements)
        //    {
        //        var val = trimSpace ? element.Trim() : element;
        //        result.Add(val);
        //    }

        //    return result;
        //}

        //public DataModel.Response.ReturnResult GetEmployee(int vEmployeeID)
        //{
        //    var result = new DataModel.Response.ReturnResult();
        //    try
        //    {
        //        var employees = _db.Employees.ToList();
        //        result.ObjData = employees;
        //        result.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.ErrMessage = ex.Message;
        //        //_logger.LogError(ex, "Error in EmployeeService.GetEmployees()");
        //    }
        //    return result;
        //}

    }
}
