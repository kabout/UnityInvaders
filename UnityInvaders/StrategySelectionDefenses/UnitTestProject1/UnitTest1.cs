using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using Moq;

namespace UnitTestProject1
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            IStrategySelectionDefenses strategySelectionDefenses = new StrategySelectionDefenses.StrategySelectionDefenses();

            var defenses = new List<IDefense>();
            int maxAses = 100;

            Mock<IDefense> defense1 = new Mock<IDefense>();
            defense1.Setup(x => x.Id).Returns(1);
            defense1.Setup(x => x.Health).Returns(100);
            defense1.Setup(x => x.Damage).Returns(10);
            defense1.Setup(x => x.Dispersion).Returns(1);
            defense1.Setup(x => x.Range).Returns(10);
            defense1.Setup(x => x.Cost).Returns(GetCost(defense1.Object));
            defenses.Add(defense1.Object);

            Mock<IDefense> defense2 = new Mock<IDefense>();
            defense2.Setup(x => x.Id).Returns(2);
            defense2.Setup(x => x.Health).Returns(100);
            defense2.Setup(x => x.Damage).Returns(5);
            defense2.Setup(x => x.Dispersion).Returns(1);
            defense2.Setup(x => x.Range).Returns(10);
            defense2.Setup(x => x.Cost).Returns(GetCost(defense2.Object));
            defenses.Add(defense2.Object);

            Mock<IDefense> defense3 = new Mock<IDefense>();
            defense3.Setup(x => x.Id).Returns(3);
            defense3.Setup(x => x.Health).Returns(80);
            defense3.Setup(x => x.Damage).Returns(5);
            defense3.Setup(x => x.Dispersion).Returns(1);
            defense3.Setup(x => x.Range).Returns(10);
            defense3.Setup(x => x.Cost).Returns(GetCost(defense3.Object));
            defenses.Add(defense3.Object);

            Mock<IDefense> defense4 = new Mock<IDefense>();
            defense4.Setup(x => x.Id).Returns(4);
            defense4.Setup(x => x.Health).Returns(100);
            defense4.Setup(x => x.Damage).Returns(5);
            defense4.Setup(x => x.Dispersion).Returns(1);
            defense4.Setup(x => x.Range).Returns(10);
            defense4.Setup(x => x.Cost).Returns(GetCost(defense4.Object));
            defenses.Add(defense4.Object);

            Mock<IDefense> defense5 = new Mock<IDefense>();
            defense5.Setup(x => x.Id).Returns(5);
            defense5.Setup(x => x.Health).Returns(70);
            defense5.Setup(x => x.Damage).Returns(5);
            defense5.Setup(x => x.Dispersion).Returns(1);
            defense5.Setup(x => x.Range).Returns(10);
            defense5.Setup(x => x.Cost).Returns(GetCost(defense5.Object));
            defenses.Add(defense5.Object);

            List<IDefense> selectedDefenses = strategySelectionDefenses.GetDefenses(defenses, maxAses).ToList();

            Assert.IsTrue(selectedDefenses.Any());
            Assert.IsTrue(selectedDefenses.First().Id == 1);
            Assert.IsTrue(selectedDefenses.Last().Id == 3);
            Assert.IsTrue(selectedDefenses.Count == 4);
        }

        private int GetCost(IDefense defense)
        {
            return (int)Math.Round(defense.Health * 0.2 + defense.Range * 0.3 + defense.Damage * 0.4 + defense.Dispersion * 0.1);
        }
    }
}
