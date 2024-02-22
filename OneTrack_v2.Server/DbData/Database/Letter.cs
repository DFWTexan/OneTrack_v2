using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class Letter
{
    public string LetterName { get; set; } = null!;

    public string? Fieldlist { get; set; }

    public bool? IsActive { get; set; }

    public string? Description { get; set; }

    public string? SendMethod { get; set; }

    public virtual ICollection<LettersGenerated> LettersGenerateds { get; set; } = new List<LettersGenerated>();
}
