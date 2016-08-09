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
            IDifficultController difficultController = new DifficultController(153);
            IObjectManager objectManager = new ObjectManager(difficultController);
            IDefenseController defenseController = new DefenseController(difficultController, objectManager);
            IMapController mapController = new MapController(defenseController, difficultController, objectManager);
            IMap map = mapController.GetEmptyMap(100);
            mapController.InitMap(map);
            Assert.IsTrue(map.Obstacles.Count > 0);
            Assert.IsTrue(map.Defenses.Count > 0);
        }
    }
}
