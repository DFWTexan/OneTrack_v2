using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class LicenseProduct
{
    public int LicenseProductId { get; set; }

    public int? LicenseId { get; set; }

    public int? ProductId { get; set; }

    public bool? IsActive { get; set; }

    public virtual License? License { get; set; }

    public virtual Product? Product { get; set; }
}
