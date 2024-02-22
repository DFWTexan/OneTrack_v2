using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class StgAgentStateTable
{
    public int AgentStateTableId { get; set; }

    public string? EmployeeNumber { get; set; }

    public string? LicenseStateCode { get; set; }

    public string? LineOfAuthorityAbv { get; set; }

    public string? LicenseStatus { get; set; }

    public string? LicenseNumber { get; set; }

    public string? ResidentCode { get; set; }

    public string? LicenseOriginalDate { get; set; }

    public string? LicenseCurrentDate { get; set; }

    public string? LicenseExpirationDate { get; set; }

    public DateTime? ChangeDate { get; set; }
}
