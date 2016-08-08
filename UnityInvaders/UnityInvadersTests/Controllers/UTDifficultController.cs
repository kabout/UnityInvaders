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
            IMap map = new Map(400, 400);
            IDifficultController difficultController = new DifficultController(DifficultLevel.VeryEasy);
            int numCells = difficultController.GetNumberOfObstacles(map);
            Assert.AreEqual(numCells, 160);
            difficultController = new DifficultController(DifficultLevel.Easy);
            numCells = difficultController.GetNumberOfObstacles(map);
            Assert.AreEqual(numCells, 480);
            difficultController = new DifficultController(DifficultLevel.Normal);
            numCells = difficultController.GetNumberOfObstacles(map);
            Assert.AreEqual(numCells, 800);
            difficultController = new DifficultController(DifficultLevel.Difficult);
            numCells = difficultController.GetNumberOfObstacles(map);
            Assert.AreEqual(numCells, 960);
            difficultController = new DifficultController(DifficultLevel.VeryDifficult);
            numCells = difficultController.GetNumberOfObstacles(map);
            Assert.AreEqual(numCells, 1280);
            difficultController = new DifficultController(DifficultLevel.God);
            numCells = difficultController.GetNumberOfObstacles(map);
            Assert.AreEqual(numCells, 1600);
        }
    }
}
