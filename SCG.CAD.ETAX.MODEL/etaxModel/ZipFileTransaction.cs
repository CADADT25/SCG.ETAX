

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ZipFileTransaction
    {
        public int ZipTransactionNo { get; set; }
        public string? ZipFileName { get; set; }
        public string? ZipFilePath { get; set; }
        public string? ZipFilePostStatus { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
