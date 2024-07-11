namespace OneTrak_v2.DataModel
{
    public class IputAddLicensePreEducation
    {
        public int LicenseID { get; set; }
        public int PreEducationID { get; set; }
        public string UserSOEID { get; set; }
    }

    public class IputDeleteLicensePreEdu
    {
        public int LicensePreEducationID { get; set; }
        public int LicenseID { get; set; }
        public string UserSOEID { get; set; }
    }
}
