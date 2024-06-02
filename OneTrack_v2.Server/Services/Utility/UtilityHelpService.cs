using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using DataModel.Response;

namespace OneTrack_v2.Services
{
    public class UtilityHelpService : IUtilityHelpService
    {
        private readonly IConfiguration _config;
        private readonly string? _connectionString;

        public UtilityHelpService(IConfiguration configuration)
        {
            _config = configuration;
            _connectionString = _config.GetConnectionString(name: "DefaultConnection");
        }

        public void LogAudit(string vBaseTableName, int vBaseTableKeyValue, string? vUserSOEID = null, string? vAuditAction = null, 
            string? vField1Name = null, string? vField1ValueBefore = null, string? vField1ValueAfter= null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("[uspAuditLog]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@BaseTableName", SqlDbType.VarChar, 50).Value = vBaseTableName;
                    command.Parameters.Add("@BaseTableKeyValue", SqlDbType.VarChar, 50).Value = vBaseTableKeyValue;
                    command.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 50).Value = vUserSOEID;
                    command.Parameters.Add("@AuditAction", SqlDbType.VarChar, 50).Value = vAuditAction;

                    command.Parameters.Add("@Field1Name", SqlDbType.VarChar, 500).Value = vField1Name;
                    command.Parameters.Add("@Field1ValueBefore", SqlDbType.VarChar, 500).Value = vField1ValueBefore;
                    command.Parameters.Add("@Field1ValueAfter", SqlDbType.VarChar, 500).Value = vField1ValueAfter;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void LogInfo(string vInfoText, string vInfoSource)
        {
            CreateLog("OneTrakV2-Error", vInfoText, null, "INFO");
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
            CreateLog("OneTrakV2-Error", vErrorText, null, "ERROR");
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
                        tw.WriteLine(msgType + " => " + strMsg + Environment.NewLine);
                        tw.WriteLine(strAdditionalInfo + Environment.NewLine);
                        tw.WriteLine(Environment.NewLine);
                    }

                }
                else if (File.Exists(path))
                {
                    using (StreamWriter w = File.AppendText(path))
                    {
                        w.WriteLine(strApplication + " ***** " + System.DateTime.Now.ToString() + Environment.NewLine);
                        w.WriteLine(msgType + " => " + strMsg + Environment.NewLine);
                        w.WriteLine(strAdditionalInfo + Environment.NewLine);
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
