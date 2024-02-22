using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwEmployeeLicenseePreEducation
{
    public int EmployeeLicensePreEducationId { get; set; }

    public int? EmployeeLicenseId { get; set; }

    public string? Status { get; set; }

    public DateTime? EducationEndDate { get; set; }

    public int? PreEducationId { get; set; }

    public int? CompanyId { get; set; }

    public DateTime? EducationStartDate { get; set; }

    public string? AdditionalNotes { get; set; }
}
