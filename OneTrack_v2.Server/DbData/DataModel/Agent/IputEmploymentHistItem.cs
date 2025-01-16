namespace OneTrak_v2.DataModel
{
    public class InputEmploymentHistItem
    {
        public int? EmploymentID { get; set; }
        public int? EmployeeID { get; set; }
        public int? EmploymentHistoryID { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? RehireDate { get; set; }
        public DateTime? NotifiedTermDate { get; set; }
        public DateTime? HrTermDate { get; set; }
        public string? HrTermCode { get; set; }
        public bool? IsForCause { get; set; }
        public string? BackgroundCheckStatus { get; set; }
        public string? BackGroundCheckNotes { get; set; }
        public bool? IsCurrent { get; set; }
        public string? UserSOEID { get; set; }
    }
}
