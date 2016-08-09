using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Model;
using UnityInvaders.Interfaces;
using System.Collections.Generic;

namespace UnityInvadersTests.Model
{
    [TestClass]
    public class UTMap
    {
        [TestMethod]
        public void Create_Map()
        {
            IMap map = new Map(300);

            Assert.IsTrue(map.Size == 300);
            Assert.IsTrue(map.Obstacles.Count == 0);
        }

        [TestMethod]
        public void Add_Obstacle_Ok()
        {
            IMap map = new Map(300);

            IObstacle obstacle = new Obstacle(1, 10, new Position(12, 12));

            Assert.IsTrue(map.AddObstacle(obstacle));
            Assert.AreEqual(map.Obstacles[0], obstacle);
        }

        [TestMethod]
        public void Add_Obstacle_Over_Width()
        {
            IMap map = new Map(300);

            IObstacle obstacle = new Obstacle(1, 10, new Position(300, 0));

            Assert.IsFalse(map.AddObstacle(obstacle));
            Assert.IsTrue(map.Obstacles.Count == 0);
        }

        [TestMethod]
        public void Add_Obstacle_Over_Margin_Width ()
        {
            IMap map = new Map(300);

            IObstacle obstacle = new Obstacle(1, 10, new Position(290, 0));

            Assert.IsFalse(map.AddObstacle(obstacle));
            Assert.IsTrue(map.Obstacles.Count == 0);
        }

        [TestMethod]
        public void Add_Obstacle_Over_Height()
        {
            IMap map = new Map(300);

            IObstacle obstacle = new Obstacle(1, 10, new Position(0, 300));

            Assert.IsFalse(map.AddObstacle(obstacle));
            Assert.IsTrue(map.Obstacles.Count == 0);
        }

        [TestMethod]
        public void Add_Obstacle_Over_Margin_Height ()
        {
            IMap map = new Map(300);

            IObstacle obstacle = new Obstacle(1, 10, new Position(0, 290));

            Assert.IsFalse(map.AddObstacle(obstacle));
            Assert.IsTrue(map.Obstacles.Count == 0);
        }

        [TestMethod]
        public void Add_Obstacle_Overlap_Other_Obstacle()
        {
            IMap map = new Map(100);

            IObstacle obstacle1 = new Obstacle(1, 10, new Position(12, 12));

            map.AddObstacle(obstacle1);

            IObstacle obstacle2 = new Obstacle(2, 10, new Position(17, 17));

            Assert.IsTrue(map.AddObstacle(obstacle2));
        }

        [TestMethod]
        public void Get_Free_Positions_For_Obstacles()
        {
            IMap map = new Map(100);

            IObstacle obstacle1 = new Obstacle(1, 10, new Position(map.Margin, map.Margin));
            map.AddObstacle(obstacle1);
            List<Position> positions = map.GetFreePositionsForObstacle(10) as List<Position>;

            for(int x = map.Margin; x < map.Size - map.Margin - (obstacle1.Radius * 2); x++)
                for(int y = map.Margin; y < map.Size - map.Margin - (obstacle1.Radius * 2); y++)
                {
                    if (x == obstacle1.Position.X && y == obstacle1.Position.Y)
                        Assert.IsFalse(positions.Exists(p => p.X == x && p.Y == y), string.Format("{0}, {1}", x, y));
                    else
                        Assert.IsTrue(positions.Exists(p => p.X == x && p.Y == y), string.Format("{0}, {1}", x, y));
                }

            IObstacle obstacle2 = new Obstacle(2, 10, new Position(map.Margin + 5, map.Margin + 5));
            map.AddObstacle(obstacle2);
            positions = map.GetFreePositionsForObstacle(10) as List<Position>;

            for (int x = map.Margin; x < map.Size - map.Margin - (obstacle2.Radius * 2); x++)
                for (int y = map.Margin; y < map.Size - map.Margin - (obstacle2.Radius * 2); y++)
                {
                    if ((x == obstacle1.Position.X && y == obstacle1.Position.Y) || (x == obstacle2.Position.X && y == obstacle2.Position.Y))
                        Assert.IsFalse(positions.Exists(p => p.X == x && p.Y == y), string.Format("{0}, {1}", x, y));
                    else
                        Assert.IsTrue(positions.Exists(p => p.X == x && p.Y == y), string.Format("{0}, {1}", x, y));
                }
        }

        [TestMethod]
        public void Get_Free_Positions_For_Defenses()
        {
            //IMap map = new Map(10, 10); 
            //IObstacle obstacle1 = new Obstacle(1, 4, new Position(2, 2));
            //map.AddObstacle(obstacle1);
            //List<Position> positions = map.GetFreePositionsForDefense() as List<Position>;

            //Assert.IsTrue(positions.Count == 0);

            //map = new Map(11, 11);
            //IObstacle obstacle2 = new Obstacle(2, 7, new Position(6, 0));
            //map.AddObstacle(obstacle2);
            //IObstacle obstacle3 = new Obstacle(3, 2, new Position(0, 6));
            //map.AddObstacle(obstacle3);
            //positions = map.GetFreePositionsForDefense() as List<Position>;

            //Assert.IsTrue(positions.Count == 1);
            //Assert.IsTrue(positions[0].X == 0 && positions[0].Y == 0);
        }
    }
}
