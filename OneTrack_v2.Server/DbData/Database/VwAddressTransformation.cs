using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwAddressTransformation
{
    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public string? Phone { get; set; }

    public string Address1New { get; set; } = null!;

    public string? Address2New { get; set; }

    public string CityNew { get; set; } = null!;

    public string StateNew { get; set; } = null!;

    public string ZipNew { get; set; } = null!;

    public string? PhoneNew { get; set; }
}
