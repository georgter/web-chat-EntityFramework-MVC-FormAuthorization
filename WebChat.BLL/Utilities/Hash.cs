using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.BLL.Utilities
{
    class Hash
    {
        /// <summary>
        /// Хеширования пароля нужно его перенисти в бизнес логику 
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public string GetHashPasvord(string pass)
        {
            string hashPass = "";
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] sorceBytes = Encoding.UTF8.GetBytes(pass);
                byte[] hashBytes = sha1.ComputeHash(sorceBytes);
                hashPass = BitConverter.ToString(hashBytes).Replace("_", String.Empty);
            }

            return hashPass;
        }
    }
}
