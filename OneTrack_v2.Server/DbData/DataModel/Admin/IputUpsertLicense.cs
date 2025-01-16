namespace OneTrak_v2.DataModel
{
    public class IputUpsertLicense
    {
        public int? LicenseID { get; set; }
        public string? LicenseName { get; set; }
        public string? LicenseAbv { get; set; }
        public decimal? LicenseFees { get; set; }
        public string? StateProvinceAbv { get; set; }
        public decimal? AppointmentFees { get; set; }
        public int? LineOfAuthorityID { get; set; }
        public decimal? PLS_Incentive1TMPay { get; set; }
        public decimal? PLS_Incentive1MRPay { get; set; }
        public decimal? Incentive2_PlusTMPay { get; set; }
        public decimal? Incentive2_PlusMRPay { get; set; }
        public decimal? LicIncentive3TMPay { get; set; }
        public decimal? LicIncentive3MRPay { get; set; }
        public bool? IsActive { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }
}
