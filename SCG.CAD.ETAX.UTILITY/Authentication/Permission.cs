using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.UTILITY.Authentication
{
    public class Permission
    {
        public bool CheckPremissionPage(string premission, string menuindex)
        {
            bool result = false;

            try
            {
                if (!string.IsNullOrEmpty(premission))
                {
                    var liststring = premission.Split(',');
                    if(liststring.Where(x=> x == menuindex).ToList().Count > 0)
                    {
                        result = true;
                    }
                    //foreach (var item in liststring)
                    //{
                    //    if (menuindex == item)
                    //    {
                    //        result = true;
                    //        break;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool CheckControlAction(List<ConfigControlFunction> listcontrol, int controlindex, string userlevel, int menuindex)
        {
            bool result = true;
            try
            {
                userlevel = "," + userlevel + ",";
                var checkcontrol = listcontrol.Where(x => x.ConfigControlFunctionMenuNo == menuindex && x.ConfigControlNo == controlindex).ToList();
                if(checkcontrol.Count > 0)
                {
                    var iscontrol = checkcontrol.FirstOrDefault(x=> x.ConfigControlFunctionRole.Contains(userlevel));
                    if(iscontrol == null)
                    {
                        result = false;
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
