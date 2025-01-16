using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwEmployeeLicense
{
    public int EmployeeLicenseId { get; set; }

    public int? AscEmployeeLicenseId { get; set; }

    public int? LicenseId { get; set; }

    public DateTime? LicenseExpireDate { get; set; }

    public string? LicenseStatus { get; set; }

    public string? LicenseNumber { get; set; }

    public bool? Reinstatement { get; set; }

    public bool? Required { get; set; }

    public bool? NonResident { get; set; }

    public DateTime? LicenseEffectiveDate { get; set; }

    public DateTime? LicenseIssueDate { get; set; }

    public int? EmploymentId { get; set; }

    public string? LicenseNote { get; set; }

    public DateTime? LineOfAuthorityIssueDate { get; set; }

    public DateTime? RetentionDate { get; set; }
}
