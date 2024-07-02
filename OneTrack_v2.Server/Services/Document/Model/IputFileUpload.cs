namespace OneTrak_v2.Document.Model
{
    public class IputFileUploadDelete
    {
        public string? FilePathUri { get; set; } = null;
        public IFormFile? File { get; set; } = null;
        public string? FileName { get; set; } = null;
    }
}
