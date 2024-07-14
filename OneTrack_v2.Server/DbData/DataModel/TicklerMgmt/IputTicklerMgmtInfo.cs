namespace OneTrak_v2.DataModel
{
    public class IputUpsertTicklerMgmt
    {
        public int? TicklerID { get; set; }
        public string? LkpValue { get; set; }
        public string? Message { get; set; }
        public DateTime? TicklerDate { get; set; }
        public DateTime? TicklerDueDate { get; set; }
        public int? LicenseTechID { get; set; }
        public int? EmploymentID { get; set; }
        public int? EmployeeLicenseID { get; set; }
        public DateTime? TicklerCloseDate { get; set; }
        public int? TicklerCloseByLicenseTechID { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }

    public class IputCloseTicklerMgmt
    {
        public int TicklerID { get; set; }
        public int TicklerCloseByLicenseTechID { get; set; }
        public string? UserSOEID { get; set; }
    }   

    public class IputDeleteTicklerMgmt
    {
        public int TicklerID { get; set; }
        public string? UserSOEID { get; set; }
    }
}
