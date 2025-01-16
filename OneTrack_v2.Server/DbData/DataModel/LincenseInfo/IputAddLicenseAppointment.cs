namespace OneTrak_v2.DataModel
{
    public class IputAddLicenseAppointment
    {
        public int EmployeeID { get; set; }
        public int EmployeeLicenseID { get; set; }
        public string? AppointmentStatus { get; set; } = null;
        public int CompanyID { get; set; }
        public DateTime? CarrierDate { get; set; } = null;
        public DateTime? AppointmentEffectiveDate { get; set; } = null;
        public DateTime? AppointmentExpireDate { get; set; } = null;
        public DateTime? AppointmentTerminationDate { get; set; } = null;
        public string UserSOEID { get; set; }
    }
}
