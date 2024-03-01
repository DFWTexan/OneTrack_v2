using OneTrak_v2.DataModel;

namespace OneTrack_v2.DataModel
{
    public class OputAgent
    {
        public int EmployeeID { get; set; }
        public int EmploymentID { get; set; }
        public string? EmployeeStatus { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? JobDate { get; set; }
        public string? EmployeeSSN { get; set; }
        public string? Soeid { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? NationalProdercerNumber { get; set; }   
        public bool? CERequired { get; set; }
        public bool? ExcludeFromReports { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? DiaryEntryName { get; set; }
        public DateTime? DiaryEntryDate { get; set; }
        public string? DiaryEntryDescription { get; set; }
        public string? LicenseIncentive { get; set; }
        public string? LicenseLevel { get; set; }
        public bool? IsLicenseincentiveSecondChance { get; set; }
        
        public int? BranchDeptScoreNumber { get; set; }
        public int? BranchDeptNumber { get; set; }
        public string? BranchDeptName { get; set; } 
        public string? BranchDeptStreet1 { get; set; }
        public string? BranchDeptStreet2 { get; set; }
        public string? BranchDeptStreet3 { get; set; }
        public string? BranchDeptStreetCity { get; set; }
        public string? BranchDeptStreetState { get; set; }
        public string? BranchDeptStreetZip { get; set; }

        public List<OputAgentHiearchy> MgrHiearchy { get; set; }
        public List<OputAgentLicenseAppointments> AgentLicenseAppointments { get; set; }

        public OputAgent()
        {
            MgrHiearchy = new List<OputAgentHiearchy>();
            AgentLicenseAppointments = new List<OputAgentLicenseAppointments>();
        }
    }
}
