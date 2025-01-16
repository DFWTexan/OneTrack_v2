using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using DataModel.Response;
using OneTrack_v2.DbData;

namespace OneTrack_v2.Services
{
    public class UtilityHelpService : IUtilityHelpService
    {
        private readonly IConfiguration _config;
        private readonly AppDataContext _db;
        private readonly string? _connectionString;

        public UtilityHelpService(AppDataContext db, IConfiguration configuration)
        {
            _db = db;
            _config = configuration;
            //_connectionString = _config.GetConnectionString(name: "DefaultConnection");
            var environment = _config["Environment"];
            if (string.IsNullOrEmpty(environment))
            {
                throw new InvalidOperationException("Environment is not specified in the configuration.");
            }

            // Get the connection string for the designated environment
            _connectionString = _config.GetSection($"EnvironmentSettings:{environment}:DefaultConnection").Value;

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException($"Connection string for environment '{environment}' is not configured.");
            }
        }

        public void LogAudit(string vBaseTableName, int vBaseTableKeyValue, string? vUserSOEID = null, string? vAuditAction = null, 
            string? vField1Name = null, string? vField1ValueBefore = null, string? vField1ValueAfter= null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspAuditLog", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@BaseTableName", SqlDbType.VarChar, 50).Value = vBaseTableName;
                    cmd.Parameters.Add("@BaseTableKeyValue", SqlDbType.VarChar, 50).Value = vBaseTableKeyValue;
                    cmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 50).Value = vUserSOEID;
                    cmd.Parameters.Add("@AuditAction", SqlDbType.VarChar, 50).Value = vAuditAction;

                    cmd.Parameters.Add("@Field1Name", SqlDbType.VarChar, 500).Value = vField1Name;
                    cmd.Parameters.Add("@Field1ValueBefore", SqlDbType.VarChar, 500).Value = vField1ValueBefore;
                    cmd.Parameters.Add("@Field1ValueAfter", SqlDbType.VarChar, 500).Value = vField1ValueAfter;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void LogInfo(string vInfoText, string? vInfoSource = null)
        {
            CreateLog("OneTrakV2-Info", vInfoText, vInfoSource, "INFO");
        }

        public void LogError(string vErrorText, string vErrorSource, object errorObject, string? vUserSOEID = null)
        {
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    //using (var command = new SqlCommand("[uspErrorLog]", connection))
                //{
                //    command.CommandType = CommandType.StoredProcedure;

                //    command.Parameters.Add("@ErrorText", SqlDbType.VarChar, 500).Value = vErrorText;
                //    command.Parameters.Add("@ErrorSource", SqlDbType.VarChar, 500).Value = vErrorSource;
                //    command.Parameters.Add("@ErrorObject", SqlDbType.VarChar, 500).Value = errorObject;
                //    command.Parameters.Add("@UserSOEID", SqlDbType.VarChar, 50).Value = vUserSOEID;

                //    connection.Open();
                //    command.ExecuteNonQuery();
                //}
            //}
            CreateLog("OneTrakV2-Error", vErrorText, vErrorSource, "ERROR");
        }

        private void CreateLog(string strApplication, string strMsg, string? strAdditionalInfo = null, string msgType = "ERROR")
        {
            // Build the configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Get the environment
            var environment = configuration["Environment"];

            // Get the OneTrakV2LogPath for the current environment
            var oneTrakV2LogPath = configuration[$"EnvironmentSettings:{environment}:OneTrakV2LogPath"];

            try
            {
                string path = oneTrakV2LogPath + strApplication + "_" + System.DateTime.Today.ToString("yyyy-MM-dd") + ".log";

                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();

                    using (TextWriter tw = new StreamWriter(path))
                    {
                        tw.WriteLine(strApplication + " ***** " + System.DateTime.Now.ToString() + Environment.NewLine);
                        //tw.WriteLine(msgType == "ERROR" ? strAdditionalInfo : "INFO" + " => " + strMsg + Environment.NewLine);
                        string strLog = string.Format("{0} => {1}", msgType == "ERROR" ? strAdditionalInfo : "INFO", strMsg);
                        
                        tw.WriteLine(strLog + Environment.NewLine);
                        //tw.WriteLine(strAdditionalInfo + Environment.NewLine);
                        tw.WriteLine(Environment.NewLine);
                    }

                }
                else if (File.Exists(path))
                {
                    using (StreamWriter w = File.AppendText(path))
                    {
                        w.WriteLine(strApplication + " ***** " + System.DateTime.Now.ToString() + Environment.NewLine);
                        //w.WriteLine(msgType == "ERROR" ? strAdditionalInfo : "INFO" + " => " + strMsg + Environment.NewLine);
                        string strLog = string.Format("{0} => {1}", msgType == "ERROR" ? strAdditionalInfo : "INFO", strMsg);

                        w.WriteLine(strLog + Environment.NewLine);
                        //w.WriteLine(strAdditionalInfo + Environment.NewLine);
                        w.WriteLine(Environment.NewLine);
                    }
                }

            }

            catch (Exception myex)
            {
                //SendEmail("wcfOneTrak Error", myex.Message.ToString() + Environment.NewLine + myex.StackTrace.ToString() + Environment.NewLine + myex.TargetSite.Name.ToString());

            }

        }

        public ReturnResult ExceptionReturnResult(Exception vException)
        {
            var errors = new List<string> { vException.Message };

            if (vException.InnerException != null)
                errors.Add(vException.InnerException.Message);

            return new ReturnResult() { StatusCode = 500, Success = false, ObjData = null, Errors = errors};
        }

        public void ExecuteErrorHandling()
        {
            // Placeholder method to execute usp_GetErrorInfo stored procedure for error handling
            throw new NotImplementedException();
        }
    }
}
