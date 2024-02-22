using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwEmployeeContEducation
{
    public int? ContEducationRequirementId { get; set; }

    public int EmployeeEducationId { get; set; }

    public int? ContEducationId { get; set; }

    public DateTime? ContEducationTakenDate { get; set; }

    public decimal? CreditHoursTaken { get; set; }

    public string? Status { get; set; }

    public string? AdditionalNotes { get; set; }
}
