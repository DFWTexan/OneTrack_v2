﻿using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class ContEducationRequirement
{
    public int ContEducationRequirementId { get; set; }

    public DateTime? EducationStartDate { get; set; }

    public DateTime? EducationEndDate { get; set; }

    public decimal? RequiredCreditHours { get; set; }

    public bool? IsExempt { get; set; }

    public int? EmploymentId { get; set; }

    public virtual ICollection<EmployeeContEducation> EmployeeContEducations { get; set; } = new List<EmployeeContEducation>();

    public virtual Employment? Employment { get; set; }
}
