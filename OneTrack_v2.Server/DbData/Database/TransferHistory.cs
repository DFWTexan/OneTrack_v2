using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class TransferHistory
{
    public int TransferHistoryId { get; set; }

    public DateTime? TransferDate { get; set; }

    public string? WorkStateAbv { get; set; }

    public string? ResStateAbv { get; set; }

    public int EmploymentId { get; set; }

    public string? BranchCode { get; set; }

    public string? Scorenumber { get; set; }

    public bool IsCurrent { get; set; }

    public virtual Employment Employment { get; set; } = null!;
}
