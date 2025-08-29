using System.Security.Cryptography;
using System.Text;

namespace projetoGloboClima.Shared.Utils
{
    public static class Hash
    {
        public static string GerarHashSHA512(string texto)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytesTexto = Encoding.UTF8.GetBytes(texto);
                byte[] hashBytes = sha512.ComputeHash(bytesTexto);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
