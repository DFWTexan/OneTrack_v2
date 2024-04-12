using DataModel.Response;
using OneTrak_v2.Services.Model;
using System.DirectoryServices.AccountManagement;

namespace OneTrak_v2.Services
{
    public class LdapService : ILdapService
    {
        public ReturnResult GetUserAccount(string vUserName, string vPassWord) 
        {
            string ldapServer = "ldapServername:389";
            string ldapDomain = "CORP";
            string ldapContainer = "OU=Users,DC=domain,DC=com";
            
           UserAccount userAccount = new UserAccount();
            ReturnResult retResult = new ReturnResult();
            try
			{
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, ldapServer, ldapContainer))
                {
                    if (pc.ValidateCredentials(vUserName, vPassWord))
                    {
                        userAccount = new UserAccount
                        {
                            DisplayName = UserPrincipal.FindByIdentity(pc, vUserName).DisplayName,
                            UserSamAcctName = UserPrincipal.FindByIdentity(pc, vUserName).SamAccountName,
                            Email = UserPrincipal.FindByIdentity(pc, vUserName).EmailAddress
                        };
                    }
                }
                // Create PrincipalContext with LDAP connection
                //using (var context = new PrincipalContext(ContextType.Domain, ldapServer, ldapDomain, vUserName, vPassWord))
                //{
                //    // Create UserPrincipal object for searching
                //    using (var userPrincipal = new UserPrincipal(context))
                //    {
                //        // Set search criteria (e.g., search for all users whose name starts with "ram")
                //        userPrincipal.GivenName = "ram*";

                //        // Create PrincipalSearcher to perform the search
                //        using (var searcher = new PrincipalSearcher(userPrincipal))
                //        {
                //            // Perform the search and iterate over the results
                //            foreach (var result in searcher.FindAll())
                //            {
                //                if (result is UserPrincipal user)
                //                {
                //                    // Access user properties
                //                    //Console.WriteLine("Name: " + user.DisplayName);
                //                    //Console.WriteLine("Username: " + user.SamAccountName);
                //                    //Console.WriteLine("Email: " + user.EmailAddress);
                //                    //Console.WriteLine("------------------------------------");
                //                    userAccount.DisplayName = user.DisplayName;
                //                    userAccount.UserSamAcctName = user.SamAccountName;
                //                    userAccount.Email = user.EmailAddress;

                //                }
                //            }
                //        }
                //    }
                //}

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
