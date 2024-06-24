namespace OneTrack_v2.DataModel
{
    public class OputVarDropDownList
    {
        public string? Value { get; set; } = null;
        public string? Label { get; set; } = null;
    }

    public class OputIntDropDownList
    {
        public int Value { get; set; } = 0;
        public string? Label { get; set; } = null;
    }

    public class OputVarDropDownList_v2
    {
        public string? Value { get; set; } = null;
        public string? Label { get; set; } = null;
    }

    public class IputFileUpload
    {
        public string? FileName { get; set; } = null;
        public string? FileUrl { get; set; } = null;
        public FormFile? File { get; set; } = null;
    }
    
}
