using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwEducationRuleCriterion
{
    public int EducationCriteriaId { get; set; }

    public string? UsageType { get; set; }

    public string? Description { get; set; }

    public string? Criteria { get; set; }

    public bool? IsActive { get; set; }
}
