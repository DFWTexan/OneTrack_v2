using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwEmploymentHistory
{
    public int EmploymentHistoryId { get; set; }

    public DateTime? HireDate { get; set; }

    public DateTime? RehireDate { get; set; }

    public bool? ForCause { get; set; }

    public int? EmploymentId { get; set; }

    public DateTime? NotifiedTermDate { get; set; }

    public DateTime? HrtermDate { get; set; }

    public string? BackgroundCheckStatus { get; set; }

    public string? BackGroundCheckNotes { get; set; }

    public bool IsCurrent { get; set; }

    public string? HrtermCode { get; set; }
}
