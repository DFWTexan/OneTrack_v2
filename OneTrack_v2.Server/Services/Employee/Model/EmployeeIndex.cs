namespace OneTrak_v2.Services.Employee.Model
{
    public class EmployeeIndex
    {
        public int? EmployeeID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Geid { get; set; }
        public string? WorkState { get; set; }
        public string? LicenseState { get; set; }
        public string? BranchName { get; set; }
        public string? ScoreNumber { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentSubType { get; set; }
        public List<IFormFile>? Files { get; set; } = new List<IFormFile>();
    }
}
