using DataModel.Response;

namespace OneTrak_v2.Services
{
    public interface IWorklistService
    {
        public ReturnResult GetWorklistData(string? vWorklistName = null, string? vWorklistDate = null, string? vLicenseTech = null);
    }
}
