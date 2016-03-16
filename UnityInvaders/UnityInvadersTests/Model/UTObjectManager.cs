using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;
using UnityInvaders.Controllers;

namespace UnityInvadersTests.Model
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
