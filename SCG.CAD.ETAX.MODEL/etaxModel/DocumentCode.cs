namespace SCG.CAD.ETAX.MODEL
{
    public partial class DocumentCode
    {
        [Key]
        public int DocumentCodeNo { get; set; }
        public string ErpSource { get; set; } = null!;
        public string DocumentCodeErp { get; set; } = null!;
        public string DocumentCodeRd { get; set; } = null!;
        public string? DocumentDescription { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
