namespace OneTrak_v2.DataModel
{
    public class OputCompanyRequirement
    {
        public int CompanyRequirementId { get; set; }
        public string? WorkStateAbv { get; set; }
        public string? ResStateAbv { get; set; }
        public string? RequirementType { get; set; }
        public bool LicLevel1 { get; set; }
        public bool LicLevel2 { get; set; }
        public bool LicLevel3 { get; set; }
        public bool LicLevel4 { get; set; }
        public DateTime StartAfterDate { get; set; }
        public string? Document { get; set; }
    }
}
