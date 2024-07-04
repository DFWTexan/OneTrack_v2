namespace OneTrak_v2.DataModel
{
    public class IputUpsertEducationRule
    {
        public int RuleNumber { get; set; }
        public string StateProvince { get; set; } = string.Empty;
        public string LicenseType { get; set; } = string.Empty;
        public int EducationStartDateID { get; set; }
        public int EducationEndDateID { get; set; }
        public int RequiredCreditHours { get; set; }
        public string ExceptionID { get; set; } = string.Empty;
        public string ExemptionID { get; set; } = string.Empty;
        public string UserSOEID { get; set; }
    }
}
