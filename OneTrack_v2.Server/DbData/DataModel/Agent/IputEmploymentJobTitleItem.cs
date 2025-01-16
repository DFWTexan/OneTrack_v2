namespace OneTrak_v2.DataModel
{
    public class IputEmploymentJobTitleItem
    {
        public int? EmploymentID { get; set; }
        public int? EmploymentJobTitleID { get; set; }
        public DateTime? JobTitleDate { get; set; }
        public int? JobTitleID { get; set; }
        public bool? IsCurrent { get; set; }
        public string? UserSOEID { get; set; }
    }
}
