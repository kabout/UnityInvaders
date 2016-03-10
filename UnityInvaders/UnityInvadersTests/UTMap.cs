using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Model;
using UnityInvaders.Interfaces;

namespace UnityInvadersTests
{
    [TestClass]
    public class UTMap
    {
        [TestMethod]
        public void Create_Map()
        {
            Map map = new Map(300, 200);

            Assert.IsTrue(map.Width == 300);
            Assert.IsTrue(map.Width == 300);
            Assert.IsTrue(map.Obstacles.Count == 0);
        }

        [TestMethod]
        public void Add_Obstacle_Ok()
        {
            Map map = new Map(300, 200);

            IObstacle obstacle = new Obstacle(10, 10, new Position(0, 0));

            Assert.IsTrue(map.AddObstacle(obstacle));
            Assert.AreEqual(map.Obstacles[0], obstacle);
        }

        [TestMethod]
        public void Add_Obstacle_Over_Width()
        {
            Map map = new Map(300, 200);

            IObstacle obstacle = new Obstacle(10, 10, new Position(300, 0));

            Assert.IsFalse(map.AddObstacle(obstacle));
            Assert.IsTrue(map.Obstacles.Count == 0);
        }

        [TestMethod]
        public void Add_Obstacle_Over_Height()
        {
            Map map = new Map(300, 200);

            IObstacle obstacle = new Obstacle(10, 10, new Position(0, 200));

            Assert.IsFalse(map.AddObstacle(obstacle));
            Assert.IsTrue(map.Obstacles.Count == 0);
        }

        [TestMethod]
        public void Add_Obstacle_Overlap_Other_Obstacle()
        {
            Map map = new Map(300, 200);

            IObstacle obstacle1 = new Obstacle(10, 10, new Position(0, 0));

            map.AddObstacle(obstacle1);

            IObstacle obstacle2 = new Obstacle(10, 10, new Position(5, 5));

            Assert.IsFalse(map.AddObstacle(obstacle2));
        }
    }
}
