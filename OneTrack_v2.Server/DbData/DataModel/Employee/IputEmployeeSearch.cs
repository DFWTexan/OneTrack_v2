namespace OneTrack_v2.DataModel
{
    public class IputEmployeeSearch
    {
        //public int CompanyID { get; set; } = 0;
        public string? EmployeeSSN { get; set; } = null;
        public string? GEID { get; set; } = null;
        public string? SCORENumber { get; set; } = null;
        public int? NationalProducerNumber { get; set; } = 0;
        public string? LastName { get; set; } = null;
        public string? FirstName { get; set; } = null;
        public List<string>? AgentStatus { get; set; } = [];
        public string? ResState { get; set; } = null;
        public string? WrkState { get; set; } = null;
        public string? BranchCode { get; set; } = null;
        public int? EmployeeLicenseID { get; set; }
        public List<string>? LicStatus { get; set; } = null;
        public string? LicState { get; set; } = null;
        public string? LicenseName { get; set; } = null;
        //public int EmploymentID { get; set; } = 0;

        public IputEmployeeSearch()
        {
            AgentStatus = new List<string>();
        }
    }
}
