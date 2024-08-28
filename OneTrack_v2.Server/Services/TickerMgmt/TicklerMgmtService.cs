using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DbData;
using OneTrack_v2.DbData.Models;
using OneTrack_v2.Services;
using OneTrak_v2.DataModel;
using System.Data;
using System.Linq;

namespace OneTrak_v2.Services
{
    public class TicklerMgmtService : ITicklerMgmtService
    {
        private readonly AppDataContext _db;
        private readonly IConfiguration _config;
        private readonly IUtilityHelpService _utilityService;
        private readonly string? _connectionString;

        public TicklerMgmtService(AppDataContext db, IConfiguration config, IUtilityHelpService utilityHelpService)
        {
            _db = db;
            _config = config;
            _connectionString = _config.GetConnectionString(name: "DefaultConnection");
            _utilityService = utilityHelpService;
        }

        public ReturnResult GetStockTickler()
        {
            var result = new ReturnResult();
            try
            {
                var sql = @"SELECT LkpField
                            ,LkpValue
                            ,SortOrder
                            FROM [dbo].[lkp_TypeStatus]
                            WHERE [LkpField] = 'Tickler'
                            UNION
                            SELECT 'Tickler' AS LkpField, 'Tickler' AS [LkpValue], '1' AS SortOrder";

                var resultStockTicklers = _db.OputStockTickler
                                                .FromSqlRaw(sql)
                                                .AsNoTracking()
                                                .OrderBy(x => x.SortOrder) // Apply ordering
                                                .ToList(); // Load data into memory

                //.FirstOrDefault();

                result.Success = true;
                result.ObjData = resultStockTicklers;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "EMFTEST-Source", new { }, "EMFTEST-UserSOEID");
            };

            return result;
        }

        public ReturnResult GetTicklerInfo(int vTicklerID = 0, int vLicenseTechID = 0, int vEmploymentID = 0)
        {
            var result = new ReturnResult();
            try
            {
                var query = from t in _db.Ticklers
                            join lt in _db.LicenseTeches on t.LicenseTechId equals lt.LicenseTechId
                            join m in _db.Employments on t.EmploymentId equals m.EmploymentId into mGroup
                            from m in mGroup.DefaultIfEmpty()
                            join e in _db.Employees on m.EmployeeId equals e.EmployeeId into eGroup
                            from e in eGroup.DefaultIfEmpty()
                            join el in _db.EmployeeLicenses on t.EmployeeLicenseId equals el.EmployeeLicenseId into elGroup
                            from el in elGroup.DefaultIfEmpty()
                            join l in _db.Licenses on el.LicenseId equals l.LicenseId into lGroup
                            from l in lGroup.DefaultIfEmpty()
                            join loa in _db.LineOfAuthorities on l.LineOfAuthorityId equals loa.LineOfAuthorityId into loaGroup
                            from loa in loaGroup.DefaultIfEmpty()
                            where (vLicenseTechID == 0 || t.LicenseTechId == vLicenseTechID) &&
                            (vEmploymentID == 0 || t.EmploymentId == vEmploymentID) &&
                            (vTicklerID == 0 || t.TicklerId == vTicklerID) &&
                            t.TicklerCloseByLicenseTechId == null
                                        select new
                                        {
                                            t.TicklerId,
                                            t.TicklerDate,
                                            t.TicklerDueDate,
                                            t.LicenseTechId,
                                            t.EmploymentId,
                                            t.EmployeeLicenseId,
                                            e.EmployeeId,
                                            loa.LineOfAuthorityName,
                                            e.Geid,
                                            TeamMemberName = e.LastName + ", " + e.FirstName,
                                            t.TicklerCloseDate,
                                            t.TicklerCloseByLicenseTechId,
                                            Message = (e.FirstName + " " + e.LastName + "\r\n" + "TM-" + e.Geid + "\r\n" ?? "") +
                                                      (loa.LineOfAuthorityName + "\r\n" ?? "") +
                                                      (t.LkpValue == "Other" ? t.Message : t.LkpValue),
                                            t.LkpValue
                                        };

                var resultTicklers = query.ToList();

                result.Success = true;
                result.ObjData = resultTicklers;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "EMFTEST-Source", new { }, "EMFTEST-UserSOEID");
            }

