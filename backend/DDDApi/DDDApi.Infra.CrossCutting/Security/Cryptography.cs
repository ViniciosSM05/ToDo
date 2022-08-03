using System.Security.Cryptography;
using System.Text;

namespace DDDApi.Infra.CrossCutting.Security
{
    public static class Cryptography
    {
        public static string Execute(string password)
        {
            var md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            var strBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++) strBuilder.Append(data[i].ToString("x2"));
            return strBuilder.ToString();
        }
    }
}
