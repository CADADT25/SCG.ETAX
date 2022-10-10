using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.UTILITY.Authentication
{
    public class Permission
    {
        public bool CheckPremissionPage(string premission, string pageindex)
        {
            bool result = false;

            try
            {
                var liststring = premission.Split(',');
                foreach(var item in liststring)
                {
                    if (pageindex == item)
                    {
                        result = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
