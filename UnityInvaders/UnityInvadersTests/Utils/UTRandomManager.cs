using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Utils;

namespace UnityInvadersTests.Utils
{
    [TestClass]
    public class UTRandomManager
    {
        [TestMethod]
        public void Random_Float_Number_3_5()
        {
            for (int i = 0; i < 10000; i++)
            {
                float num = RandomManager.GetRandomNumber(3, 5);
                Assert.IsTrue(num >= 3);
                Assert.IsTrue(num < 5);
            }
        }

        [TestMethod]
        public void Random_Float_Number_Less_10()
        {
            for (int i = 0; i < 10000; i++)
            {
                float num = RandomManager.GetRandomNumber(10);
                Assert.IsTrue(num >= 0);
                Assert.IsTrue(num < 10);
            }
        }

        [TestMethod]
        public void Random_Float_Number_0_1()
        {
            for (int i = 0; i < 10000; i++)
            {
                float num = RandomManager.GetRandomFloatNumber();
                Assert.IsTrue(num >= 0);
                Assert.IsTrue(num < 1);
            }
        }
        [TestMethod]
        public void Random_Int_Number_3_5()
        {
            for (int i = 0; i < 10000; i++)
            {
                int num = RandomManager.GetRandomNumber(3, 5);
                Assert.IsTrue(num >= 3);
                Assert.IsTrue(num < 5);
            }
        }

        [TestMethod]
        public void Random_Int_Number_Less_10()
        {
            for (int i = 0; i < 10000; i++)
            {
                int num = RandomManager.GetRandomNumber(10);
                Assert.IsTrue(num >= 0);
                Assert.IsTrue(num < 10);
            }
        }

        [TestMethod]
        public void Random_Int_Number_0_1()
        {
            for (int i = 0; i < 10000; i++)
            {
                int num = RandomManager.GetRandomIntNumber();
                Assert.IsTrue(num >= 0);
                Assert.IsTrue(num < 1);
            }
        }
    }
}
