namespace OneTrak_v2.Document.Model
{
    public class IputFileUploadDelete
    {
        public string? FilePathUri { get; set; } = null;
        public required IFormFile? File { get; set; }
        public required string FileName { get; set; }
        public required string FileExt { get; set; }
    }
}
