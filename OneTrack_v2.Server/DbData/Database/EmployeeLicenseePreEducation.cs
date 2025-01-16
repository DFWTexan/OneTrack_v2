using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class EmployeeLicenseePreEducation
{
    public int EmployeeLicensePreEducationId { get; set; }

    public int? EmployeeLicenseId { get; set; }

    public string? Status { get; set; }

    public DateTime? EducationEndDate { get; set; }

    public int? PreEducationId { get; set; }

    public int? CompanyId { get; set; }

    public DateTime? EducationStartDate { get; set; }

    public string? AdditionalNotes { get; set; }

    public virtual Company? Company { get; set; }

    public virtual EmployeeLicense? EmployeeLicense { get; set; }

    public virtual PreEducation? PreEducation { get; set; }
}
