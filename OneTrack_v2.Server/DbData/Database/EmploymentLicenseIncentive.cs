using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class EmploymentLicenseIncentive
{
    public int EmploymentLicenseIncentiveId { get; set; }

    public int EmployeeLicenseId { get; set; }

    public string? RollOutGroup { get; set; }

    public int? DmemploymentId { get; set; }

    public int? CcdBmemploymentId { get; set; }

    public string? DmsentBySoeid { get; set; }

    public DateTime? DmsentDate { get; set; }

    public DateTime? DmapprovalDate { get; set; }

    public DateTime? DmdeclinedDate { get; set; }

    public DateTime? Dm10daySentDate { get; set; }

    public string? Dm10daySentBySoeid { get; set; }

    public DateTime? Dm20daySentDate { get; set; }

    public string? Dm20daySentBySoeid { get; set; }

    public string? Dmcomment { get; set; }

    public string? TmsentBySoeid { get; set; }

    public DateTime? TmsentDate { get; set; }

    public int? Ccd2BmemploymentId { get; set; }

    public DateTime? TmapprovalDate { get; set; }

    public DateTime? TmdeclinedDate { get; set; }

    public DateTime? Tm10daySentDate { get; set; }

    public string? Tm10daySentBySoeid { get; set; }

    public DateTime? TmexceptionDate { get; set; }

    public string? Tmexception { get; set; }

    public string? Tmcomment { get; set; }

    public string? TmokToSellSentBySoeid { get; set; }

    public DateTime? TmokToSellSentDate { get; set; }

    public int? CcokToSellBmemploymentId { get; set; }

    public DateTime? TmomsapprtoSendToHrdate { get; set; }

    public DateTime? TmsentToHrdate { get; set; }

    public DateTime? IncetivePeriodDate { get; set; }

    public string? Notes { get; set; }

    public string? IncentiveStatus { get; set; }

    public DateTime? Tm45daySentDate { get; set; }

    public string? Tm45daySentBySoeid { get; set; }
}
