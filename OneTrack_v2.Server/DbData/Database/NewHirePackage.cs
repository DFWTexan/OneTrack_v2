using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class NewHirePackage
{
    public int PackageId { get; set; }

    public string? StateProvinceAbv { get; set; }

    public byte[]? PackageFile { get; set; }
}
