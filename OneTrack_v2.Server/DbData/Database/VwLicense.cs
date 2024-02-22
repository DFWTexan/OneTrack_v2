using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwLicense
{
    public int LicenseId { get; set; }

    public string? LicenseName { get; set; }

    public decimal? LicenseFees { get; set; }

    public string? StateProvinceAbv { get; set; }

    public decimal? AppointmentFees { get; set; }

    public bool? IsActive { get; set; }

    public string? LicenseAbv { get; set; }

    public int? LineOfAuthorityId { get; set; }

    public decimal PlsIncentive1Tmpay { get; set; }

    public decimal PlsIncentive1Mrpay { get; set; }

    public decimal Incentive2PlusTmpay { get; set; }

    public decimal Incentive2PlusMrpay { get; set; }

    public decimal LicIncentive3Tmpay { get; set; }

    public decimal LicIncentive3Mrpay { get; set; }
}
