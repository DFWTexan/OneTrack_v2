using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class XxxEdr
{
    public string Edr { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? BranchKey { get; set; }

    public string? FacilityCode { get; set; }

    public string? ReportingName { get; set; }

    public string? SupervisoryCode { get; set; }

    public string? SupervisoryDesc { get; set; }

    public string? TypeforNetworkComm { get; set; }

    public string? TypeDesc { get; set; }

    public string? CountryId { get; set; }

    public string? StreetAddress1 { get; set; }

    public string? StreetAddress2 { get; set; }

    public string? StreetAddress3 { get; set; }

    public string? StreetCity { get; set; }

    public string? StreetState { get; set; }

    public string? StreetZip { get; set; }

    public string? PoboxLabel { get; set; }

    public string? PoboxDrawer { get; set; }

    public string? Pocity { get; set; }

    public string? Postate { get; set; }

    public string? Pozip { get; set; }

    public string? Faxnumber { get; set; }

    public string? ManagerNameFirst { get; set; }

    public string? ManagerNameMiddle { get; set; }

    public string? ManagerNameMaternal { get; set; }

    public string? ManagerNameLast { get; set; }

    public string? Puid { get; set; }

    public string? Lutype1 { get; set; }

    public string? Vpsdest { get; set; }

    public string? OpenClosedInd { get; set; }

    public string? OpenClosedInddesc { get; set; }

    public string? OpenDate { get; set; }

    public string? ClosedDate { get; set; }

    public string? UpdateUserId { get; set; }

    public string? UpdateDate { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
}
