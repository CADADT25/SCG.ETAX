

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class RdDocument
    {
        public int RdDocumentNo { get; set; }
        public int? RdDocumentCode { get; set; }
        public string? RdDocumentNameTh { get; set; }
        public string? RdDocumentNameEn { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
