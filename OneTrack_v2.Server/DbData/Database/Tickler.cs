using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class Tickler
{
    public int TicklerId { get; set; }

    public string? LkpValue { get; set; }

    public string? Message { get; set; }

    public DateTime? TicklerDate { get; set; }

    public DateTime? TicklerDueDate { get; set; }

    public int? LicenseTechId { get; set; }

    public int? EmploymentId { get; set; }

    public int? EmployeeLicenseId { get; set; }

    public DateTime? TicklerCloseDate { get; set; }

    public int? TicklerCloseByLicenseTechId { get; set; }
}
