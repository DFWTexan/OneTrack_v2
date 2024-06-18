namespace OneTrak_v2.DataModel
{
    public class IputAgentDetail
    {
        public string? EmployeeSSN { get; set; } = "";
        public int EmployeeID { get; set; } = 0;
        public string? LastName { get; set; } = "";
        public string? FirstName { get; set; } = "";
        public string? MiddleName { get; set; } = "";
        public string? SOEID { get; set; } = "";
        public DateTime? DateOfBirth { get; set; } 
        public int? NationalProducerNumber { get; set; } = 0;
        public string? GEID { get; set; } = "";
        public string? Alias { get; set; } = "";
        public bool ExcludeFromRpts { get; set; } = false;

        public string? Address1 { get; set; } = "";
        public string? Address2 { get; set; } = "";
        public string? City { get; set; } = "";
        public string? State { get; set; } = "";
        public string? Zip { get; set; } = "";
        public string? Phone { get; set; } = "";
        public string? Fax { get; set; } = "";

        public int EmploymentID { get; set; } = 0;
        public string? Email { get; set; } = "";
        public string? WorkPhone { get; set; } = "";
        public string? EmployeeStatus { get; set; } = "";
        public int? CompanyID { get; set; } = 0;
        public bool? CERequired { get; set; } = false;
        public string? LicenseLevel { get; set; } = "";
        public string? LicenseIncentive { get; set; } = "";

        public bool SecondChance { get; set; } = false;
        public string? UserSOEID { get; set; }
    }

}
