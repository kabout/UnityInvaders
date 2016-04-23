using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Interfaces;
using UnityInvaders.Transforms;
using UnityInvaders.Managers;
using UnityInvaders.Controllers;

namespace UnityInvadersTests.Controllers
{
    [TestClass]
    public class UTGameController
    {
        [TestMethod]
        public void TestMethod1()
        {
            IDifficultController difficultController = new DifficultController(UnityInvaders.Model.DifficultLevel.VeryEasy);
            IDefenseController defenseController = new DefenseController(difficultController);
            IObjectManager objectManager = new ObjectManager(difficultController);
            IMapController mapController = new MapController(defenseController, difficultController, objectManager);
            IMap map = mapController.GetEmptyMap(100, 100);
            mapController.InitMap(map);
            //MapToUnity mapToUnity = new MapToUnity(floor, obstacle);
            //UnityMap unityMap = mapToUnity.Convert(map);
        }
    }
}
