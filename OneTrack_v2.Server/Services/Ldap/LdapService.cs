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
        public ReturnResult GetUserAccount(string vUserName, string vPassWord) 
        {
            string ldapServer = "corp.fin";
            //int ldapPort = 636;
            string ldapDomain = "DC=corp,DC=fin";

            UserAccount userAccount = new UserAccount();
            ReturnResult retResult = new ReturnResult();
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
                            userAccount.UserSamAcctName = user.SamAccountName;  // Return the SAM account name
                            userAccount.DisplayName = user.DisplayName;
                            userAccount.Email = user.EmailAddress;
                        }
                        else
                        {
                            throw new ApplicationException("User authenticated but cannot be found in directory.");
                        }
                    }
                    else
                    {
                        return null;  // Authentication failed, return null
                    }
                }

                retResult.ObjData = userAccount;
                retResult.Success = true;
                retResult.StatusCode = 200;
            }
			catch (Exception ex)
			{
                retResult.StatusCode = 500;
                retResult.ErrMessage = ex.Message;
            }

            return retResult;
        }
    }
}
