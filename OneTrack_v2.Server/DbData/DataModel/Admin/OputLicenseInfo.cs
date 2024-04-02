namespace OneTrak_v2.DataModel
{
    public class OputLicenseInfo
    {
        public int LicenseId { get; set; }
        public string? LicenseName { get; set; }
        public string? LicenseAbv { get; set; }
        public string? StateProvinceAbv { get; set; }
        public string? LineOfAuthorityAbv { get; set; }
        public int LineOfAuthorityId { get; set; }
        public bool? AgentStateTable { get; set; }
        public decimal PlsIncentive1Tmpay { get; set; }
        public decimal PlsIncentive1Mrpay { get; set; }
        public decimal Incentive2PlusTmpay { get; set; }
        public decimal Incentive2PlusMrpay { get; set; }
        public decimal LicIncentive3Tmpay { get; set; }
        public decimal LicIncentive3Mrpay { get; set; }
        public bool? IsActive { get; set; }
        public List<LicenseCompanyItem> CompanyItems { get; set; }
        public List<LicensePreExamItem> PreExamItems { get; set; }
        public List<LicensePreEducationItem> PreEducationItems { get; set; }
        public List<LicenseProductItem> ProductItems { get; set; }

       public OputLicenseInfo()
        {
            CompanyItems = new List<LicenseCompanyItem>();
            PreExamItems = new List<LicensePreExamItem>();
            PreEducationItems = new List<LicensePreEducationItem>();
            ProductItems = new List<LicenseProductItem>();
        }
    }

    public class LicenseCompanyItem 
    { 
        public int LicenseCompanyId { get; set; }
        public int CompanyId { get; set; }
        public string? CompanyAbv { get; set; }
        public string? CompanyType { get; set; }
        public string? CompanyName { get; set; }
        public string? TIN { get; set; }
        public string? NAICNumber { get; set; }
        public bool? IsActive { get; set; }
        public int AddressId { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Country { get; set; }
    }

    public class LicensePreExamItem
    {
        public int ExamId { get; set; }
        public string? ExamName { get; set; }
        public string? StateProvinceAbv { get; set; }
        public string? CompanyName { get; set; }
        public string? DeliveryMethod { get; set; }
        public int LicenseExamID { get; set; }
        public int ExamProviderID { get; set; }
        public bool? IsActive { get; set; }
    }

    public class LicensePreEducationItem
    {         
        public int LicensePreEducationID { get; set; }
        public int PreEducationID { get; set; }
        public string? EducationName { get; set; }
        public string? StateProvinceAbv { get; set; }
        public short CreditHours { get; set; }
        public int CompanyID { get; set; }
        public string? CompanyName { get; set; }
        public string? DeliveryMethod { get; set; }
        public bool? IsActive { get; set; }
    }

    public class LicenseProductItem
    {
        public int LicenseProductID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductAbv { get; set; }
        public bool? IsActive { get; set; }
    }
}
