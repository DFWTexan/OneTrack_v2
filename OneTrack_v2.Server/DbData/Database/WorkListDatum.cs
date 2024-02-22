using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class WorkListDatum
{
    public int WorkListDataId { get; set; }

    public string? WorkListName { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? WorkListData { get; set; }

    public DateTime? ProcessDate { get; set; }

    public string? ProcessedBy { get; set; }

    public string? LicenseTech { get; set; }

    public virtual WorkList? WorkListNameNavigation { get; set; }
}
