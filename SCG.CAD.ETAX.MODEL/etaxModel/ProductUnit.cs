namespace SCG.CAD.ETAX.MODEL
{
    public partial class ProductUnit
    {
        [Key]
        public int ProductUnitNo { get; set; }
        public string ProductUnitErp { get; set; } = null!;
        public string ProductUnitRd { get; set; } = null!;
        public string? ProductUnitDescription { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
