using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ShawnMclean.Utility
{
    public enum HashAlgorithmType
    {
        SHA1,
        SHA256,
        SHA512
    }

    public class Hashing
    {

        public static string ComputeHash(string plainText, byte[] saltBytes, HashAlgorithmType hashAlgorithm)
        {
            //convert the plain text into a byte array
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            // Because we support multiple hashing algorithms, we must define
            // hash object as a common (abstract) base class. We will specify the
            // actual hashing algorithm class later during object creation.
            HashAlgorithm hash;

            // Initialize appropriate hashing algorithm class.
            switch (hashAlgorithm)
            {
                case HashAlgorithmType.SHA1:
                    hash = new SHA1Managed();
                    break;
                case HashAlgorithmType.SHA256:
                    hash = new SHA256Managed();
                    break;
                case HashAlgorithmType.SHA512:
                    hash = new SHA512Managed();
                    break;

                default:
                    hash = new MD5CryptoServiceProvider();
                    break;
            }

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        /// <summary>
        /// Hash a text with SHA-512 bit encryption that also accepts a salt
        /// </summary>
        /// <param name="text">string to be hashed</param>
        /// <param name="salt">salt to be added to the string</param>
        /// <returns></returns>
        public static string HashSHA512(string text, string salt)
        {
            string saltAndPwd = String.Concat(text, salt);

            var ae = new ASCIIEncoding();
            byte[] hashValue, messageBytes = ae.GetBytes(saltAndPwd);
            var sHhash = new SHA512Managed();

            hashValue = sHhash.ComputeHash(messageBytes);

            sHhash.Dispose();

            return ae.GetString(hashValue);
        }
    }
}
