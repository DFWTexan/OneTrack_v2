using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class LettersGenerated
{
    public int LetterDataId { get; set; }

    public int? EmploymentId { get; set; }

    public string? LetterName { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? LetterData { get; set; }

    public string? BranchCode { get; set; }

    public string? BranchCodeNumber { get; set; }

    public string? DistrictTmnum { get; set; }

    public string? RegionCode { get; set; }

    public int? EmployeeLicenseId { get; set; }

    public int? CompanyId { get; set; }

    public virtual Letter? LetterNameNavigation { get; set; }
}
