using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileUserManagement
    {
        [Key]
        public int UserNo { get; set; }
        public string UserEmail { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string DomainName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string GroupId { get; set; } = null!;
        public DateTime? PasswordRegister { get; set; }
        public DateTime? PasswordExpire { get; set; }
        public string? AttempLogin { get; set; }
        public DateTime? AttempLast { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int? AccountStatus { get; set; }
    }
}
