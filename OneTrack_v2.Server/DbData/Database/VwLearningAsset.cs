using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwLearningAsset
{
    public string? AssetId { get; set; }

    public string? AssetTitle { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsActive { get; set; }
}
