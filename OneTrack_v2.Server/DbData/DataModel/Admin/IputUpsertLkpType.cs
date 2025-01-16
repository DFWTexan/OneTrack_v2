namespace OneTrak_v2.DataModel
{
    public class IputUpsertLkpType
    {
        public string? UpsertType { get; set; }
        public string? LkpField { get; set; }
        public string? LkpValue { get; set; }
        public int SortOrder { get; set; }
        public string? UserSOEID { get; set; }
    }
}
