using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileUserRole
    {
        [Key]
        public int ProfileUserRoleNo { get; set; }
        public string? ProfileUserRoleNameTh { get; set; }
        public string? ProfileUserRoleNameEn { get; set; }
        public string? ProfileUserRoleDescription { get; set; }
        public string? ProfileUserRoleMunu { get; set; }
        public string? ProfileUserRoleFunction { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
