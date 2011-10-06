using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ShawnMclean.Utility.Text
{
    /// <summary>
    /// Used to generate a random string
    /// </summary>
    public enum StringType
    {
        /// <summary>
        /// Consist of all possible english characters
        /// </summary>
        AlphaNumericSymbol,
        /// <summary>
        /// A-Z, a-z and 1-10
        /// </summary>
        AlphaNumeric
    }

    public static class StringHelpers
    {
        /// <summary>
        /// Generate a random string of characters
        /// </summary>
        /// <param name="length">length of string</param>
        /// <param name="type">type of string to be generated</param>
        /// <returns></returns>
        public static string GenerateRandomString(int length, StringType type)
        {
            switch (type)
            {
                case StringType.AlphaNumeric:
                    string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
                    char[] chars = new char[length];
                    Random rd = new Random();

                    for (int i = 0; i < length; i++)
                    {
                        chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                    }

                    return new string(chars);
                    break;

                case StringType.AlphaNumericSymbol:
                    //Generate a cryptographic random number.
                    var rng = new RNGCryptoServiceProvider();
                    var buff = new byte[length];
                    rng.GetBytes(buff);

                    rng.Dispose();

                    // Return a Base64 string representation of the random number.
                    return Convert.ToBase64String(buff);
                    break;
                default:
                    throw new ArgumentException("Type not supported");
            }
        }
    }
}
