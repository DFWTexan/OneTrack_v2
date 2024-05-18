namespace OneTrak_v2.DataModel
{
    public class IputTransferHistoryItem
    {
        public int? EmployeeID { get; set; }
        public int? EmploymentID { get; set; }
        public int? TransferHistoryID { get; set; }
        public string? BranchCode { get; set; }
        public string? WorkStateAbv { get; set; }
        public string? ResStateAbv { get; set; }
        public string? TransferDate { get; set; }
        public bool? IsCurrent { get; set; }
        public string? UserSOEID { get; set; }
    }
}
