using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class LicenseExam
{
    public int LicenseExamId { get; set; }

    public int? LicenseId { get; set; }

    public int? ExamId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Exam? Exam { get; set; }

    public virtual License? License { get; set; }
}
