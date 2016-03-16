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
        public void Get_Num_Cells_Obstacles()
        {
            IMap map = new Map(200, 200);
            IDifficultController difficultController = new DifficultController();
            int numCells = difficultController.GetNumberCellsOfObstacles(map, DifficultLevel.VeryEasy);
            Assert.AreEqual(numCells, 2000);
            numCells = difficultController.GetNumberCellsOfObstacles(map, DifficultLevel.Easy);
            Assert.AreEqual(numCells, 4000);
            numCells = difficultController.GetNumberCellsOfObstacles(map, DifficultLevel.Normal);
            Assert.AreEqual(numCells, 5000);
            numCells = difficultController.GetNumberCellsOfObstacles(map, DifficultLevel.Difficult);
            Assert.AreEqual(numCells, 6000);
            numCells = difficultController.GetNumberCellsOfObstacles(map, DifficultLevel.VeryDifficult);
            Assert.AreEqual(numCells, 8000);
            numCells = difficultController.GetNumberCellsOfObstacles(map, DifficultLevel.God);
            Assert.AreEqual(numCells, 10000);
        }
        [TestMethod]
        public void Get_Num_Cells_Defenses()
        {
            IMap map = new Map(200, 200);
            IDifficultController difficultController = new DifficultController();
            int numCells = difficultController.GetNumberCellsOfDefenses(map, Constants.DEFENSE_SIZE, DifficultLevel.VeryEasy);
            Assert.AreEqual(numCells, 1000);
            numCells = difficultController.GetNumberCellsOfDefenses(map, Constants.DEFENSE_SIZE, DifficultLevel.Easy);
            Assert.AreEqual(numCells, 3000);
            numCells = difficultController.GetNumberCellsOfDefenses(map, Constants.DEFENSE_SIZE, DifficultLevel.Normal);
            Assert.AreEqual(numCells, 4000);
            numCells = difficultController.GetNumberCellsOfDefenses(map, Constants.DEFENSE_SIZE, DifficultLevel.Difficult);
            Assert.AreEqual(numCells, 5000);
            numCells = difficultController.GetNumberCellsOfDefenses(map, Constants.DEFENSE_SIZE, DifficultLevel.VeryDifficult);
            Assert.AreEqual(numCells, 6000);
            numCells = difficultController.GetNumberCellsOfDefenses(map, Constants.DEFENSE_SIZE, DifficultLevel.God);
            Assert.AreEqual(numCells, 7000);
        }
    }
}
