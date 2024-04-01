namespace OneTrak_v2.DataModel
{
    public class OputEducationRule
    {
        public int RuleNumber { get; set; }
        public string? StateProvince { get; set; }
        public string? LicenseType { get; set; }
        public decimal RequiredCreditHours { get; set; }
        public int EducationStartDateID { get; set; }
        public string? EducationStartDate { get; set; }
        public int EducationEndDateID { get; set; }
        public string? EducationEndDate { get; set; }
        public string? ExceptionID { get; set; }
        public string? ExemptionID { get; set; }
        public bool IsActive { get; set; }

    }
}
