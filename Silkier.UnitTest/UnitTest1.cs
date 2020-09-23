using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Silkier.Extensions;
using System;
using System.Security.Permissions;

namespace Silkier.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        List<string> list = new List<string>();
        [TestInitialize]
        public   void   TestInitialize()
        {
            for (int i = 0; i < 30000000; i++)
            {
                list.Add(i.ToString());
            }
        }
        [TestMethod]
        public void TestSplitMethod1()
        {
            var lstx = list.Split(10000);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestPartitionMethod1()
        {
            var lstx = list.Partition(10000);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestChunkMethod1()
        {
            var lstx = list.Chunk(10000);
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void TestSplit2Method1()
        {
            var lstx = list.Split2(10000);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestSplit3Method1()
        {
            var lstx = list.Split3(10000);
            Assert.IsTrue(true);
        }
    }
}
