using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace api.Utils
{
    public class Encrypt
    {
        public static string GetMD5Hash(string input)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
                bs = md5.ComputeHash(bs);
                System.Text.StringBuilder s = new System.Text.StringBuilder();
                foreach (byte b in bs)
                    s.Append(b.ToString("x2").ToLower());
                return s.ToString();
            }
        }
    }
}
