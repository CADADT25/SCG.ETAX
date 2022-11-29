using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class VerifyUserDataFromJwt
    {
        public string ExternalId { get; set; }
        public string ExternalId2 { get; set; }
        public string Email { get; set; }
        public DateTime iat { get; set; }
        public string userId { get; set; }
        public string redirectUrl { get; set; }
    }
}
