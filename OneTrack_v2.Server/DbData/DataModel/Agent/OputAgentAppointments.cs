namespace OneTrack_v2.DataModel
{
    public class OputAgentAppointments
    {
        public int LicenseID { get; set; }
        public int EmployeeAppointmentID { get; set; }
        public string? LicenseState { get; set; }
        public string? LineOfAuthority { get; set; }
        public DateTime? AppointmentEffectiveDate { get; set; }
        public string? AppointmentStatus { get; set; }
        public int EmployeeLicenseID { get; set; }
        public DateTime? CarrierDate { get; set; }
        public DateTime? AppointmentExpireDate { get; set; }
        public DateTime? AppointmentTerminationDate { get; set; }
        public int? CompanyID { get; set; }
        public string? CompanyAbv { get; set; }
        public DateTime? RetentionDate { get; set; }
    }
}
