using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwEmployeeLicensePreExam
{
    public int EmployeeLicensePreExamId { get; set; }

    public int? EmployeeLicenseId { get; set; }

    public int? ExamId { get; set; }

    public string? Status { get; set; }

    public DateTime? ExamScheduleDate { get; set; }

    public DateTime? ExamTakenDate { get; set; }

    public string? AdditionalNotes { get; set; }
}
