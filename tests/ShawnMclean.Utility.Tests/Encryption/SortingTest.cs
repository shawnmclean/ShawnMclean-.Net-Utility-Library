using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ShawnMclean.Utility.Collections;

namespace ShawnMclean.Utility.Tests.Encryption
{
    [TestFixture]
    public class SortingTest
    {

        [Test]
        public void IsSorted_Returns_False_On_Unordered_List()
        {
            bool expected = false;
            IList<int> list = new List<int>{1,2,5,4};

            Assert.AreEqual(expected, list.IsSorted());
        }

        [Test]
        public void IsSorted_Returns_True_On_Ordered_List()
        {
            bool expected = true;
            IList<int> list = new List<int> { 1, 2, 5, 6 };

            Assert.AreEqual(expected, list.IsSorted());
        }
    }
}
