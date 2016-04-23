using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Interfaces;
using UnityInvaders.Controllers;
using UnityInvaders.Model;

namespace UnityInvadersTests.Controllers
{
    [TestClass]
    public class UTDifficultController
    {
        [TestMethod]
        public void Get_Num_Obstacles()
        {
            IMap map = new Map(200, 200);
            IDifficultController difficultController = new DifficultController(DifficultLevel.VeryEasy);
            int numCells = difficultController.GetNumberOfObstacles(map);
            Assert.AreEqual(numCells, 20);
            difficultController = new DifficultController(DifficultLevel.Easy);
            numCells = difficultController.GetNumberOfObstacles(map);
            Assert.AreEqual(numCells, 40);
            difficultController = new DifficultController(DifficultLevel.Normal);
            numCells = difficultController.GetNumberOfObstacles(map);
            Assert.AreEqual(numCells, 50);
            difficultController = new DifficultController(DifficultLevel.Difficult);
            numCells = difficultController.GetNumberOfObstacles(map);
            Assert.AreEqual(numCells, 60);
            difficultController = new DifficultController(DifficultLevel.VeryDifficult);
            numCells = difficultController.GetNumberOfObstacles(map);
            Assert.AreEqual(numCells, 80);
            difficultController = new DifficultController(DifficultLevel.God);
            numCells = difficultController.GetNumberOfObstacles(map);
            Assert.AreEqual(numCells, 100);
        }
    }
}
