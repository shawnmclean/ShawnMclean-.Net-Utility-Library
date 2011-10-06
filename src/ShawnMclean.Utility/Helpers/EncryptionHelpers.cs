using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ShawnMclean.Utility.Helpers
{
    public static class EncryptionHelpers
    {
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
