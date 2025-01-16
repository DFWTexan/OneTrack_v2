namespace OneTrak_v2.DataModel
{
    public class IputUpsertProduct
    {
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductAbv { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }

    public class IputDeleteProduct
    {
        public int ProductID { get; set; }
        public string UserSOEID { get; set; } = string.Empty;
    }
}
