using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwPreEducation
{
    public int PreEducationId { get; set; }

    public string? EducationName { get; set; }

    public short? CreditHours { get; set; }

    public string? DeliveryMethod { get; set; }

    public decimal? Fees { get; set; }

    public int? EducationProviderId { get; set; }

    public bool? XxxIsActive { get; set; }

    public string? StateProvinceAbv { get; set; }
}
