using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ConfigApplication
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationName { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
