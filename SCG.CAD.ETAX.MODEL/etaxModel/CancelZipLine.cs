namespace SCG.CAD.ETAX.MODEL
{
    public partial class CancelZipLine
    {
        [Key]
        public int CancelZipLineNo { get; set; }
        public int CancelZipHeaderNo { get; set; }
        public string BillingNo { get; set; } = null!;
        public string TransactionNo { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
