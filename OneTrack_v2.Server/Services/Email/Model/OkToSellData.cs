using System.Security.Principal;

namespace OneTrak_v2.Services.Model
{
    public class OkToSellData
    {
        public int GEID { get; set; } 
        public string? TMName { get; set; }
        public string? TMNumber { get; set; }
        public string? TMTitle { get; set; }
        public string? LicenseTechName { get; set; }
        public string? LicenseTechPhone { get; set; }
        public string? LicenseTechTitle { get; set; }
        public List<SellStateItem> SellStates { get; set; }
        public List<LicenseStateItem> licenseStateItems { get; set; }
        public List<LicenseEffectiveDate> licenseEffectiveDates { get; set; }

        public OkToSellData()
        {
            SellStates = new List<SellStateItem>();
            licenseStateItems = new List<LicenseStateItem>();
            licenseEffectiveDates = new List<LicenseEffectiveDate>();
        }
    }

    public class SellStateItem
    {
        public string? StateProvinceAbv { get; set; } 
        public string? StateProvinceName { get; set; }
    }

    public class LicenseStateItem
    {
        public int EmployeeLicenseID { get; set; }
        public string? StateAbbr { get; set; }
        public string? LineOfAuthorityName { get; set; }
        public string? LicenseNumber { get; set; }
    }

    public class LicenseEffectiveDate
    {
        public int EmployeeLicenseID { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? AppointmentEffectiveDate { get; set; }
    }
}
