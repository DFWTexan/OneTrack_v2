using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class RequiredLicense
{
    public int RequiredLicenseId { get; set; }

    public string WorkStateAbv { get; set; } = null!;

    public string ResStateAbv { get; set; } = null!;

    public int LicenseId { get; set; }

    public string BranchCode { get; set; } = null!;

    public string RequirementType { get; set; } = null!;

    public int? PackageId { get; set; }

    public bool LicLevel1 { get; set; }

    public bool LicLevel2 { get; set; }

    public bool LicLevel3 { get; set; }

    public bool LicLevel4 { get; set; }

    public bool PlsIncentive1 { get; set; }

    public bool Incentive2Plus { get; set; }

    public bool LicIncentive3 { get; set; }

    public string? StartDocument { get; set; }

    public string? RenewalDocument { get; set; }

    public decimal? PlsIncentive1Tmamt { get; set; }

    public decimal? Incentive2PlusTmamt { get; set; }

    public decimal? TmincentiveAmt3 { get; set; }

    public decimal? PlsIncentive1Bmamt { get; set; }

    public decimal? Incentive2PlusBmamt { get; set; }

    public decimal? BmincentiveAmt3 { get; set; }

    public virtual License License { get; set; } = null!;

    public virtual NewHirePackage? Package { get; set; }
}
