namespace OneTrak_v2.DataModel

{
    public class IputSendIncentiveEmail
    {
        public int EmployeeID { get; set; }
        public int EmploymentID { get; set; }
        public int EmployeeLicenseID { get; set; }
        public int? IncentiveID { get; set; }
        public string? TypeOfIncentive { get; set; } = string.Empty;
        public string? IncentiveEmailType { get; set; } = string.Empty;
        public int? CommunicationID { get; set; }
        public int? DMEmploymentID { get; set; }
        public int? CCdBMEmploymentID { get; set; }
        public int? CCd2BMEmploymentID { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }
}
