using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwExam
{
    public int ExamId { get; set; }

    public string? ExamName { get; set; }

    public decimal? ExamFees { get; set; }

    public bool? XxxIsActive { get; set; }

    public string? StateProvinceAbv { get; set; }

    public int? ExamProviderId { get; set; }

    public string? DeliveryMethod { get; set; }
}
