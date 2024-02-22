using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class LicenseTech
{
    public int LicenseTechId { get; set; }

    public string? Soeid { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public bool? IsActive { get; set; }

    public string? TeamNum { get; set; }

    public string? LicenseTechEmail { get; set; }

    public string? LicenseTechPhone { get; set; }

    public string? LicenseTechFax { get; set; }

    public virtual ICollection<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();
}
