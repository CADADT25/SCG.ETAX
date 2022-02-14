namespace SCG.CAD.ETAX.MODEL
{
    public partial class CancelZipHeader
    {
        [Key]
        public int CancelZipHeaderNo { get; set; }
        public string? StatusCancel { get; set; }
        public string? WorkFlowCode { get; set; }
        public string? CancelReason { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
