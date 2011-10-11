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
        SHA512,
        MD5,
        PBKDF2
    }

    public class Hashing
    {

        /// <summary>
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, a random salt
        /// is generated and appended to the plain text. This salt is stored at
        /// the end of the hash value, so it can be used later for hash
        /// </summary>
        /// <param name="plainText">Plain text value to be hashed. Throws ArgumentNullException if null.</param>
        /// <param name="salt">Salt to be added to the hash. If hashAlgorithm is PBKDF2, salt must be in the 
        /// format: '{iterations}.{saltstring}'. FormatException thrown if not in that format. Throws ArgumentNullException if null.</param>
        /// <param name="hashAlgorithm">The algorithm to be used. Throws ArgumentOutOfRangeException if some unknown value is passed.</param>
        /// <returns>Hash value formatted as a base64-encoded string.</returns>
        public static string ComputeHash(string plainText, string salt, HashAlgorithmType hashAlgorithm)
        {
            if (plainText == null) throw new ArgumentNullException("plainText");
            if (salt == null) throw new ArgumentNullException("salt");

            //convert the plain text into a byte array
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            //convert the salt into a byte array
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

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
                case HashAlgorithmType.MD5:
                    hash = new MD5CryptoServiceProvider();
                    break;
                case HashAlgorithmType.PBKDF2:
                    //get the position of the . that splits the string
                    var i = salt.IndexOf('.');
                    
                    //Throw exception if i = -1
                    if (i < 1)
                        throw new FormatException("salt is not in the expected format of '{int}.{string}'");

                    var iters = int.Parse(salt.Substring(0, i), System.Globalization.NumberStyles.HexNumber);

                    //Throws exception if iteration is is less than 1
                    if (iters < 1)
                        throw new ArgumentOutOfRangeException("salt","Iterations in salt must be greater than 0.");


                    salt = salt.Substring(i + 1);

                    using (var pbkdf2 = new Rfc2898DeriveBytes(plainText, saltBytes, iters))
                    {
                        var key = pbkdf2.GetBytes(64);
                        return Convert.ToBase64String(key);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("hashAlgorithm");
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
        /// Convenience wrapper around Random to grab a new salt value.
        /// Treat this value as opaque, as it captures iterations.
        /// </summary>
        public static string GenerateSalt(int explicitIterations, int saltSize)
        {
            var rand = RandomNumberGenerator.Create();

            var ret = new byte[saltSize];

            rand.GetBytes(ret);

            return explicitIterations + "." + Convert.ToBase64String(ret);
        }

    }
}
