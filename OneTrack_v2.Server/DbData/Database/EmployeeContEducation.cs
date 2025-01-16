using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class EmployeeContEducation
{
    public int EmployeeEducationId { get; set; }

    public int? ContEducationId { get; set; }

    public int? ContEducationRequirementId { get; set; }

    public DateTime? ContEducationTakenDate { get; set; }

    public decimal? CreditHoursTaken { get; set; }

    public string? Status { get; set; }

    public string? AdditionalNotes { get; set; }

    public virtual ContEducation? ContEducation { get; set; }

    public virtual ContEducationRequirement? ContEducationRequirement { get; set; }
}
