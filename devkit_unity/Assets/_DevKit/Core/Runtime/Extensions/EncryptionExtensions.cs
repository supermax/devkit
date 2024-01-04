using System.Security.Cryptography;
using System.Text;

namespace DevKit.Core.Extensions
{
    public static class EncryptionExtensions
    {
        public static string GenerateMD5(this string input)
        {
            input.ThrowIfNullOrEmpty(nameof(input));

            // Create a new instance of the MD5CryptoServiceProvider object.
            using var md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new StringBuilder to collect the bytes and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
