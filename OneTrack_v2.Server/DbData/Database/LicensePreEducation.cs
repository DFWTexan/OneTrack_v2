using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class LicensePreEducation
{
    public int LicensePreEducationId { get; set; }

    public int? LicenseId { get; set; }

    public int? PreEducationId { get; set; }

    public bool? IsActive { get; set; }

    public virtual License? License { get; set; }

    public virtual PreEducation? PreEducation { get; set; }
}
