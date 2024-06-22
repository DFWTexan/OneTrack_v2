namespace OneTrak_v2.DataModel
{
    public class IputSendEmail
    {
        public int? EmailTemplateID { get; set; }
        public string? EmailTo { get; set; }
        public List<string>? CcEmail { get; set; }
        public string? Subject { get; set; }
        public string? EmailContent { get; set; }
        public string? UserSOEID { get; set; }
    }
}
