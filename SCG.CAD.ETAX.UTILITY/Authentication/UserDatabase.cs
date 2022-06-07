using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace SCG.CAD.ETAX.UTILITY
{
    public class UserDatabase
    {
        private readonly IWebHostBuilder env;
        public UserDatabase(IWebHostBuilder env)
        {
            this.env = env;
        }
        private static string CreateHash(string str)
        {
            var salt = "997eff51db1544c7a3c2ddeb2053f051";
            var md5 = new HMACMD5(Encoding.UTF8.GetBytes(salt + str));
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            str = string.Empty;
            foreach (var x in data)
                str += x.ToString("X2");
            return str;
        }
        public async Task<User?> AuthenticateUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;
            var path = Path.Combine("/", "Users");
            if (!Directory.Exists(path))
                return null;
            path += Path.DirectorySeparatorChar + email;
            if (!File.Exists(path))
                return null;
            if (await File.ReadAllTextAsync(path) != CreateHash(password))
                return null;
            return new User(email);
        }
        public async Task<User?> AddUser(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    return null;
                var path = Path.Combine("/", "Users"); // CREATE THE "USERS" FOLDER IN THE PROJECT'S FOLDER!!!
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path += Path.DirectorySeparatorChar + email;
                if (File.Exists(path))
                    return null;
                await File.WriteAllTextAsync(path, CreateHash(password));
                return new User(email);
            }
            catch
            {
                return null;
            }
        }
    }
}
