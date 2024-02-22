using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class ContEducation
{
    public int ContEducationId { get; set; }

    public string? EducationName { get; set; }

    public string? DeliveryMethod { get; set; }

    public string? Topic { get; set; }

    public decimal? Fees { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public DateTime? ExpireDate { get; set; }

    public int? EducationProviderId { get; set; }

    public virtual ICollection<EmployeeContEducation> EmployeeContEducations { get; set; } = new List<EmployeeContEducation>();
}
