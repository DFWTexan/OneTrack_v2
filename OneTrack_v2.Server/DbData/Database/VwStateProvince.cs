using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwStateProvince
{
    public string StateProvinceCode { get; set; } = null!;

    public string? StateProvinceName { get; set; }

    public string? Country { get; set; }

    public string StateProvinceAbv { get; set; } = null!;

    public int? DoiaddressId { get; set; }

    public int? LicenseTechId { get; set; }

    public bool? IsActive { get; set; }

    public string? Doiname { get; set; }
}
