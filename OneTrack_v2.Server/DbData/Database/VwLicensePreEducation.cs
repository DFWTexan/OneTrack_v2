using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwLicensePreEducation
{
    public int LicensePreEducationId { get; set; }

    public int? LicenseId { get; set; }

    public int? PreEducationId { get; set; }

    public bool? IsActive { get; set; }
}
