using DataModel.Response;
using OneTrak_v2.Services.Model;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.Net;

namespace OneTrak_v2.Services
{
    public class LdapService : ILdapService
    {
        private readonly string _groupNameRoleAdmin = "RG_OMS_LICEDIT_ADMIN";
        private readonly string _groupNameRoleTech = "RG_OMS_LICEDIT_FULL";
        private readonly string _groupNameRoleRead = "RG_OMS_LICEDIT_RO";
        private readonly string _groupNameDevUser = "RG_OMS_DEVELOPER";

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

                            var userGroups = user.GetAuthorizationGroups();
                            foreach (GroupPrincipal group in userGroups)
                            {
                                if (group.Name == _groupNameRoleAdmin)
                                {
                                    userAccount.IsAdminRole = true;
                                }
                                else if (group.Name == _groupNameRoleTech)
                                {
                                    userAccount.IsTechRole = true;
                                }
                                else if (group.Name == _groupNameRoleRead)
                                {
                                    userAccount.IsReadRole = true;
                                }
                                else if (group.Name == _groupNameDevUser)
                                {
                                    userAccount.IsSuperUser = true;
                                }
                            }

                            retResult.StatusCode = 200;
                            retResult.ObjData = userAccount;
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
    }
}
