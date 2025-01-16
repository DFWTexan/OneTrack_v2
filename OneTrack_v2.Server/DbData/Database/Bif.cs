using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class Bif
{
    public int Bifid { get; set; }

    public string? HrDepartmentId { get; set; }

    public string? Name { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? CloseDate { get; set; }

    public string? ScoreNumber { get; set; }

    public DateTime? UpdateDate { get; set; }
}
