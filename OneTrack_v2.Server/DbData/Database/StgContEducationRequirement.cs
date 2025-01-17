﻿using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class StgContEducationRequirement
{
    public int StgContEducationRequirementId { get; set; }

    public int? EmploymentId { get; set; }

    public DateTime? EducationStartDate { get; set; }

    public DateTime? EducationEndDate { get; set; }

    public decimal? RequiredCreditHours { get; set; }

    public bool? IsExempt { get; set; }
}