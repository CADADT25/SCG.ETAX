

namespace SCG.CAD.ETAX.MODEL
{
    public partial class ProfileEmailType
    {
        [Key]
        public int EmailTypeNo { get; set; }
        public string? EmailTypeName { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
