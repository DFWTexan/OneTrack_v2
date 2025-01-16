namespace OneTrak_v2.DataModel
{
    public class IputUpdateLicenseAppointment
    {
        public int EmployeeAppointmentID { get; set; }
        public string AppointmentStatus { get; set; } = string.Empty;
        public int CompanyID { get; set; }
        public DateTime? CarrierDate { get; set; } = null;
        public DateTime? AppointmentEffectiveDate { get; set; } = null;
        public DateTime? AppointmentExpireDate { get; set; } = null;
        public DateTime? AppointmentTerminationDate { get; set; } = null;
        public string UserSOEID { get; set; }
    }
}
