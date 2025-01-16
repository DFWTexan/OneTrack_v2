using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwEmployee
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Suffix { get; set; }

    public string? Alias { get; set; }

    public string? Geid { get; set; }

    public string? Soeid { get; set; }

    public int? NationalProducerNumber { get; set; }

    public int? EmployeeSsnid { get; set; }

    public int AddressId { get; set; }

    public bool? ExcludeFromRpts { get; set; }

    public DateTime? PurgeDate { get; set; }

    public string? LegalHold { get; set; }

    public string? Urccode { get; set; }
}
