using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace DevKit.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if string is null or empty
        /// </summary>
        /// <param name="source">The source string</param>
        /// <returns>"True" if string is null or empty</returns>
        public static bool IsNullOrEmpty(this string source)
        {
            var isEmpty = string.IsNullOrEmpty(source);
            return isEmpty;
        }

        /// <summary>
        /// Returns <see cref="string"/> in JSON field name format
        /// </summary>
        /// <param name="propName">Source property name</param>
        /// <returns><see cref="string"/> in JSON field name format</returns>
        public static string ToJsonPropName(this string propName)
        {
            if (propName.IsNullOrEmpty() || propName.Length < 2)
            {
                return propName;
            }

            propName = propName.Trim();
            propName = propName.Replace("_", string.Empty);
            var firstChar = propName[0].ToString().ToLowerInvariant();
            propName = propName.Remove(0, 1);
            var jPropName = propName.Insert(0, firstChar);
            return jPropName;
        }

        /// <summary>
        /// The Input String (UTF8) is sealed with a MD5-128-Bit-Hash and then Crypted to a Base64 String
        /// </summary>
        /// <param name="originalText"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptToBase64(this string originalText, string password)
        {
            using var md = MD5.Create();
            var userBytes = Encoding.UTF8.GetBytes(originalText); // UTF8 saves Space
            var userHash = md.ComputeHash(userBytes);
            SymmetricAlgorithm crypt = Aes.Create(); // (Default: AES-CCM (Counter with CBC-MAC))
            crypt.Key = md.ComputeHash(Encoding.UTF8.GetBytes(password)); // MD5: 128 Bit Hash
            crypt.IV = new byte[16]; // by Default. IV[] to 0.. is OK simple crypt
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(userBytes, 0, userBytes.Length); // User Data
            cryptoStream.Write(userHash, 0, userHash.Length); // Add HASH
            cryptoStream.FlushFinalBlock();
            var resultString = Convert.ToBase64String(memoryStream.ToArray());
            return resultString;
        }

        /// <summary>
        /// Try to get original (decrypted) String. Password (and Base64-format) must be correct
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DecryptFromBase64(this string encryptedText, string password)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            using var crypt = Aes.Create();
            using var md = MD5.Create();
            crypt.Key = md.ComputeHash(Encoding.UTF8.GetBytes(password));
            crypt.IV = new byte[16];
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            cryptoStream.FlushFinalBlock();
            var allBytes = memoryStream.ToArray();
            var userLen = allBytes.Length - 16;
            if (userLen < 0)
            {
                throw new InvalidDataException("Invalid Length");   // No Hash?
            }
            var userHash = new byte[16];
            Array.Copy(allBytes, userLen, userHash, 0, 16); // Get the 2 Hashes
            var decryptHash = md.ComputeHash(allBytes, 0, userLen);
            if (!userHash.SequenceEqual(decryptHash))
            {
                throw new InvalidDataException("Invalid Hash");
            }
            var resultString = Encoding.UTF8.GetString(allBytes,0, userLen);
            return resultString;
        }
    }
}
