namespace OneTrak_v2.DataModel
{
    public class IputUpsertDiaryItem
    {
        public int? EmploymentID { get; set; }
        public int DiaryID { get; set; }
        public string? DiaryName { get; set; }
        public DateTime? DiaryDate { get; set; }
        public string? SOEID { get; set; }
        public string? Notes { get; set; }
        public string? UserSOEID { get; set; }
    }
}
