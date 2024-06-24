namespace OneTrak_v2.DataModel
{
    public class IputSendEmail
    {
        public string EmployeeID { get; set; }
        public string EmploymentID { get; set; }
        public int CommunicationID { get; set; }
        public string? EmailTo { get; set; }
        public List<string>? CcEmail { get; set; }
        public string? EmailSubject { get; set; }
        public string? EmailContent { get; set; }
        public IFormFile? Attachment { get; set; } // Changed to IFormFile to accept file uploads
        public string? UserSOEID { get; set; }
    }
}
