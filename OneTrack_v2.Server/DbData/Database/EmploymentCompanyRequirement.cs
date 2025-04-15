using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class EmploymentCompanyRequirement
{
    public int EmploymentCompanyRequirementId { get; set; }

    public int? EmploymentId { get; set; }

    //public int? AssetSk { get; set; }
    public string? AssetTitle { get; set; }

    public string? AssetId { get; set; }

    public string? LearningProgramStatus { get; set; }

    public DateTime? LearningProgramEnrollmentDate { get; set; }

    public DateTime? LearningProgramCompletionDate { get; set; }
}
