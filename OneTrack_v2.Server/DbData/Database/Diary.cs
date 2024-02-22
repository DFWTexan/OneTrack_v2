using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class Diary
{
    public int DiaryId { get; set; }

    public string? DiaryName { get; set; }

    public string? Notes { get; set; }

    public DateTime? DiaryDate { get; set; }

    public string? Soeid { get; set; }

    public int? EmploymentId { get; set; }

    public virtual Employment? Employment { get; set; }
}
