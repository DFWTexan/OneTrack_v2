namespace OneTrak_v2.DataModel
{
    public class IputUpsertCompany
    {
        public int? CompanyId { get; set; }
        public string? CompanyAbv { get; set; }
        public string? CompanyType { get; set; }
        public string? CompanyName { get; set; }
        public int? Tin { get; set; }
        public int? Naicnumber { get; set; }
        public int? AddressId { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Phone { get; set; }
        public string? Country { get; set; }
        public string? Zip { get; set; }
        public string? Fax { get; set; }
        public string? UserSOEID { get; set; }
    }
}
