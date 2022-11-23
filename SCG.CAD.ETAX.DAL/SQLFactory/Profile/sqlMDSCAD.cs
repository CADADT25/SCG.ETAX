using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.DAL
{
    public class sqlMDSCAD
    {
        public string GET_MANAGER_BY_USER(string user)
        {
            string strSql = @"SELECT UserID,FirstNameEN,LastNameEN,EmailAddressBusiness,ManagerUserID,ManagerEmail,ManagerName
                          FROM [HR].[vweHR_User]
                          WHERE ManagerUserID IS NOT NULL AND (EmailAddressBusiness = '" + user + "' OR UserID = '" + user + "' )";

            return strSql;
        }

    }
}
