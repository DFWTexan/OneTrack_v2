namespace OneTrak_v2.DataModel
{
    public class IputUpsertAgentLicense
    {
        public int EmployeeID { get; set; }
        public int EmploymentID { get; set; }
        public int EmployeeLicenseID { get; set; }
        public int? AscEmployeeLicenseID { get; set; }
        public int? LicenseID { get; set; }
        public DateTime? LicenseExpireDate { get; set; }
        public string? LicenseStatus { get; set; }
        public string? LicenseNumber { get; set; }
        public bool? Reinstatement { get; set; }
        public bool? Required { get; set; }
        public bool? NonResident { get; set; }
        public DateTime? LicenseEffectiveDate { get; set; }
        public DateTime? LicenseIssueDate { get; set; }
        public DateTime? LineOfAuthorityIssueDate { get; set; }
        public string? LicenseNote { get; set; }
        public string? AppointmentStatus { get; set; }
        public string? CompanyID { get; set; }
        public DateTime? CarrierDate { get; set; }
        public DateTime? AppointmentEffectiveDate { get; set; }
        public DateTime? AppointmentExpireDate { get; set; }
        public DateTime? AppointmentTerminationDate { get; set; }
        public string UserSOEID { get; set; }
    }
}
