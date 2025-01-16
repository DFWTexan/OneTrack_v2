namespace OneTrak_v2.Services.Model
{
    public class NameChangeInfo
    {
        public int? EmployeeID { get; set; }
        public int? EmploymentID { get; set; }

        public string? TMName { get; set; }
        public string? TMNumber { get; set; }
        public string? TMTitle { get; set; }
        public string? TMEmail { get; set; }

        public string? LicTechName { get; set; }
        public string? LicTechPhone { get; set; }
        public string? LicTechTitle { get; set; }
                
        public string? NameOld { get; set; }
        public string? NameNew { get; set; }
        public string? ChangeDate { get; set; }
    }
}
