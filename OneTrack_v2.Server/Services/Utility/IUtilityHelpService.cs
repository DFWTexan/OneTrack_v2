namespace OneTrack_v2.Services
{
    public interface IUtilityHelpService
    {
        public void LogAudit(string vBaseTableName, int vBaseTableKeyValue, string? vUserSOEID = null, string? vAuditAction = null,
            string? vField1Name = null, string? vField1ValueBefore = null, string? vField1ValueAfter = null);
        public void LogError(string vErrorText, string vErrorSource, object errorObject, string? vUserSOEID = null);

        public void ExecuteErrorHandling();
    }
}
