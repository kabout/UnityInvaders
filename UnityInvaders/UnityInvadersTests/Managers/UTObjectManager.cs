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
            IDifficultController difficultController = new DifficultController();
            IObjectManager objectManager = new ObjectManager(difficultController);
            IObstacle obstacle = objectManager.GenerateObstacle(60, map);
            Assert.IsTrue(map.IsValidPosition(obstacle));
        }

        [TestMethod]
        public void Generate_Obstacles_In_Different_Positions()
        {
            IMap map = new Map(200, 200);
            IDifficultController difficultController = new DifficultController();
            IObjectManager objectManager = new ObjectManager(difficultController);

            List<IObstacle> obstacles = new List<IObstacle>();

            for (int i = 0; i < 1000; i++)
            {
                IObstacle obstacle = objectManager.GenerateObstacle(60, map);

                if (obstacle == null)
                    return;

                if (obstacles.Exists(x => x.Position.X == obstacle.Position.X && x.Position.Y == obstacle.Position.Y))
                    throw new Exception("Incorrect Position");

                obstacles.Add(obstacle);
            }
        }

        [TestMethod]
        public void Generate_Defense()
        {
            IMap map = new Map(200, 200);
            IDifficultController difficultController = new DifficultController();
            IObjectManager objectManager = new ObjectManager(difficultController);
            IDefense defense = objectManager.GenerateDefense(Constants.DEFENSE_SIZE, DifficultLevel.Easy, map);
            Assert.IsTrue(map.IsValidPosition(defense));
        }
    }
}
