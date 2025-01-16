namespace OneTrak_v2.DataModel
{
    public class IputAddLicenseCompany
    {
        public int LicenseID { get; set; }
        public int CompanyID { get; set; }
        public string UserSOEID { get; set; }
    }

    public class IputDeleteLicenseCompany
    {
        public int LicenseCompanyID { get; set; }
        public int LicenseID { get; set; }
        public string UserSOEID { get; set; }
    }
}
