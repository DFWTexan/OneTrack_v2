namespace OneTrak_v2.DataModel
{
    public class OputEmailComTemplate
    {
        public int CommunicationID { get; set; }
        public required string CommunicationName { get; set; }
        public required string DocType { get; set; }
        public required string DocTypeAbv { get; set; }
        public required string DocSubType { get; set; }
        public required string EmailAttachments { get; set; }
        public bool HasNote { get; set; }
        public required string DocTypeDocSubType { get; set; }
    }
}
