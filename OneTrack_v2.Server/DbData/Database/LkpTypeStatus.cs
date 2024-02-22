using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class LkpTypeStatus
{
    public string LkpField { get; set; } = null!;

    public string LkpValue { get; set; } = null!;

    public short? SortOrder { get; set; }
}
