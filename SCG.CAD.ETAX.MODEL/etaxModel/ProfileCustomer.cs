

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileCustomer
    {
        public int CustomerProfileNo { get; set; }
        public string CustomerId { get; set; } = null!;
        public string CompanyCode { get; set; } = null!;
        public string OutputType { get; set; } = null!;
        public string NumberOfCopies { get; set; } = null!;
        public string? CustomerEmail { get; set; }
        public string? CustomerCcemail { get; set; }
        public string? EmailType { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
