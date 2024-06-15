namespace OneTrak_v2.DataModel
{
    public class IputUpdateLicenseIncentive
    {
        public int EmploymentLicenseIncentiveID { get; set; }

        public string? RollOutGroup { get; set; } = string.Empty;
        public int? DMEmploymentID { get; set; } = null;
        public int? CCdBMEmploymentID { get; set; } = null;
        public string? DMSentBySOEID { get; set; } = string.Empty;
        public DateTime? DMSentDate { get; set; } = null;
        public DateTime? DMApprovalDate { get; set; } = null;
        public DateTime? DMDeclinedDate { get; set; } = null;
        public DateTime? DM10DaySentDate { get; set; } = null;
        public string? DM10DaySentBySOEID { get; set; } = string.Empty;
        public DateTime? DM20DaySentDate { get; set; } = null;
        public string? DM20DaySentBySOEID { get; set; } = string.Empty;
        public string? DMComment { get; set; } = string.Empty;
        public string? TMSentBySOEID { get; set; } = string.Empty;
        public DateTime? TMSentDate { get; set; } = null;
        public string? CCd2BMEmploymentID { get; set; } = string.Empty;
        public DateTime? TMApprovalDate { get; set; } = null;
        public DateTime? TMDeclinedDate { get; set; } = null;
        public DateTime? TM10DaySentDate { get; set; } = null;
        public string? TM10DaySentBySOEID { get; set; } = string.Empty;
        public DateTime? TM45DaySentDate { get; set; } = null;
        public string? TM45DaySentBySOEID { get; set; } = string.Empty;
        public DateTime? TMExceptionDate { get; set; } = null;
        public string? TMException { get; set; } = string.Empty;
        public string? TMComment { get; set; } = string.Empty;
        public string? TMOkToSellSentBySOEID { get; set; } = string.Empty;
        public DateTime? TMOkToSellSentDate { get; set; } = null;
        public string? CCOkToSellBMEmploymentID { get; set; } = string.Empty;
        public DateTime? TMOMSApprtoSendToHRDate { get; set; } = null;
        public DateTime? TMSentToHRDate { get; set; } = null;
        public DateTime? IncetivePeriodDate { get; set; } = null;
        public string? IncentiveStatus { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;

        public string UserSOEID { get; set; }
    }
}
