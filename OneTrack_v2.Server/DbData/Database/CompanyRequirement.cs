using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class CompanyRequirement
{
    public int CompanyRequirementId { get; set; }

    public string WorkStateAbv { get; set; } = null!;

    public string ResStateAbv { get; set; } = null!;

    public string RequirementType { get; set; } = null!;

    public bool LicLevel1 { get; set; }

    public bool LicLevel2 { get; set; }

    public bool LicLevel3 { get; set; }

    public bool LicLevel4 { get; set; }

    public string? Document { get; set; }

    public DateTime? StartAfterDate { get; set; }
}
