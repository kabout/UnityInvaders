using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Model;
using UnityInvaders.Interfaces;
using System.Collections.Generic;
using UnityInvaders.Controllers;
using UnityInvaders.Managers;

namespace UnityInvadersTests.Model
{
    [TestClass]
    public class UTMap
    {
        [TestMethod]
        public void Create_Map()
        {
            IMap map = new Map(300, 200);

            Assert.IsTrue(map.Width == 300);
            Assert.IsTrue(map.Width == 300);
            Assert.IsTrue(map.Obstacles.Count == 0);
        }

        [TestMethod]
        public void Add_Obstacle_Ok()
        {
            IMap map = new Map(300, 200);

            IObstacle obstacle = new Obstacle(1, 10, 10, new Position(0, 0));

            Assert.IsTrue(map.AddObstacle(obstacle));
            Assert.AreEqual(map.Obstacles[0], obstacle);
        }

        [TestMethod]
        public void Add_Obstacle_Over_Width()
        {
            IMap map = new Map(300, 200);

            IObstacle obstacle = new Obstacle(1, 10, 10, new Position(300, 0));

            Assert.IsFalse(map.AddObstacle(obstacle));
            Assert.IsTrue(map.Obstacles.Count == 0);
        }

        [TestMethod]
        public void Add_Obstacle_Over_Height()
        {
            IMap map = new Map(300, 200);

            IObstacle obstacle = new Obstacle(1, 10, 10, new Position(0, 200));

            Assert.IsFalse(map.AddObstacle(obstacle));
            Assert.IsTrue(map.Obstacles.Count == 0);
        }

        [TestMethod]
        public void Add_Obstacle_Overlap_Other_Obstacle()
        {
            IMap map = new Map(300, 200);

            IObstacle obstacle1 = new Obstacle(1, 10, 10, new Position(0, 0));

            map.AddObstacle(obstacle1);

            IObstacle obstacle2 = new Obstacle(2, 10, 10, new Position(5, 5));

            Assert.IsTrue(map.AddObstacle(obstacle2));
        }

        [TestMethod]
        public void Get_Free_Positions_For_Obstacles()
        {
            IMap map = new Map(30, 20);

            IObstacle obstacle1 = new Obstacle(1, 10, 10, new Position(0, 0));
            map.AddObstacle(obstacle1);
            List<Position> positions = map.GetFreePositionsForObstacle(10, 10) as List<Position>;

            for (int x = 0; x < map.Width - obstacle1.Width; x++)
                for (int y = 0; y < map.Height - obstacle1.Height; y++)
                {
                    if (x == 0 && y == 0)
                        Assert.IsFalse(positions.Exists(p => p.X == x && p.Y == y), string.Format("{0}, {1}", x, y));
                    else
                        Assert.IsTrue(positions.Exists(p => p.X == x && p.Y == y), string.Format("{0}, {1}", x, y));
                }

            IObstacle obstacle2 = new Obstacle(2, 10, 10, new Position(5, 5));
            map.AddObstacle(obstacle2);
            positions = map.GetFreePositionsForObstacle(10, 10) as List<Position>;

            for (int x = 0; x < map.Width - obstacle2.Width; x++)
                for (int y = 0; y < map.Height - obstacle2.Height; y++)
                {
                    if ((x == 0 && y == 0) || (x == 5 && y == 5))
                        Assert.IsFalse(positions.Exists(p => p.X == x && p.Y == y), string.Format("{0}, {1}", x, y));
                    else
                        Assert.IsTrue(positions.Exists(p => p.X == x && p.Y == y), string.Format("{0}, {1}", x, y));
                }
        }

        [TestMethod]
        public void Get_Free_Positions_For_Defenses()
        {
            IMap map = new Map(10, 10);
            IObstacle obstacle1 = new Obstacle(1, 4, 4, new Position(2, 2));
            map.AddObstacle(obstacle1);
            List<Position> positions = map.GetFreePositionsForDefense() as List<Position>;

            Assert.IsTrue(positions.Count == 0);

            map = new Map(11, 11);
            IObstacle obstacle2 = new Obstacle(2, 2, 7, new Position(5, 0));
            map.AddObstacle(obstacle2);
            IObstacle obstacle3 = new Obstacle(3, 7, 2, new Position(0, 5));
            map.AddObstacle(obstacle3);
            positions = map.GetFreePositionsForDefense() as List<Position>;

            Assert.IsTrue(positions.Count == 1);
            Assert.IsTrue(positions[0].X == 0 && positions[0].Y == 0);
        }

        [TestMethod]
        public void PrintMap()
        {
            IDifficultController difficultController = new DifficultController(DifficultLevel.Normal);
            IObjectManager objectManager = new ObjectManager(difficultController);
            IMapController mapController = new MapController(new DefenseController(difficultController, objectManager), difficultController, objectManager);
            IMap map = mapController.GetEmptyMap(100,100);
            mapController.InitMap(map);

            string mapString = map.PrintMap();

        }
    }
}
