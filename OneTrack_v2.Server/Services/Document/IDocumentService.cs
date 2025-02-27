using DataModel.Response;

namespace OneTrak_v2.Services
{
    public interface IDocumentService
    {
        Task<ReturnResult> Upload(Stream vStream, string vFileName, string vFilePathType);

        //Task<ReturnResult> Upload_v2(Stream vStream, OneTrak_v2.Document.Model.IputFileUploadDelete input);

        //Task<ReturnResult> Delete(OneTrak_v2.Document.Model.IputFileUploadDelete input);
    }
}
