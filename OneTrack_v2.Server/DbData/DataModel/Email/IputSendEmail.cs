namespace OneTrak_v2.DataModel
{
    public class IputSendEmail
    {
        public int EmployeeID { get; set; }
        public int EmploymentID { get; set; }
        public int CommunicationID { get; set; }
        public string? EmailTo { get; set; }
        public List<string>? CcEmail { get; set; }
        public string? EmailSubject { get; set; }
        public string? EmailContent { get; set; }
        //public string? EmailAttachment { get; set; }
        public List<IFormFile>? FileAttachments { get; set; } = new List<IFormFile>();
        public string? UserSOEID { get; set; }
    }
}
