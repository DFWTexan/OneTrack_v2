namespace OneTrak_v2.DataModel
{
    public class IputUpsertLicenseTech
    {
        public int? LicenseTechID { get; set; }
        public string? SOEID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? TeamNum { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }

    public class IputDeleteLicenseTech
    {
        public int LicenseTechID { get; set; }
        public string? UserSOEID { get; set; }
    }
}
