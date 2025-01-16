using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class AuditLogCe
{
    public int AuditLogId { get; set; }

    public string? ProcessName { get; set; }

    public string? StepName { get; set; }

    public string? EducationRule { get; set; }

    public string? ExemptionRule { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? ProcessStatus { get; set; }

    public int? InsertRowCount { get; set; }

    public int? UpdateRowCount { get; set; }
}
