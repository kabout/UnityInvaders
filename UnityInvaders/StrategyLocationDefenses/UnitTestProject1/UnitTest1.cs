using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StrategyLocationDefenses;
using System.Collections.Generic;
using Moq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<IObstacle> obstacles = new List<IObstacle>();

            Mock<IObstacle> obstacle1 = new Mock<IObstacle>();
            obstacle1.Setup(x => x.Id).Returns(1);
            obstacle1.Setup(x => x.Radius).Returns(15);
            obstacle1.Setup(x => x.Position).Returns(new Position(20, 0, -20));
            obstacles.Add(obstacle1.Object);

            Mock<IObstacle> obstacle2 = new Mock<IObstacle>();
            obstacle2.Setup(x => x.Id).Returns(2);
            obstacle2.Setup(x => x.Radius).Returns(20);
            obstacle2.Setup(x => x.Position).Returns(new Position(30, 0, -35));
            obstacles.Add(obstacle2.Object);

            Mock<IObstacle> obstacle3 = new Mock<IObstacle>();
            obstacle3.Setup(x => x.Id).Returns(3);
            obstacle3.Setup(x => x.Radius).Returns(15);
            obstacle3.Setup(x => x.Position).Returns(new Position(75, 0, -20));
            obstacles.Add(obstacle3.Object);

            Mock<IObstacle> obstacle4 = new Mock<IObstacle>();
            obstacle4.Setup(x => x.Id).Returns(4);
            obstacle4.Setup(x => x.Radius).Returns(15);
            obstacle4.Setup(x => x.Position).Returns(new Position(75, 0, -70));
            obstacles.Add(obstacle4.Object);

            IStrategyLocationDefenses strategyLocationDefenses = new StrategyLocationDefenses.StrategyLocationDefenses();
            strategyLocationDefenses.InitStrategy(obstacles, 5, 100, 10);
            List<IDefense> defenses = new List<IDefense>();

            IPosition position = strategyLocationDefenses.GetPositionForDefense(defenses);
        }
    }
}
