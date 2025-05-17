namespace OneTrak_v2.DataModel
{
    public class IputUpsertCompanyRequirement
    {
        public int? CompanyRequirementId { get; set; }
        public string? WorkStateAbv { get; set; }
        public string? ResStateAbv { get; set; }
        public string? RequirementType { get; set; }
        public bool LicLevel1 { get; set; }
        public bool LicLevel2 { get; set; }
        public bool LicLevel3 { get; set; }
        public bool LicLevel4 { get; set; }
        public string? StartAfterDate { get; set; }
        public string? Document { get; set; }
        public string? UserSOEID { get; set; }
    }
}
