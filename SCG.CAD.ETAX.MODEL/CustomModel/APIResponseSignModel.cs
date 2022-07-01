using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.CustomModel
{
    public class APIResponseSignModel
    {
        //pdf signed file  in format byte[] encode base64
        public string fileSigned { get; set; }
        //Signed result Code
        public string resultCode { get; set; }
        //Signed Result description
        public string resultDes { get; set; }
    }

    /*
     
    ResultCode	Remark
    000 	    Service Request success 
    xxx 	    xxxxxxxxxxxxxxxxxxxxxxxxxx
    999 	    Exception error
     
     */
}
