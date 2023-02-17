using LockStation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestStation
{
    [TestClass]
    public class ModelTest
    {
        MainModel Model = new MainModel();
        [TestMethod]
        public void TestStringTime()
        {
            Model.Total = 185;
            Model.Elapsed = 0;
            Assert.AreEqual("03:05", Model.TimeString);
            Model.Tick();
            Assert.AreEqual("03:04", Model.TimeString);
        }

        [TestMethod]
        public void TestStateClass()
        {
            StateWriter.SaveState(1, 15);
            int vl=StateWriter.LoadState();
            Assert.AreEqual(15, vl);
        }

        [DataRow(0,3)]
        [TestMethod()]
        public void TestConfiguration(int day,int exp)
        {
            int vl=StateWriter.LoadTimeForDay(day);
            Assert.AreEqual(exp, vl);
        }
    }
}
