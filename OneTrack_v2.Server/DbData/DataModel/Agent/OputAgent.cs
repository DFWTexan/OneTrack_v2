using OneTrak_v2.DataModel;
using System.Security.Cryptography.X509Certificates;

namespace OneTrack_v2.DataModel
{
    public class OputAgent
    {
        public int EmployeeID { get; set; }
        public int EmploymentID { get; set; }
        public string? GEID { get; set; }
        public string? EmployeeStatus { get; set; }
        public int? CompanyID { get; set; }
        public string? CompanyName { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Suffix { get; set; }
        public string? Alias { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? JobDate { get; set; }
        public string? EmployeeSSN { get; set; }
        public string? Soeid { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? HomePhone { get; set; }
        public string? WorkPhone { get; set; }
        public string? FaxPhone { get; set; }
        public string? Email { get; set; }
        public int? NationalProdercerNumber { get; set; }   
        public bool? CERequired { get; set; }
        public bool? ExcludeFromReports { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? DiarySOEID { get; set; }
        public string? DiaryEntryName { get; set; }
        public DateTime? DiaryEntryDate { get; set; }
        public string? DiaryNotes { get; set; }
        public string? DiaryTechName { get; set; }
        public string? LicenseIncentive { get; set; }
        public string? LicenseLevel { get; set; }
        public bool? IsLicenseincentiveSecondChance { get; set; }
        public string? BranchCode { get; set; }
        public string? BranchDeptScoreNumber { get; set; }
        public string? BranchDeptNumber { get; set; }
        public string? BranchDeptName { get; set; } 
        public string? BranchDeptStreet1 { get; set; }
        public string? BranchDeptStreet2 { get; set; }
        public string? BranchDeptStreetCity { get; set; }
        public string? BranchDeptStreetState { get; set; }
        public string? BranchDeptStreetZip { get; set; }
        public string? BranchDeptPhone { get; set; }
        public string? BranchDeptFax { get; set; }
        public string? BranchDeptEmail { get; set; }
        public string? WorkStateAbv { get; set; }
        public string? ResStateAbv { get; set; } 

        public List<OputAgentHiearchy> MgrHiearchy { get; set; }
        public List<OputAgentLicenseAppointments> AgentLicenseAppointments { get; set; }
        public List<AgentEmploymentItem> EmploymentHistory { get; set; }
        public List<AgentTransferItem> TransferHistory { get; set; }
        public List<CompayRequirementsItem> CompayRequirementsHistory { get; set; }
        public List<EmploymentJobTitleItem> EmploymentJobTitleHistory { get; set; }
        public List<AgentContEduRequiredItem> ContEduRequiredItems { get; set; }
        public List<AgentContEduCompletedItem> ContEduCompletedItems { get; set; }
        public List<DiaryCreatedByItem> DiaryCreatedByItems { get; set; }
        public List<DiaryItem> DiaryItems { get; set; }
        public List<EmploymentCommunicationItem> EmploymentCommunicationItems { get; set; }
        public List<OputAgentLicenses> LicenseItems { get; set; }
        public List<OputAgentAppointments> AppointmentItems { get; set; }
        public List<TicklerItem> TicklerItems { get; set; }
        public List<WorklistItem> WorklistItems { get; set; }

        public OputAgent()
        {
            MgrHiearchy = new List<OputAgentHiearchy>();
            AgentLicenseAppointments = new List<OputAgentLicenseAppointments>();
            EmploymentHistory = new List<AgentEmploymentItem>();
            TransferHistory = new List<AgentTransferItem>();
            CompayRequirementsHistory = new List<CompayRequirementsItem>();
            EmploymentJobTitleHistory = new List<EmploymentJobTitleItem>();
            ContEduRequiredItems = new List<AgentContEduRequiredItem>();
            ContEduCompletedItems = new List<AgentContEduCompletedItem>();
            DiaryCreatedByItems = new List<DiaryCreatedByItem>();
            DiaryItems = new List<DiaryItem>();
            EmploymentCommunicationItems = new List<EmploymentCommunicationItem>();
            LicenseItems = new List<OputAgentLicenses>();
            AppointmentItems = new List<OputAgentAppointments>();
            TicklerItems = new List<TicklerItem>();
            WorklistItems = new List<WorklistItem>();
        }
    }

    public class AgentContEduRequiredItem
    {
        public int EmployeeEducationId { get; set; }
        public int ContEducationRequirementID { get; set; }
        public DateTime? EducationStartDate { get; set; }
        public DateTime? EducationEndDate { get; set; }
        public decimal? RequiredCreditHours { get; set; }
        public Boolean? IsExempt { get; set; }
        public int? EmploymentID { get; set; }
    }

    public class AgentContEduCompletedItem
    {
        public int EmployeeEducationID { get; set; }
        public string? EducationName { get; set; }
        public int? ContEducationRequirementID { get; set; }
        public DateTime? ContEducationTakenDate { get; set; }
        public decimal? CreditHoursTaken { get; set; }
        public string? AdditionalNotes { get; set; }
    }

    public class  DiaryCreatedByItem
    {
        public string? SOEID { get; set; }
        public string? TechName { get; set; }
    }

    public class DiaryItem
    {
        public int DiaryID { get; set; }
        public string? SOEID { get; set; }
        public string? TechName { get; set; }
        public string? DiaryName { get; set; }
        public DateTime? DiaryDate { get; set; }
        public string? Notes { get; set; }
        //public string? EmploymentID { get; set; }
    }

    public class EmploymentCommunicationItem
    {
        public int EmploymentCommunicationID { get; set; }
        public string? LetterName { get; set; }
        public DateTime? EmailCreateDate { get; set; }
        public DateTime? EmailSentDate { get; set; }
    }

    public class TicklerItem {         
        public int TicklerID { get; set; }
        public int? LicenseTechID { get; set; }
        public int? EmploymentID { get; set; }
        public int? EmployeeLicenseID { get; set;}
        public string? TicklerMessage { get; set; }
        public DateTime? TicklerDueDate { get; set; }
    }

    public class WorklistItem
    {
        public int? WorkListDataID { get; set; }
        public string? WorkListName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? LicenseTech { get; set; }
    }
}