            return result;
        }

        public ReturnResult GetLicenseTech(int vLicenseTechID = 0, string? vSOEID = null)
        {
            var result = new ReturnResult();
            try
            {

                var query = from l in _db.LicenseTeches
                            where (vLicenseTechID == 0 || l.LicenseTechId == vLicenseTechID) &&
                                  (vSOEID == null || l.Soeid == vSOEID) &&
                                  l.IsActive == true
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

                var resultLicTechs = query.ToList();

                result.Success = true;
                result.ObjData = resultLicTechs;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;

                _utilityService.LogError(ex.Message, "EMFTEST-Source", new { }, "EMFTEST-UserSOEID");
            }

            return result;
        }
        public ReturnResult UpsertTickler([FromBody] IputUpsertTicklerMgmt vInput)
        {
            var result = new ReturnResult();
            try
            {
                if (vInput.TicklerID == 0)
                {
                    // INSERT Tickler
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspTicklerInsert", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@LkpValue", vInput.LkpValue));
                            cmd.Parameters.Add(new SqlParameter("@Message", vInput.Message));
                            cmd.Parameters.Add(new SqlParameter("@TicklerDate", DateTime.Now));
                            cmd.Parameters.Add(new SqlParameter("@TicklerDueDate", vInput.TicklerDueDate));
                            cmd.Parameters.Add(new SqlParameter("@LicenseTechID", vInput.LicenseTechID));
                            cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                            cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                            cmd.Parameters.Add(new SqlParameter("@TicklerCloseDate", vInput.TicklerCloseDate));
                            cmd.Parameters.Add(new SqlParameter("@TicklerCloseByLicenseTechID", vInput.TicklerCloseByLicenseTechID));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    // UPDATE Tickler
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("uspTicklerUpdate", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@TicklerID", vInput.TicklerID));
                            cmd.Parameters.Add(new SqlParameter("@LkpValue", vInput.LkpValue));
                            cmd.Parameters.Add(new SqlParameter("@Message", vInput.Message));
                            cmd.Parameters.Add(new SqlParameter("@TicklerDate", vInput.TicklerDate));
                            cmd.Parameters.Add(new SqlParameter("@TicklerDueDate", vInput.TicklerDueDate));
                            cmd.Parameters.Add(new SqlParameter("@LicenseTechID", vInput.LicenseTechID));
                            cmd.Parameters.Add(new SqlParameter("@EmploymentID", vInput.EmploymentID));
                            cmd.Parameters.Add(new SqlParameter("@EmployeeLicenseID", vInput.EmployeeLicenseID));
                            cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Tickler Created/Updated Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# TCKLR-1509-49597].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult CloseTickler([FromBody] IputCloseTicklerMgmt vInput)
        {

            var result = new ReturnResult();
            try
            {
                // CLOSE Tickler
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspTicklerClose", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@TicklerID", vInput.TicklerID));
                        cmd.Parameters.Add(new SqlParameter("@TicklerCloseByLicenseTechID", vInput.TicklerCloseByLicenseTechID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Tickler Closed Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# TCKLR-1509-29197].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
        public ReturnResult DeleteTickler([FromBody] IputDeleteTicklerMgmt vInput)
        {
             var result = new ReturnResult();
            try
            {
                // DELETE Tickler
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspTicklerDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@TicklerID", vInput.TicklerID));
                        cmd.Parameters.Add(new SqlParameter("@UserSOEID", vInput.UserSOEID));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.ObjData = new { Message = "Tickler Deleted Successfully." };
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = "Server Error - Please Contact Support [REF# TCKLR-1509-29133].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, vInput.UserSOEID);
            }

            return result;
        }
    }
}
