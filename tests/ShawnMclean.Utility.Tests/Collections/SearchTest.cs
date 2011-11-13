using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ShawnMclean.Utility.Collections;

namespace ShawnMclean.Utility.Tests.Collections
{
    [TestFixture]
    public class SearchTest
    {
        [Test]
        public void CountSorted_Returns_Correct_For_Last_Element()
        {
            int expectedCount = 1;
            List<int> list = new List<int> {1,2,3,4,5};
            int searchVal = 5;

            int count = list.CountSorted(searchVal);

            Assert.AreEqual(expectedCount, count);
        }

        [Test]
        public void CountSorted_Returns_Correct_For_First_Element()
        {
            int expectedCount = 1;
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };
            int searchVal = 1;

            int count = list.CountSorted(searchVal);

            Assert.AreEqual(expectedCount, count);
        }

        [Test]
        public void CountSorted_Returns_Correct_For_All_Elements()
        {
            int expectedCount = 4;
            List<int> list = new List<int> { 5,5,5,5 };
            int searchVal = 5;

            int count = list.CountSorted(searchVal);

            Assert.AreEqual(expectedCount, count);
        }
        [Test]
        public void CountSorted_Returns_Minus_1_For_No_Elements()
        {
            int expectedCount = -1;
            List<int> list = new List<int> { 5, 5, 5, 5 };
            int searchVal = 1;

            int count = list.CountSorted(searchVal);

            Assert.AreEqual(expectedCount, count);
        }
    }
}
