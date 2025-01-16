using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwProduct
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public DateTime? ExpireDate { get; set; }

    public bool? XxxIsActive { get; set; }

    public bool? XxxAgentMaster { get; set; }

    public string? ProductAbv { get; set; }
}
