using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvadersTests.Model
{
    [TestClass]
    public class UTObjectGenerator
    {
        [TestMethod]
        public void Generate_Obstacle()
        {
            IMap map = new Map(200, 200);
            IObjectGenerator objectGenerator = new ObjectGenerator();
            IObstacle obstacle = objectGenerator.GenerateObstacle(60, map);
            Assert.IsTrue(map.IsValidPosition(obstacle));
        }
        [TestMethod]
        public void Generate_Defense()
        {
            IMap map = new Map(200, 200);
            IObjectGenerator objectGenerator = new ObjectGenerator();
            IDefense defense = objectGenerator.GenerateDefense(Constants.DEFENSE_SIZE, DifficultLevel.Easy, map);
            Assert.IsTrue(map.IsValidPosition(defense));
        }
    }
}
