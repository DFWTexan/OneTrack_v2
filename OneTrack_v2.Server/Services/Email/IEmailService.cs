using DataModel.Response;
using OneTrak_v2.Services.Model;

namespace OneTrack_v2.Services
{
    public interface IEmailService
    {
        public ReturnResult GetEmailTemplate(int vCommunicationID, int vEmploymentID);
        
    }
}
