using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class Variable
    {
        // Request status
        public static string RequestStatusCode_WaitManager = "wait_manager";
        public static string RequestStatusCode_WaitOfficer = "wait_officer";
        public static string RequestStatusCode_Reject = "reject";
        public static string RequestStatusCode_Complete = "complete";
        public static string RequestStatusCode_Cancel = "cancel";

        // Request Action
        public static string RequestActionCode_Delete = "delete";
        public static string RequestActionCode_Undelete = "undelete";
        public static string RequestActionCode_ReSignNewTrans = "resign_new_trans";
        public static string RequestActionCode_ReSignNewCert = "resign_new_cert";
    }
}
