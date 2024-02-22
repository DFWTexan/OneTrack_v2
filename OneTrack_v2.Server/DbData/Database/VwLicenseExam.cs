using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwLicenseExam
{
    public int LicenseExamId { get; set; }

    public int? LicenseId { get; set; }

    public int? ExamId { get; set; }

    public bool? IsActive { get; set; }
}
