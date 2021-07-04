using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bowling.Tests
{
    [TestClass()]
    public class RollTests
    {
        [TestMethod()]
        public void RollTest()
        {
            bool exceptionThrown = false; 
            try
            {
                Roll roll = new Roll(-1, 10);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Number of knocked down pins must be between 0 and 10");
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
            exceptionThrown = false;

            try
            {
                Roll roll = new Roll(11, 10);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Number of knocked down pins must be between 0 and 10");
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            Assert.IsNotNull(new Roll(0, 10));
        }

        [TestMethod()]
        public void getKnockedDownPinsTest()
        {
            for (int i = 0; i < 11; i++)
            {
                Roll roll = new Roll(i, 10);
                Assert.IsTrue(i == roll.getKnockedDownPins());
            }
        }

        [TestMethod()]
        public void getScoreTest()
        {
            for (int i = 0; i < 11; i++)
            {
                Roll roll = new Roll(i, 10);
                Assert.IsTrue(i == roll.getScore());
            }
        }

        [TestMethod()]
        public void knockedDownAllPinsTest()
        {
            Roll roll;
            for (int i = 0; i < 10; i++)
            {
                roll = new Roll(i, 10);
                Assert.IsFalse(roll.knockedDownAllPins());
            }
            roll = new Roll(10, 10);
            Assert.IsTrue(roll.knockedDownAllPins());
        }
    }
}