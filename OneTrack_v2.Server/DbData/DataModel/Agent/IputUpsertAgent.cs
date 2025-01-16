namespace OneTrak_v2.DataModel
{
    public class IputUpsertAgent
    {
        public int EmployeeID { get; set; } = 0;
        public int EmploymentID { get; set; } = 0;
        public string EmployeeSSN { get; set; } = string.Empty;
        // Employee
        public int NationalProducerNumber { get; set; } = 0;
        public string GEID { get; set; } = string.Empty;
        public string Alias { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string SOEID { get; set; } = string.Empty;
        public bool ExcludeFromRpts { get; set; } = false;
        public bool CERequired { get; set; } = false;
        // Employment
        public string EmployeeStatus { get; set; } = string.Empty;
        public int CompanyID { get; set; } = 0;
        public string WorkPhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string LicenseIncentive { get; set; } = string.Empty;
        public string LicenseLevel { get; set; } = string.Empty;
        public bool SecondChance { get; set; } = false;
        // Address
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        // EmploymentHistory
        public DateTime HireDate { get; set; } = DateTime.MinValue;
        public string UsrSOEID { get; set; } = string.Empty;
        public string BackgroundCheckStatus { get; set; } = string.Empty;
        public string BackgroundCheckNote { get; set; } = string.Empty;
        // TransferHistor
        public string BranchCode { get; set; } = string.Empty;
        public string WorkStateAbv { get; set; } = string.Empty;
        public string ResStateAbv { get; set; } = string.Empty;
        // EmploymentJobTitle
        public int JobTitleID { get; set; } = 0;
        public DateTime JobTitleDate { get; set; } = DateTime.MinValue;

        public string UserSOEID { get; set; } = string.Empty;
    }
}
