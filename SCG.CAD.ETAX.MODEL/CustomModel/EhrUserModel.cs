using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public class EhrUserModel
    {
        public string UserID { get; set; }
        public string FirstNameEN { get; set; }
        public string LastNameEN { get; set; }
        public string EmailAddressBusiness { get; set; }
        public string ManagerUserID { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerName { get; set; }
        public bool IsUse { get; set; }
    }
}
