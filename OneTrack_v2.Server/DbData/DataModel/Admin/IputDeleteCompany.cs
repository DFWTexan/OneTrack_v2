using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OneTrak_v2.DataModel
{
    public class IputDeleteCompany
    {
        public int? CompanyID { get; set; }
        public int AddressID { get; set; }
        public string UserSOEID { get; set; }
    }
}
