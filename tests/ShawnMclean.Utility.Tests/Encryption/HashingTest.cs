using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ShawnMclean.Utility.Tests.Encryption
{
    [TestFixture]
    public class HashingTest
    {
        [Test]
        public void Salt_Format_Returns_Iteration_At_Start()
        {
            int iterations = 125;
            int saltSize = 10;

            string salt = Hashing.GenerateSalt(iterations, saltSize);

            var i = salt.IndexOf('.');
            var iterationsActual = int.Parse(salt.Substring(0, i));
            salt = salt.Substring(i + 1);
            Assert.AreEqual(iterations, iterationsActual);
        }

        [Test]
        public void Salt_Size_Is_Correct()
        {
            int iterations = 5;
            int saltSize = 12;

            string salt = Hashing.GenerateSalt(iterations, saltSize);

            var i = salt.IndexOf('.');
            salt = salt.Substring(i + 1);

            //convert to bytes
            byte[] hashWithSaltBytes = Convert.FromBase64String(salt);

            Assert.AreEqual(saltSize, hashWithSaltBytes.Length);
        }

        #region PBKDF2 Hashing Tests

        [Test]
        public void PBKDF2_Hash_Throws_FormatException_WithOut_Iteration()
        {
            string salt = "fsdfsdfwer";
            string plainText = "Password";

            Assert.Throws<FormatException>(() => Hashing.ComputeHash(plainText, salt, HashAlgorithmType.PBKDF2));
        }

        [Test]
        public void PBKDF2_Hash_Throws_ArgumentOutOfRangException_When_Iteration_Is_Less_Than_One()
        {
            string salt = "0.testhguser";
            string plainText = "Password";

            Assert.Throws<ArgumentOutOfRangeException>(() => Hashing.ComputeHash(plainText, salt, HashAlgorithmType.PBKDF2));
        }

        [Test]
        public void PBKDF2_Hashes_To_The_Same_Thing_Twice()
        {
            string salt = "5.testhguser";
            string plainText = "Password";

            string firstHash = Hashing.ComputeHash(plainText, salt, HashAlgorithmType.PBKDF2);
            string secondHash = Hashing.ComputeHash(plainText, salt, HashAlgorithmType.PBKDF2);

            Assert.AreEqual(firstHash, secondHash);
        }

        [Test]
        public void PBKDF2_Hashes_To_512_Bits()
        {
            
            int expectedSize = 64;
            string salt = "5.testhguser";
            string plainText = "Password";

            string firstHash = Hashing.ComputeHash(plainText, salt, HashAlgorithmType.PBKDF2);

            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(firstHash);

            Assert.AreEqual(expectedSize, hashWithSaltBytes.Length);
        }

        [Test]
        public void PBKDF2_Hashes_To_The_Different_Hash_On_Different_PlainText()
        {
            string salt = "5.testhguser";
            string plainText1 = "Password";
            string plainText2 = "Password1";

            string firstHash = Hashing.ComputeHash(plainText1, salt, HashAlgorithmType.PBKDF2);
            string secondHash = Hashing.ComputeHash(plainText2, salt, HashAlgorithmType.PBKDF2);

            Assert.AreNotEqual(firstHash, secondHash);
        }

        [Test]
        public void PBKDF2_Hashes_To_The_Different_Hash_On_Different_Salt()
        {
            string salt1 = "5.testhguser";
            string salt2= "5.testhguser1";
            string plainText = "Password";

            string firstHash = Hashing.ComputeHash(plainText, salt1, HashAlgorithmType.PBKDF2);
            string secondHash = Hashing.ComputeHash(plainText, salt2, HashAlgorithmType.PBKDF2);

            Assert.AreNotEqual(firstHash, secondHash);
        }
        #endregion

    }
}
