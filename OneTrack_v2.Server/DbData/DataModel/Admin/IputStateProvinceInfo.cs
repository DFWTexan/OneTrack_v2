namespace OneTrak_v2.Server.DbData.DataModel.Admin
{
    public class IputUpsertStateProvince
    {
        public string? UpsertType { get; set; }
        public string? StateProvinceCode { get; set; }
        public string? StateProvinceName { get; set; }
        public string? Country { get; set; }
        public string? StateProvinceAbv { get; set; }
        public int? LicenseTechID { get; set; }
        public string DOIName { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
        public string? AddressType { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Phone { get; set; }
        public string? Zip { get; set; }
        public string? Fax { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }

    public class IputDeleteStateProvince
    {
        public string StateProvinceCode { get; set; }
        public int DOIAddressID { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }
}
