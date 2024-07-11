namespace OneTrak_v2.DataModel
{
    public class IputAddLicenseProduct
    {
        public int LicenseID { get; set; }
        public int ProductID { get; set; }
        public string UserSOEID { get; set; }
    }

    public class IputDeleteLicenseProduct
    {
        public int LicenseProductID { get; set; }
        public int LicenseID { get; set; }
        public string UserSOEID { get; set; }
    }
}
