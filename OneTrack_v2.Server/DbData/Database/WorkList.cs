using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class WorkList
{
    public string WorkListName { get; set; } = null!;

    public string? Fieldlist { get; set; }

    public bool? IsActive { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<WorkListDatum> WorkListData { get; set; } = new List<WorkListDatum>();
}
