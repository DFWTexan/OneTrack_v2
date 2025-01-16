using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwEducationRule
{
    public int EducationRuleId { get; set; }

    public int? RuleNumber { get; set; }

    public string? StateProvince { get; set; }

    public string? LicenseType { get; set; }

    public decimal? RequiredCreditHours { get; set; }

    public bool? IsActive { get; set; }

    public int? EducationEndDateId { get; set; }

    public string? ExceptionId { get; set; }

    public string? ExemptionId { get; set; }

    public int? EducationStartDateId { get; set; }
}
