namespace OneTrak_v2.DataModel
{
    public class OputAgentLicApplicationInfo
    {
        public List<AgentLicApplicationItem> LicenseApplicationItems { get; set; }
        public List<AgentLicPreEducationItem> LicensePreEducationItems { get; set; }
        public List<AgentLicPreExamItem> LicensePreExamItems { get; set; }
        public List<AgentLicRenewalItem> LicenseRenewalItems { get; set; }
        public OputAgentLicApplicationInfo()
        {
            LicenseApplicationItems = new List<AgentLicApplicationItem>();
            LicensePreEducationItems = new List<AgentLicPreEducationItem>();
            LicensePreExamItems = new List<AgentLicPreExamItem>();
            LicenseRenewalItems = new List<AgentLicRenewalItem>();
        }
    }

    public class AgentLicApplicationItem
    {
        public int LicenseApplicationID { get; set; }
        public DateTime? SentToAgentDate { get; set; }
        public DateTime? RecFromAgentDate { get; set; }
        public DateTime? SentToStateDate { get; set; }
        public DateTime? RecFromStateDate { get; set; }
        public string? ApplicationStatus { get; set; }
        public string? ApplicationType { get; set; }
    }

    public class AgentLicPreEducationItem
    {
        public int EmployeeLicensePreEducationID { get; set; }
        public string? Status { get; set; }
        public DateTime? EducationStartDate { get; set; }
        public DateTime? EducationEndDate { get; set; }
        public int? PreEducationID { get; set; }
        public int? CompanyID { get; set; }
        public string? EducationName { get; set; }
        public int? EmployeeLicenseID { get; set; }
        public string? AdditionalNotes { get; set; }
    }

     public class AgentLicPreExamItem
    {
        public int EmployeeLicensePreExamID { get; set; }
        public int? EmployeeLicenseID { get; set; }
        public string? Status { get; set; }
        public int? ExamID { get; set; }
        public string? ExamName { get; set; }   
        public DateTime? ExamScheduleDate { get; set; }
        public DateTime? ExamTakenDate { get; set; }
        public string? AdditionalNotes { get; set; }
    }

    public class AgentLicRenewalItem
    {
        public int EmployeeLicenseID { get; set; }
        public int? LicenseApplicationID { get; set; }
        public DateTime? SentToAgentDate { get; set; }
        public DateTime? RecFromAgentDate { get; set; }
        public DateTime? SentToStateDate { get; set; }
        public DateTime? RecFromStateDate { get; set; }
        public string? ApplicationStatus { get; set; }
        public string? ApplicationType { get; set; }
        public DateTime? RenewalDate { get; set; }
        public string? RenewalMethod { get; set; }
    }
    
}
