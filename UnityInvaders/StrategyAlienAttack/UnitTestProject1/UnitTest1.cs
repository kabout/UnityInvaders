using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            List<IDefense> defenses = new List<IDefense>();

            Mock<IObstacle> obstacle1 = new Mock<IObstacle>();
            obstacle1.Setup(x => x.Id).Returns(1);
            obstacle1.Setup(x => x.Radius).Returns(15);
            obstacle1.Setup(x => x.Position).Returns(new Position(20, 0, 20));
            obstacles.Add(obstacle1.Object);

            Mock<IObstacle> obstacle2 = new Mock<IObstacle>();
            obstacle2.Setup(x => x.Id).Returns(2);
            obstacle2.Setup(x => x.Radius).Returns(20);
            obstacle2.Setup(x => x.Position).Returns(new Position(30, 0, 35));
            obstacles.Add(obstacle2.Object);

            Mock<IObstacle> obstacle3 = new Mock<IObstacle>();
            obstacle3.Setup(x => x.Id).Returns(3);
            obstacle3.Setup(x => x.Radius).Returns(15);
            obstacle3.Setup(x => x.Position).Returns(new Position(75, 0, 20));
            obstacles.Add(obstacle3.Object);

            Mock<IObstacle> obstacle4 = new Mock<IObstacle>();
            obstacle4.Setup(x => x.Id).Returns(4);
            obstacle4.Setup(x => x.Radius).Returns(15);
            obstacle4.Setup(x => x.Position).Returns(new Position(75, 0, 70));
            obstacles.Add(obstacle4.Object);

            Mock<IPosition> alienPosition = new Mock<IPosition>();
            alienPosition.Setup(p => p.X).Returns(95);
            alienPosition.Setup(p => p.Z).Returns(15);

            Mock<IPosition> defensePosition1 = new Mock<IPosition>();
            defensePosition1.Setup(p => p.X).Returns(35);
            defensePosition1.Setup(p => p.Z).Returns(75);

            Mock<IPosition> defensePosition2 = new Mock<IPosition>();
            defensePosition2.Setup(p => p.X).Returns(45);
            defensePosition2.Setup(p => p.Z).Returns(65);

            Mock<IPosition> defensePosition3 = new Mock<IPosition>();
            defensePosition3.Setup(p => p.X).Returns(45);
            defensePosition3.Setup(p => p.Z).Returns(85);

            Mock<IDefense> defense1 = new Mock<IDefense>();
            defense1.Setup(d => d.Position).Returns(defensePosition1.Object);
            defense1.Setup(d => d.Radius).Returns(5);
            defenses.Add(defense1.Object);

            Mock<IDefense> defense2 = new Mock<IDefense>();
            defense2.Setup(d => d.Position).Returns(defensePosition2.Object);
            defense2.Setup(d => d.Radius).Returns(5);
            defenses.Add(defense2.Object);

            Mock<IDefense> defense3 = new Mock<IDefense>();
            defense3.Setup(d => d.Position).Returns(defensePosition3.Object);
            defense3.Setup(d => d.Radius).Returns(5);
            defenses.Add(defense3.Object);

            IStrategyAlienAttack strategyAlienAttack = new StrategyAlienAttack.StrategyAlienAttack();
            List<IPosition> positions = strategyAlienAttack.CalculatePath(alienPosition.Object, defensePosition1.Object, obstacles, defenses, 100, 10);
        }
    }
}
