namespace OneTrak_v2.DataModel
{
    public class IputUpsertExam
    {
        public int? ExamID { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public decimal? ExamFees { get; set; }
        public int? ExamProviderID { get; set; }
        public string? StateProvinceAbv { get; set; }
        public string? DeliveryMethod { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }
}
