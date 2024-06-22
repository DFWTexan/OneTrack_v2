namespace OneTrak_v2.DataModel
{
    public class IputSendEmail
    {
        public string? EmailTo { get; set; }
        public List<string>? CcEmail { get; set; }
        public string? Subject { get; set; }
        public string? HeaderContent { get; set; }
        public string? BodyContent { get; set; }
        public string? FooterContent { get; set; }
        public int? EmailTemplateID { get; set; }
        public string? EmailBody { get; set; }
        public string? UserSOEID { get; set; }
    }
}
