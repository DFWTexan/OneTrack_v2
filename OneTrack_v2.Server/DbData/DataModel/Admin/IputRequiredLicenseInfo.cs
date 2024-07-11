namespace OneTrak_v2.DataModel
{
    public class IputUpsertRequiredLicense
    {
        public int? RequiredLicenseID { get; set; }
        public string WorkStateAbv { get; set; } = string.Empty;
        public string ResStateAbv { get; set; } = string.Empty;
        public int LicenseID { get; set; }
        public string? BranchCode { get; set; }
        public string? RequirementType { get; set; }
        public bool LicLevel1 { get; set; }
        public bool LicLevel2 { get; set; }
        public bool LicLevel3 { get; set; }
        public bool LicLevel4 { get; set; }
        public bool PLS_Incentive1 { get; set; }
        public bool Incentive2_Plus { get; set; }
        public bool LicIncentive3 { get; set; }
        public string? StartDocument { get; set; }
        public string? RenewalDocument { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }

    public class IputDeleteRequiredLicense
    {
        public int? RequiredLicenseID { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }
}
