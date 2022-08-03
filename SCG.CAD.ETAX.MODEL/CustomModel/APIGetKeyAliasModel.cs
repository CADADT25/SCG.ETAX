using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.CustomModel
{
    public class APIGetKeyAliasModel
    {
        public string totalKeyAlias { get; set; }
        public string resultCode { get; set; }
        public string resultDes { get; set; }
        public KeyAliasList keyAliasList { get; set; }
    }

    public class KeyAliasList
    {
        public string certSerial { get; set; }
        public string keyAlias { get; set; }
    }
}
