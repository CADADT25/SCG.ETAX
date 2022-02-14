

namespace SCG.CAD.ETAX.MODEL
{
    public partial class ZipFilePost
    {
        [Key]
        public int ZipFilePostNo { get; set; }
        public int ZipTransactionNo { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int ZipFileStatus { get; set; }
    }
}
