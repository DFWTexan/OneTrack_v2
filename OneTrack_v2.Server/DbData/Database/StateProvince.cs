using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class StateProvince
{
    public string StateProvinceCode { get; set; } = null!;

    public string? StateProvinceName { get; set; }

    public string? Country { get; set; }

    public string StateProvinceAbv { get; set; } = null!;

    public int? DoiaddressId { get; set; }

    public int? LicenseTechId { get; set; }

    public bool? IsActive { get; set; }

    public string? Doiname { get; set; }

    public virtual Address? Doiaddress { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual LicenseTech? LicenseTech { get; set; }

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    public virtual ICollection<PreEducation> PreEducations { get; set; } = new List<PreEducation>();
}
