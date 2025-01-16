namespace OneTrak_v2.DataModel
{
    public class IputUpsertPreEducation
    {
        public int? PreEducationID { get; set; }
        public string EducationName { get; set; } = string.Empty;
        public short? CreditHours { get; set; }
        public string? DeliveryMethod { get; set; }
        public decimal? Fees { get; set; }
        public int? EducationProviderID { get; set; }
        public string? StateProvinceAbv { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }

    public class IputDeletePreEducation
    {
        public int PreEducationID { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }
}
