using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Interfaces;
using UnityInvaders.Controllers;
using UnityInvaders.Managers;
using UnityInvaders.Model;

namespace UnityInvadersTests.Controllers
{
    [TestClass]
    public class UTMapController
    {
        [TestMethod]
        public void Init_Map_Correct()
        {
            IDifficultController difficultController = new DifficultController();
            IObjectManager objectManager = new ObjectManager(difficultController);
            IDefenseController defenseController = new DefenseController(difficultController);
            IMapController mapController = new MapController(defenseController, difficultController, objectManager);
            IMap map = mapController.GetEmptyMap(300, 400);
            mapController.InitMap(map, DifficultLevel.Normal);
            Assert.IsTrue(map.Obstacles.Count > 0);
            Assert.IsTrue(map.Defenses.Count > 0);
        }
    }
}
