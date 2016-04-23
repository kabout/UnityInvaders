using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;
using UnityInvaders.Controllers;
using UnityInvaders.Managers;
using System.Collections.Generic;
using System;

namespace UnityInvadersTests.Managers
{
    [TestClass]
    public class UTObjectManager
    {
        [TestMethod]
        public void Generate_Obstacle()
        {
            IMap map = new Map(200, 200);
            IDifficultController difficultController = new DifficultController(DifficultLevel.Easy);
            IObjectManager objectManager = new ObjectManager(difficultController);
            IObstacle obstacle = objectManager.GenerateObstacle(map);
            Assert.IsTrue(map.IsValidPosition(obstacle));
        }

        [TestMethod]
        public void Generate_Obstacles_In_Different_Positions()
        {
            IMap map = new Map(20, 20);
            IDifficultController difficultController = new DifficultController(DifficultLevel.Easy);
            IObjectManager objectManager = new ObjectManager(difficultController);

            List<Position> positions = new List<Position>();

            for (int i = 0; i < 40; i++)
            {
                IObstacle obstacle = objectManager.GenerateObstacle(map);

                if (obstacle == null)
                    return;

                if (positions.Exists(p => p.X == obstacle.Position.X && p.Y == obstacle.Position.Y))
                    throw new Exception("Incorrect Position");

                positions.Add(obstacle.Position);
                map.AddObstacle(obstacle);
            }
        }

        [TestMethod]
        public void Generate_Defense()
        {
            IMap map = new Map(200, 200);
            IDifficultController difficultController = new DifficultController(DifficultLevel.Easy);
            IObjectManager objectManager = new ObjectManager(difficultController);
            IDefense defense = objectManager.GenerateDefense(map);
            Assert.IsTrue(map.IsValidPosition(defense));
        }
    }
}
