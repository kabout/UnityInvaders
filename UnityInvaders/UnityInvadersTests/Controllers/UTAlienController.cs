using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Interfaces;
using UnityInvaders.Controllers;
using UnityInvaders.Managers;
using UnityInvaders.Model;
using System.Drawing;
using UnityInvaders.Utils;
using System.Collections.Generic;

namespace UnityInvadersTests.Controllers
{
    [TestClass]
    public class UTAlienController
    {
        [TestMethod]
        public void Check_Path_Aliens()
        {
            IDifficultController difficultController = new DifficultController(600);
            IObjectManager objectManager = new ObjectManager(difficultController);
            IDefenseController defenseController = new DefenseController(difficultController, objectManager);
            IMapController mapController = new MapController(defenseController, difficultController, objectManager);
            IMap map = mapController.GetEmptyMap(300);
            mapController.InitMap(map);

            List<Position> sourcePositions = map.GetFreePositionsForAlien().ToList();
            int index = RandomManager.GetRandomNumber(0, sourcePositions.Count - 1);
            Position sourcePosition = sourcePositions[index];

            List<Position> targetPositions = map.Defenses.ToList().ConvertAll(x => x.Position);
            index = RandomManager.GetRandomNumber(0, targetPositions.Count - 1);
            Position targetPosition = targetPositions[index];
            targetPosition.X--;
            targetPosition.Y--;

            Bitmap image = ExportMapToImage.Instance.ConvertToBitMap(map.GetMap(), map.Size, sourcePosition, targetPosition);
            image.Save(@"C:\temp\mapSourceTarget.bmp");

            IAlienController alienController = new AlienController();
            List<Position> path = alienController.CalculePath(sourcePosition, targetPosition, map);

            image = ExportMapToImage.Instance.ConvertToBitMap(map.GetMap(), map.Size, path);
            image.Save(@"C:\temp\mapWithPath.bmp");
        }
        [TestMethod]
        public void Check_Path_Aliens2 ()
        {
            IDifficultController difficultController = new DifficultController(600);
            IObjectManager objectManager = new ObjectManager(difficultController);
            IDefenseController defenseController = new DefenseController(difficultController, objectManager);
            IMapController mapController = new MapController(defenseController, difficultController, objectManager);
            IMap map = mapController.GetEmptyMap(300);
            mapController.InitMap(map);

            List<Position> sourcePositions = map.GetFreePositionsForAlien().ToList();
            int index = RandomManager.GetRandomNumber(0, sourcePositions.Count - 1);
            Position sourcePosition = sourcePositions[index];

            List<Position> targetPositions = map.Defenses.ToList().ConvertAll(x => x.Position);
            index = RandomManager.GetRandomNumber(0, targetPositions.Count - 1);
            Position targetPosition = targetPositions[index];
            targetPosition.X--;
            targetPosition.Y--;

            Bitmap image = ExportMapToImage.Instance.ConvertToBitMap(map.GetMap(), map.Size, sourcePosition, targetPosition);
            image.Save(@"C:\temp\mapSourceTarget.bmp");

            IAlienController alienController = new AlienController();
            List<Position> path = alienController.CalculePath(sourcePosition, targetPosition, map.GetMap());

            image = ExportMapToImage.Instance.ConvertToBitMap(map.GetMap(), map.Size, path);
            image.Save(@"C:\temp\mapWithPath.bmp");
        }
    }
}
