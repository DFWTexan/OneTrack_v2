using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwLicenseTransformation
{
    public string CurrentLicName { get; set; } = null!;

    public string NewLicName { get; set; } = null!;
}
