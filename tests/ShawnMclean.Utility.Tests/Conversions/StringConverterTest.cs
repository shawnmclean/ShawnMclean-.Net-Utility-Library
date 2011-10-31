using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ShawnMclean.Utility.Conversions;

namespace ShawnMclean.Utility.Tests.Conversions
{
    [TestFixture]
    public class StringConverterTest
    {
        [Test]
        public void ThousandsToK_Converts_1500_To_1_5K()
        {
            string expected = "1.5K";

            string actual = 1500.ThousandsToK();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThousandsToK_Converts_100_To_100()
        {
            string expected = "100";

            string actual = 100.ThousandsToK();

            Assert.AreEqual(expected, actual);
        }
    }
}
