using System.Security.Cryptography;
using System.Text;

namespace ParaProbar
{
    public class KeyGenerator
    {
        public string GetHashString(string keyPublic)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in this.GettingHash(keyPublic))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        private byte[] GettingHash(string text)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
        }
    }
}
