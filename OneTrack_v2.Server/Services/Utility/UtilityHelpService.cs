using Microsoft.Data.SqlClient;
using System.Data;

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

        public void ExecuteErrorHandling()
        {
            // Placeholder method to execute usp_GetErrorInfo stored procedure for error handling
            throw new NotImplementedException();
        }
    }
}
