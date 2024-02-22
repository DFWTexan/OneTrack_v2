using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwCompany
{
    public int CompanyId { get; set; }

    public string? CompanyAbv { get; set; }

    public string? CompanyType { get; set; }

    public string? CompanyName { get; set; }

    public int? Tin { get; set; }

    public int? Naicnumber { get; set; }

    public int? AddressId { get; set; }

    public bool? XxxIsActive { get; set; }

    public bool LetterRequired { get; set; }
}
