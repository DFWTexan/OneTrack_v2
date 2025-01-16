using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class EmploymentJobTitle
{
    public int EmploymentJobTitleId { get; set; }

    public int EmploymentId { get; set; }

    public DateTime? JobTitleDate { get; set; }

    public bool IsCurrent { get; set; }

    public int? JobTitleId { get; set; }
}
