using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwEmploymentLicenseIncentive
{
    public int EmploymentLicenseIncentiveId { get; set; }

    public int EmployeeLicenseId { get; set; }

    public string? RollOutGroup { get; set; }

    public int? DmemploymentId { get; set; }

    public int? CcdBmemploymentId { get; set; }

    public string? DmsentBySoeid { get; set; }

    public DateTime? DmsentDate { get; set; }

    public DateTime? DmapprovalDate { get; set; }

    public DateTime? DmdeclinedDate { get; set; }

    public DateTime? IncetivePeriodDate { get; set; }

    public string? IncentiveStatus { get; set; }
}
