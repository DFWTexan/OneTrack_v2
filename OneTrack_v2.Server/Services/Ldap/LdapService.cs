using DataModel.Response;
using OneTrak_v2.Services.Model;
using System.DirectoryServices.AccountManagement;
using System.Net.NetworkInformation;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.Net;
using OneTrack_v2.DbData;

namespace OneTrak_v2.Services
{
    public class LdapService : ILdapService
    {
        private readonly IConfiguration _config;
        private readonly AppDataContext _db;
        private readonly string? _groupNameRoleAdmin = string.Empty;
        private readonly string? _groupNameRoleTech = string.Empty;
        private readonly string? _groupNameRoleRead = string.Empty;
        private readonly string? _groupNameRoleQA = string.Empty;
        private readonly string? _groupNameDevUser = string.Empty;

        public LdapService(AppDataContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
            string environment = _config.GetValue<string>("Environment") ?? "DVLP";

            string adminRolekey = $"EnvironmentSettings:{environment}:AdminRole";
            string techRolekey = $"EnvironmentSettings:{environment}:TechRole";
            string readRolekey = $"EnvironmentSettings:{environment}:ReadRole";
            string qaRolekey = $"EnvironmentSettings:{environment}:QARole";
            string dvlpRolekey = $"EnvironmentSettings:{environment}:DvlpRole";

            _groupNameRoleAdmin = _config.GetValue<string>(adminRolekey);
            _groupNameRoleTech = _config.GetValue<string>(techRolekey);
            _groupNameRoleRead = _config.GetValue<string>(readRolekey);
            _groupNameRoleQA = _config.GetValue<string>(qaRolekey);
            _groupNameDevUser = _config.GetValue<string>(dvlpRolekey);

        }

        public ReturnResult GetUserAcctInfo(string vUserName, string vPassWord) 
        {
            string ldapServer = "corp.fin";
            //int ldapPort = 636;
            string ldapDomain = "DC=corp,DC=fin";

            UserAcctInfo userAccount = new UserAcctInfo();
            ReturnResult retResult = new ReturnResult();
            retResult.Success = true;
            retResult.ObjData = null;
            retResult.ErrMessage = null;

            try
			{
                // Set up the domain context with credentials
                using (var pc = new PrincipalContext(ContextType.Domain, ldapServer, ldapDomain, vUserName, vPassWord))
                {
                    // Validate the credentials
                    bool isValid = pc.ValidateCredentials(vUserName, vPassWord);
                    bool isAuthorized = false;

                    if (isValid)
                    {
                        // If credentials are valid, find the user to get the account name
                        UserPrincipal user = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, vUserName);
                        if (user != null)
                        {
                            userAccount.SOEID = user.SamAccountName;  // Return the SAM account name
                            userAccount.DisplayName = user.DisplayName;
                            userAccount.Email = user.EmailAddress;
                            userAccount.Enabled = user.Enabled;
                            userAccount.EmployeeId = user.EmployeeId;
                            userAccount.HomeDirectory = user.HomeDirectory;
                            userAccount.LastLogon = user.LastLogon;
                            userAccount.LicenseTechID = GetLicenseTechID(user.SamAccountName);

                            var userGroups = user.GetAuthorizationGroups();
                            foreach (GroupPrincipal group in userGroups)
                            {
                                if (new[] { _groupNameRoleAdmin, _groupNameRoleTech, _groupNameRoleRead, _groupNameRoleQA, _groupNameDevUser }.Contains(group.Name))
                                {
                                    if (group.Name == _groupNameRoleAdmin)
                                    {
                                        userAccount.IsAdminRole = true;
                                        isAuthorized = true;
                                    }
                                    else if (group.Name == _groupNameRoleTech)
                                    {
                                        userAccount.IsTechRole = true;
                                        isAuthorized = true;
                                    }
                                    else if (group.Name == _groupNameRoleRead)
                                    {
                                        userAccount.IsReadRole = true;
                                        isAuthorized = true;
                                    }
                                    else if (group.Name == _groupNameRoleQA)
                                    {
                                        userAccount.IsQARole = true;
                                        isAuthorized = true;
                                    }
                                    else if (group.Name == _groupNameDevUser)
                                    {
                                        userAccount.IsSuperUser = true;
                                        isAuthorized = true;
                                    }
                                }
                            }

                            if (!isAuthorized)
                            {
                                retResult.StatusCode = 403;
                                retResult.ErrMessage = "User not authorized";
                            }
                            else
                            {
                                retResult.StatusCode = 200;
                                retResult.ObjData = userAccount;
                            }
                        }
                        else
                        {
                            retResult.StatusCode = 403;
                            retResult.ErrMessage = "User not found";
                        }
                    }
                    else
                    {
                        retResult.StatusCode = 401;
                        retResult.ErrMessage = "Invalid credentials";
                    }
                }
            }
			catch (Exception ex)
			{
                retResult.Success = false;
                retResult.StatusCode = 500;
                retResult.ErrMessage = ex.Message;

            }

            return retResult;
        }
        private int GetLicenseTechID(string vSOEID)
        {
            var licenseTechID = (from l in _db.LicenseTeches
                                 where l.Soeid == vSOEID
                                 select l.LicenseTechId).FirstOrDefault();

            return licenseTechID;
        }
        protected bool PingServer(string serverName)
        {
            try
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(serverName);

                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log exception message or do something with it
                return false;
            }
        }
    }
}
