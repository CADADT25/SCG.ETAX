using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.UTILITY
{
    public interface IUserDatabase
    {
        Task<User?> AuthenticateUser(string email, string password);
        Task<User?> AddUser(string email, string password);
    }
}
