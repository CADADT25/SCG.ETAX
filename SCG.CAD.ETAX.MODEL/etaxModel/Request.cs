using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class Request
    {
        [Key]
        public Guid Id { get; set; }
        public string RequestNo { get; set; } = null!;
        public string RequestAction { get; set; } = null!;
        public string StatusCode { get; set; } = null!;
        public string? CompanyCode { get; set; }
        public string Manager { get; set; } = null!;
        public bool ManagerAction { get; set; }
        public string? OfficerBy { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
    }
}
