namespace OneTrack_v2.DataModel
{
    public class OputAgentEmploymentTranserHistory
    {
        public List<AgentEmploymentItem> AgentEmploymentItems { get; set; }
        public List<AgentTransferItem> AgentTransferItems { get; set; }
        public List<CompayRequirementsItem> CompayRequirementsItems { get; set; }
        public List<EmploymentJobTitleItem> EmploymentJobTitleItems { get; set; }

        public OputAgentEmploymentTranserHistory()
        {
            AgentEmploymentItems = new List<AgentEmploymentItem>();
            AgentTransferItems = new List<AgentTransferItem>();
            CompayRequirementsItems = new List<CompayRequirementsItem>();
            EmploymentJobTitleItems = new List<EmploymentJobTitleItem>();
        }
    }

   public class AgentEmploymentItem
    {
        public int EmploymentHistoryID { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? RehireDate { get; set; }
        public DateTime? NotifiedTermDate { get; set; }
        public DateTime? HRTermDate { get; set; }
        public string? HRTermCode { get; set; }
        public bool? IsForCause { get; set; }
        public string? BackgroundCheckStatus { get; set; }
        public string? BackGroundCheckNotes { get; set; }
        public bool? IsCurrent { get; set; }
    }
   public class AgentTransferItem
   {
        public int TransferHistoryID { get; set; }
        public string? BranchCode { get; set; }
        public string? WorkStateAbv { get; set; }
        public string? ResStateAbv { get; set; }
        public DateTime? TransferDate { get; set; }
        public string? State { get; set; }
        public bool? IsCurrent { get; set; }
   }
   public class CompayRequirementsItem
    {
        public int EmploymentCompanyRequirementID { get; set; }
        public string? AssetIdString { get; set; }
        public string? LearningProgramStatus { get; set; }
        public DateTime? LearningProgramEnrollmentDate { get; set; }
        public DateTime? LearningProgramCompletionDate { get; set; }
   }
   public class  EmploymentJobTitleItem
   {
        public int EmploymentJobTitleID { get; set; }
        public int EmploymentID { get; set; }
        public DateTime? JobTitleDate { get; set; }
        public string? JobCode { get; set; }
        public string? JobTitle { get; set; }
        public bool? IsCurrent { get; set; }
   }
}
