using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.EMAIL.BussinessLayer
{
    public class Email
    {
        public void SendEmail()
        {
            GetDataFromDataBase();
        }

        public void GetDataFromDataBase()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
