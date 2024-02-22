using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class Branch
{
    public string? Edr { get; set; }

    public string BranchCode { get; set; } = null!;

    public string? FacilityCode { get; set; }

    public string? OfficeName { get; set; }

    public string? SupervisoryCode { get; set; }

    public string? SupervisoryCodeDesc { get; set; }

    public string? TypeforNetworkComm { get; set; }

    public string? TypeDescription { get; set; }

    public string? CountryId { get; set; }

    public string? StreetAddress1 { get; set; }

    public string? StreetAddress2 { get; set; }

    public string? StreetAddress3 { get; set; }

    public string? Streetcity { get; set; }

    public string? StreetState { get; set; }

    public string? Streetzip { get; set; }

    public string? Boxlabel { get; set; }

    public string? Poboxdrawer { get; set; }

    public string? Pocity { get; set; }

    public string? PoState { get; set; }

    public string? Pozip { get; set; }

    public string? InternalPhone { get; set; }

    public string? CustomerPhone { get; set; }

    public string? FaxNumber { get; set; }

    public string? ManagerNamefirst { get; set; }

    public string? ManagerNamemIddle { get; set; }

    public string? ManagerNamematernal { get; set; }

    public string? ManagerNamelast { get; set; }

    public string? PuId { get; set; }

    public string? LuType1 { get; set; }

    public string? VpsDest { get; set; }

    public string? OpenClosedInd { get; set; }

    public string? OpenClosedIndDesc { get; set; }

    public string? OpenDate { get; set; }

    public string? ClosedDate { get; set; }

    public string? UpDateuserId { get; set; }

    public string? UpDateDate { get; set; }

    public string? ActionLoanInd { get; set; }

    public string? ActionbankInd { get; set; }

    public string? TbranTypeInd { get; set; }

    public string? MaestroStatus { get; set; }

    public string? MaestroDesc { get; set; }

    public string? TbranTerminalType { get; set; }

    public string? SubType { get; set; }

    public string? SubTypeDesc { get; set; }

    public string? UrbanInd { get; set; }

    public string? UrbanDesc { get; set; }

    public string? AcquisitionIndicator { get; set; }

    public string? AcquisitionIndDesc { get; set; }

    public string? AcquisitionState { get; set; }

    public string? AcquisitionBranch { get; set; }

    public virtual XxxEdr? EdrNavigation { get; set; }
}
