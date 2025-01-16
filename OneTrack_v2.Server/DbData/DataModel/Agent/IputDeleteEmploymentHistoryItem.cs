using System.ComponentModel.DataAnnotations;

namespace OneTrak_v2.DataModel
{
    public class IputDeleteEmploymentHistoryItem
    {
        [Required]
        public int EmploymentID { get; set; }
        [Required]
        public int EmploymentHistoryID { get; set; }
        [Required]
        public string? UserSOEID { get; set; }
    }
}
