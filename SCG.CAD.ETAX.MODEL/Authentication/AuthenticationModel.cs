using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL
{
    public class AuthenticationModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool authenticated { get; set; }

        public string? UrlRedirect { get; set; }
        public string? MessageRedirect { get; set; }

    }
}
