using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileUserGroup
    {
        [Key]
        public int ProfileUserGroupNo { get; set; }
        public string? ProfileUserGroupNameTh { get; set; }
        public string? ProfileUserGroupNameEn { get; set; }
        public string? ProfileUserGroupDescription { get; set; }
        public string? ProfileUserRoleId { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
