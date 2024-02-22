using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class EducationRuleCriterion
{
    public int EducationCriteriaId { get; set; }

    public string? UsageType { get; set; }

    public string? Description { get; set; }

    public string? Criteria { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<EducationRule> EducationRuleEducationEndDates { get; set; } = new List<EducationRule>();

    public virtual ICollection<EducationRule> EducationRuleEducationStartDates { get; set; } = new List<EducationRule>();
}
