using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class StgOtincentive
{
    public int OtincentiveId { get; set; }

    public int? EmploymentId { get; set; }

    public int? EmployeeLicenseId { get; set; }

    public int? EmploymentLicenseIncentiveId { get; set; }

    public int TeamNumber { get; set; }

    public string? AgentName { get; set; }

    public string? WkSt { get; set; }

    public string? LicType { get; set; }

    public string? UpdtLi { get; set; }

    public string? UpdtAh { get; set; }

    public string? RolloutGrp { get; set; }

    public string? Dmname { get; set; }

    public string? Bmname { get; set; }

    public string? SentToDmby { get; set; }

    public DateTime? DateSentToDm { get; set; }

    public DateTime? DateDmapproved { get; set; }

    public DateTime? DateDmdeclined { get; set; }

    public string? DmapprovalStatus { get; set; }

    public DateTime? Dmfollowup10Date { get; set; }

    public DateTime? Dmfollowup20Date { get; set; }

    public string? Dmcomment { get; set; }

    public string? ProgramEligible { get; set; }

    public string? SentToTmby { get; set; }

    public DateTime? DateSentToTm { get; set; }

    public DateTime? DateReceivedTmapproval { get; set; }

    public DateTime? DateReceivedTmdecline { get; set; }

    public string? TmapprovalStatus { get; set; }

    public DateTime? Tmfollowup10Date { get; set; }

    public DateTime? EnrollDate { get; set; }

    public string? Tmcomment { get; set; }

    public DateTime? ExceptionStartDate { get; set; }

    public string? ExceptionReason { get; set; }

    public DateTime? IncentiveExpirationDate { get; set; }

    public DateTime? CompletedIn90days { get; set; }

    public DateTime? AppointmentEffDate { get; set; }

    public DateTime? OmsapprToSendToHr { get; set; }

    public DateTime? DateSentToHr { get; set; }

    public DateTime? IncetivePeriodDate { get; set; }

    public string? Notes { get; set; }
}
