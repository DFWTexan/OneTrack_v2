using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class JobTitle
{
    public int JobTitleId { get; set; }

    public string? JobTitle1 { get; set; }

    public string? JobCode { get; set; }

    public bool RequireLicense { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? Reviewed { get; set; }

    public string LicenseIncentive { get; set; } = null!;

    public string LicenseLevel { get; set; } = null!;

    public bool? IsActive { get; set; }
}
