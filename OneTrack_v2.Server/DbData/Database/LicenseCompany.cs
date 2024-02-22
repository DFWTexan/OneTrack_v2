using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class LicenseCompany
{
    public int LicenseCompanyId { get; set; }

    public int? LicenseId { get; set; }

    public int? CompanyId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Company? Company { get; set; }

    public virtual License? License { get; set; }
}
