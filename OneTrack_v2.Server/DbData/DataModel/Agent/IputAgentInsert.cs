using System.ComponentModel.DataAnnotations;

namespace OneTrack_v2.DataModel
{
    public class IputAgentInsert
    {
        public string EmployeeSSN { get; set; } = "";

        // Employee
        public int NationalProducerNumber { get; set; } = 0;
        public string GEID { get; set; } = "";
        public string Alias { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";
        [Required]
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        [Required]
        public string? PreferredName { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string SOEID { get; set; } = "";
        public bool ExcludeFromRpts { get; set; } = false;

        // Employment
        public string EmployeeStatus { get; set; } = "";
        public int CompanyID { get; set; } = 0;
        public string WorkPhone { get; set; } = "";
        public string Email { get; set; } = "";
        public string LicenseIncentive { get; set; } = "";
        public string LicenseLevel { get; set; } = "";

        // Address
        public string Address1 { get; set; } = "";
        public string Address2 { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string Zip { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Fax { get; set; } = "";

        // EmploymentHistory
        [Required]
        public DateTime HireDate { get; set; } = DateTime.MinValue;
        public string UserSOEID { get; set; }
        public string BackgroundCheckStatus { get; set; } = "";
        public string BackgroundCheckNote { get; set; } = "";

        // TransferHistory
        public string BranchCode { get; set; } = "";
        [Required]
        public string WorkStateAbv { get; set; } = "";
        [Required] 
        public string ResStateAbv { get; set; } = "";

        // EmploymentJobTitle
        public int JobTitleID { get; set; } = 0;
        public DateTime JobTitleDate { get; set; } = DateTime.MinValue;
    }
}
