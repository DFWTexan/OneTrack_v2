using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwAddress
{
    public int AddressId { get; set; }

    public string? AddressType { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Phone { get; set; }

    public string? Country { get; set; }

    public string? Zip { get; set; }

    public string? Fax { get; set; }
}
