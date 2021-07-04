using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bowling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.Tests
{
    [TestClass()]
    public class FrameRollsTests
    {
        [TestMethod()]
        public void canRollTest()
        {
            FrameRolls frameRolls = new FrameRolls(false);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(0);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(0);
            Assert.IsFalse(frameRolls.canRoll());

            frameRolls = new FrameRolls(false);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(10);
            Assert.IsFalse(frameRolls.canRoll());

            frameRolls = new FrameRolls(false);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(0);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(10);
            Assert.IsFalse(frameRolls.canRoll());

            frameRolls = new FrameRolls(false);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(3);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(7);
            Assert.IsFalse(frameRolls.canRoll());

            frameRolls = new FrameRolls(true);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(0);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(0);
            Assert.IsFalse(frameRolls.canRoll());

            frameRolls = new FrameRolls(true);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(10);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(0);
            Assert.IsFalse(frameRolls.canRoll());

            frameRolls = new FrameRolls(true);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(0);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(10);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(10);
            Assert.IsFalse(frameRolls.canRoll());

            frameRolls = new FrameRolls(true);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(3);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(7);
            Assert.IsTrue(frameRolls.canRoll());
            frameRolls.doRoll(0);
            Assert.IsFalse(frameRolls.canRoll());
        }

        [TestMethod()]
        public void doRollTest()
        {
            FrameRolls frameRolls = new FrameRolls(false);
            Roll roll = frameRolls.doRoll(0);
            Assert.IsNotNull(roll);
            Assert.AreEqual(roll, frameRolls.getRoll(0));
        }

        [TestMethod()]
        public void getRollCountTest()
        {
            FrameRolls frameRolls = new FrameRolls(false);
            Assert.IsTrue(0 == frameRolls.getRollCount());
            frameRolls.doRoll(0);
            Assert.IsTrue(1 == frameRolls.getRollCount());
            frameRolls.doRoll(0);
            Assert.IsTrue(2 == frameRolls.getRollCount());

            frameRolls = new FrameRolls(true);
            Assert.IsTrue(0 == frameRolls.getRollCount());
            frameRolls.doRoll(10);
            Assert.IsTrue(1 == frameRolls.getRollCount());
            frameRolls.doRoll(10);
            Assert.IsTrue(2 == frameRolls.getRollCount());
            frameRolls.doRoll(10);
            Assert.IsTrue(3 == frameRolls.getRollCount());
        }

        [TestMethod()]
        public void getRollTest()
        {
            bool exceptionThrown = false;

            FrameRolls frameRolls = new FrameRolls(false);
            frameRolls.doRoll(0);
            Assert.IsNotNull(frameRolls.getRoll(0));

            try
            {
                frameRolls.getRoll(-1);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Roll index out of bound");
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
            exceptionThrown = false;

            try
            {
                frameRolls.getRoll(1);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Roll index out of bound");
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        public void hasStrikeTest()
        {
            FrameRolls frameRolls = new FrameRolls(false);
            frameRolls.doRoll(10);
            Assert.IsTrue(frameRolls.hasStrike());

            frameRolls = new FrameRolls(false);
            frameRolls.doRoll(0);
            frameRolls.doRoll(10);
            Assert.IsFalse(frameRolls.hasStrike());

            frameRolls = new FrameRolls(false);
            frameRolls.doRoll(3);
            frameRolls.doRoll(7);
            Assert.IsFalse(frameRolls.hasStrike());
        }

        [TestMethod()]
        public void hasSpareTest()
        {
            FrameRolls frameRolls = new FrameRolls(false);
            frameRolls.doRoll(10);
            Assert.IsFalse(frameRolls.hasSpare());

            frameRolls = new FrameRolls(false);
            frameRolls.doRoll(3);
            frameRolls.doRoll(7);
            Assert.IsTrue(frameRolls.hasSpare());

            frameRolls = new FrameRolls(false);
            frameRolls.doRoll(0);
            frameRolls.doRoll(10);
            Assert.IsTrue(frameRolls.hasSpare());

            frameRolls = new FrameRolls(true);
            frameRolls.doRoll(10);
            frameRolls.doRoll(0);
            Assert.IsFalse(frameRolls.hasSpare());
        }

        [TestMethod()]
        public void getLastRollTest()
        {
            FrameRolls frameRolls = new FrameRolls(false);
            frameRolls.doRoll(0);
            Assert.IsNull(frameRolls.getLastRoll());

            frameRolls = new FrameRolls(false);
            frameRolls.doRoll(0);
            frameRolls.doRoll(0);
            Assert.IsNotNull(frameRolls.getLastRoll());
        }

        [TestMethod()]
        public void canExtraRollTest()
        {
            FrameRolls frameRolls = new FrameRolls(false);
            Assert.IsFalse(frameRolls.canExtraRoll());

            frameRolls = new FrameRolls(false);
            frameRolls.doRoll(0);
            Assert.IsFalse(frameRolls.canExtraRoll());

            frameRolls = new FrameRolls(false);
            frameRolls.doRoll(0);
            frameRolls.doRoll(10);
            Assert.IsFalse(frameRolls.canExtraRoll());

            frameRolls = new FrameRolls(false);
            frameRolls.doRoll(3);
            frameRolls.doRoll(7);
            Assert.IsFalse(frameRolls.canExtraRoll());

            frameRolls = new FrameRolls(true);
            Assert.IsFalse(frameRolls.canExtraRoll());

            frameRolls = new FrameRolls(true);
            frameRolls.doRoll(0);
            Assert.IsFalse(frameRolls.canExtraRoll());

            frameRolls = new FrameRolls(true);
            frameRolls.doRoll(0);
            frameRolls.doRoll(10);
            Assert.IsTrue(frameRolls.canExtraRoll());

            frameRolls = new FrameRolls(true);
            frameRolls.doRoll(3);
            frameRolls.doRoll(7);
            Assert.IsTrue(frameRolls.canExtraRoll());

            frameRolls = new FrameRolls(true);
            frameRolls.doRoll(3);
            frameRolls.doRoll(7);
            frameRolls.doRoll(10);
            Assert.IsFalse(frameRolls.canExtraRoll());
        }

        [TestMethod()]
        public void getScoreTest()
        {
            FrameRolls frameRolls = new FrameRolls(false);
            frameRolls.doRoll(0);
            frameRolls.doRoll(0);
            Assert.IsTrue(0 == frameRolls.getScore());

            frameRolls = new FrameRolls(false);
            frameRolls.doRoll(3);
            frameRolls.doRoll(7);
            Assert.IsTrue(10 == frameRolls.getScore());

            frameRolls = new FrameRolls(false);
            frameRolls.doRoll(10);
            Assert.IsTrue(10 == frameRolls.getScore());

            frameRolls = new FrameRolls(true);
            frameRolls.doRoll(10);
            frameRolls.doRoll(10);
            frameRolls.doRoll(10);
            Assert.IsTrue(30 == frameRolls.getScore());
        }
    }
}