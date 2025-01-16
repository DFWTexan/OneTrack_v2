using System.Security.Cryptography.X509Certificates;

namespace OneTrak_v2.DataModel
{
    public class IputADBankerImportStatus
    {
        public int TeamMemberID { get; set; }
        public string? CourseState { get; set; }
        public string? StudentName { get; set; }
        public string? CourseTitle { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string UserSOEID { get; set; }
    }
}
