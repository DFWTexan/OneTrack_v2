using DataModel.Response;
using OneTrak_v2.Services.Model;

namespace OneTrak_v2.Services
{
    public interface ILdapService
    {
        public ReturnResult GetUserAccount(string vUserName, string vPassWord);
    }
}
