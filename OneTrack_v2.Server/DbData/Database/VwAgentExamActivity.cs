using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwAgentExamActivity
{
    public int? EmploymentId { get; set; }

    public string? ManagerName { get; set; }

    public string? AgentName { get; set; }

    public string AgentEmail { get; set; } = null!;

    public string? AgentPhone { get; set; }

    public string? ResidentState { get; set; }

    public string? LicenseState { get; set; }

    public string ExamCode { get; set; } = null!;

    public string ProductCode { get; set; } = null!;

    public DateTime? Registered { get; set; }

    public DateTime? FirstAccess { get; set; }

    public DateTime? LastLogin { get; set; }

    public short? PctComplete { get; set; }

    public DateTime? Date100PctComplete { get; set; }

    public int? DaysToReach100Pct { get; set; }

    public string? TimeInCourse { get; set; }

    public short? BestSyescore { get; set; }

    public short? BestGuaranteeScore { get; set; }

    public DateTime? CourseExpirationDate { get; set; }

    public string? TimeInReading { get; set; }

    public string? TimeInStudyByTopicQuizzes { get; set; }

    public string? TimeInSye { get; set; }
}
