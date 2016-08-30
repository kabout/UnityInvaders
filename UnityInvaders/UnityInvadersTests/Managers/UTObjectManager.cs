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
        //[TestMethod]
        //public void Generate_Obstacle()
        //{
        //    IMap map = new Map(200);
        //    IDifficultController difficultController = new DifficultController(153);
        //    IObjectManager objectManager = new ObjectManager(difficultController);
        //    IObstacle obstacle = objectManager.GenerateObstacle(map, Constants.MIN_OBSTACLE_RADIUS, Constants.MAX_OBSTACLE_RADIUS);
        //    Assert.IsTrue(map.IsValidPosition(obstacle));
        //}

        //[TestMethod]
        //public void Generate_Defense()
        //{
        //    IMap map = new Map(200);
        //    IDifficultController difficultController = new DifficultController(153);
        //    IObjectManager objectManager = new ObjectManager(difficultController);
        //    IDefense defense = objectManager.GenerateDefense(map, Constants.DEFAULT_DEFENSE_RADIO);
        //    Assert.IsTrue(map.IsValidPosition(defense));
        //}
    }
}
