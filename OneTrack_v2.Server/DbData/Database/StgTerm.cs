using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class StgTerm
{
    public int StgTermsId { get; set; }

    public string? Ssno { get; set; }

    public string? Geid { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Zip { get; set; }

    public string? DateofBirth { get; set; }

    public string? HireDate { get; set; }

    public string? JobTitle { get; set; }

    public string? LicenseState { get; set; }

    public string? Branch { get; set; }

    public string? Status { get; set; }

    public string? LeaveStatus { get; set; }

    public string? Pru { get; set; }

    public string? HrtermDate { get; set; }

    public string? FormerCo { get; set; }
}
