using System;
using System.Collections.Generic;

namespace wcfOnOneTrack_v2eTrack_API.DbData.Models;

public partial class LineOfAuthority
{
    public int LineOfAuthorityId { get; set; }

    public string? LineOfAuthorityAbv { get; set; }

    public string? LineOfAuthorityName { get; set; }

    public bool? AgentStateTable { get; set; }
}
