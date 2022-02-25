using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileReasonIssue
    {
        [Key]
        public int ReasonIssueNo { get; set; }
        public string? ReasonIssueDataSource { get; set; }
        public string? ReasonIssueErpDocumentType { get; set; }
        public string? ReasonIssueErpReasonCode { get; set; }
        public string? ReasonIssueRdReasonCode { get; set; }
        public string? ReasonIssueDescription { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
