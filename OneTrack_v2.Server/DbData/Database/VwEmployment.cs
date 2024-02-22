using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwEmployment
{
    public int EmploymentId { get; set; }

    public int EmployeeId { get; set; }

    public int? CompanyId { get; set; }

    public bool? IsRehire { get; set; }

    public string? EmployeeStatus { get; set; }

    public string? JobCode { get; set; }

    public string? JobTitle { get; set; }

    public string? EmployeeType { get; set; }

    public bool Cerequired { get; set; }

    public string? WorkPhone { get; set; }

    public bool IsCurrent { get; set; }

    public string? Email { get; set; }

    public string? DirRptMgrTmnum { get; set; }

    public string? H1employmentId { get; set; }

    public string? H2employmentId { get; set; }

    public string? H3employmentId { get; set; }

    public string? H4employmentId { get; set; }

    public string? H5employmentId { get; set; }

    public string? H6employmentId { get; set; }

    public string? Tmtype { get; set; }

    public string LicenseIncentive { get; set; } = null!;

    public string LicenseLevel { get; set; } = null!;

    public DateTime? RetentionDate { get; set; }

    public bool SecondChance { get; set; }
}
