

namespace SCG.CAD.ETAX.MODEL
{
    public partial class TaxCode
    {
        [Key]
        public int TaxCodeNo { get; set; }
        public string TaxCodeErp { get; set; } = null!;
        public string TaxCodeRd { get; set; } = null!;
        public string? TaxCodeDescription { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
