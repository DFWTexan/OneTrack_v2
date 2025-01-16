namespace OneTrak_v2.DataModel
{
    public class IputUpdateLicenseCompany
    {
        public int LicenseCompanyID { get; set; }
        public bool IsActive { get; set; }
        public string UserSOEID { get; set; }
    }

    public class IputUpdateLicenseExam
    {
        public int LicenseExamID { get; set; }
        public bool IsActive { get; set; }
        public string UserSOEID { get; set; }
    }

    public class IputUpdateLicensePreEducation
    {
        public int LicensePreEducationID { get; set; }
        public bool IsActive { get; set; }
        public string UserSOEID { get; set; }
    }

    public class IputUpdateLicenseProduct
    {
        public int LicenseProductID { get; set; }
        public bool IsActive { get; set; }
        public string UserSOEID { get; set; }
    }
}
