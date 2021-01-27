using System;
using System.Text;

namespace Pokeshop.Api.Common
{
    public static class PasswordHelper
    {
        public static string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            return Convert.ToBase64String(passwordBytes);
        }

        public static string Decrypt(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData)) return "";

            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length);

            return result;
        }
    }
}
