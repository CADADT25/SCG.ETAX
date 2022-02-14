namespace SCG.CAD.ETAX.MODEL
{
    public partial class NewsBoard
    {
        [Key]
        public int NewsBoardNo { get; set; }
        public string NewsBoardHeader { get; set; } = null!;
        public string NewsBoardBody { get; set; } = null!;
        public string? NewsBoardFooter { get; set; }
        public int NewsBoardSeq { get; set; }
        public DateTime NewsBoardStart { get; set; }
        public DateTime NewsBoardEnd { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
