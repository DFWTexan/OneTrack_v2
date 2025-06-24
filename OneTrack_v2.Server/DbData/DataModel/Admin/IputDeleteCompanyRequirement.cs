namespace OneTrak_v2.DataModel
{
    public class IputDeleteCompanyRequirement
    {
        public int CompanyRequirementId { get; set; }

        //public string WorkStateAbv { get; set; } = null!;

        //public string ResStateAbv { get; set; } = null!;

        //public string RequirementType { get; set; } = null!;

        //public bool LicLevel1 { get; set; }

        //public bool LicLevel2 { get; set; }
         
        //public bool LicLevel3 { get; set; }

        //public bool LicLevel4 { get; set; }

        //// Fixed the issue by removing the invalid instantiation of the IFormFile interface.
        //public IFormFile? Document { get; set; }

        //public DateTime? StartAfterDate { get; set; }
        public string? UserSOEID { get; set; }
    }
}
