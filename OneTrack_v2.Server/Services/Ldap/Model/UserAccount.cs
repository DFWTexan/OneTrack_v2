namespace OneTrak_v2.Services.Model
{
    public class UserAcctInfo
    {
        public string? DisplayName { get; set; }
        public string? SOEID { get; set; }
        public string? Email { get; set; }
        public bool? Enabled { get; set; }
        public string? EmployeeId { get; set; }
        public string? HomeDirectory { get; set; }
        public DateTime? LastLogon { get; set; }
        public bool? IsAdminRole { get; set; }
        public bool? IsTechRole { get; set; }
        public bool? IsReadRole { get; set; }
        public bool? IsSuperUser { get; set; }
    }
}
