namespace OneTrak_v2.Server.DbData.DataModel.LincenseInfo
{
    public class IputUpsertLicenseAppointment
    {
        public int EmployeeID { get; set; } 
        public int EmployeeAppointmentID { get; set; }
        public int? EmployeeLicenseID { get; set; }
        public string AppointmentStatus { get; set; } = string.Empty;
        public int? CompanyID { get; set; }
        public DateTime? CarrierDate { get; set; }
        public DateTime? AppointmentEffectiveDate { get; set; }
        public DateTime? AppointmentExpireDate { get; set; }
        public DateTime? AppointmentTerminationDate { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }
}
