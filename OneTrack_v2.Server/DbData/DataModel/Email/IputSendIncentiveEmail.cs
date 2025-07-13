namespace OneTrak_v2.DataModel

{
    public class IputSendIncentiveEmail
    {
        public int EmployeeID { get; set; }
        public int EmploymentID { get; set; }
        public int EmployeeLicenseID { get; set; }
        public int CommunicationID { get; set; }
        public int? DMEmploymentID { get; set; }
        public int? CCdBMEmploymentID { get; set; }
        public int? CCd2BMEmploymentID { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }
}
