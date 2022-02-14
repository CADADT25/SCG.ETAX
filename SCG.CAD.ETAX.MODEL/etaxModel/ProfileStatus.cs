

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileStatus
    {
        public int StatusNo { get; set; }
        public string? StatusNameTh { get; set; }
        public string StatusNameEn { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
