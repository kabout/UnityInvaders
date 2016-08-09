using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Interfaces;
using UnityInvaders.Controllers;
using UnityInvaders.Model;
using UnityInvaders.Managers;

namespace UnityInvadersTests.Controllers
{
    [TestClass]
    public class UTDefenseController
    {
        [TestMethod]
        public void Place_Defenses_In_Map()
        {
            IMap map = new Map(200);
            IDifficultController difficultController = new DifficultController(153);
            IDefenseController defenseController = new DefenseController(difficultController, new ObjectManager(difficultController));
            defenseController.PlaceDefenses(map);

            Assert.IsTrue(map.Defenses.Count > 0);
        }

        [TestMethod]
        public void Place_Defenses_In_Map_Empty()
        {
            IMap map = new Map(0);
            IDifficultController difficultController = new DifficultController(153);
            IDefenseController defenseController = new DefenseController(difficultController, new ObjectManager(difficultController));
            defenseController.PlaceDefenses(map);

            Assert.IsTrue(map.Defenses.Count == 0);
        }
    }
}
