using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UnityEngine;
using UnityInvaders.Controllers;
using UnityInvaders.Interfaces;

namespace UnityInvadersTests.Controllers
{
    [TestClass]
    public class UTAlienController
    {
        [TestMethod]
        public void Check_Path_Aliens()
        {
            //IDifficultController difficultController = new DifficultController(600);
            //IObjectManager objectManager = new ObjectManager(difficultController, null, null);
            //IStrategyLocationDefenses strategyLocationDefenses = new ManagerDefenses();
            //IMapController mapController = new MapController(strategyLocationDefenses, difficultController, objectManager, null);
            //IMap map = mapController.GetEmptyMap(300, 10);
            //mapController.InitMap(map);

            //Vector3 sourcePosition = new Vector3(0, 0, 0);

            //List<Vector3> targetPositions = map.Defenses.ToList().ConvertAll(x => x.Position);
            //int index = RandomManager.GetRandomNumber(0, targetPositions.Count - 1);
            //Vector3 targetPosition = targetPositions[index];
            //targetPosition.x -= Constants.DEFAULT_DEFENSE_RADIO;
            //targetPosition.y -= Constants.DEFAULT_DEFENSE_RADIO;

            //Bitmap image = ExportMapToImage.Instance.ConvertToBitMap(map.GetMap(), map.Size, sourcePosition, targetPosition);
            //image.Save(@"C:\temp\mapSourceTarget.bmp");

            //IAlienController alienController = new AlienController();
            //List<Vector3> path = alienController.CalculatePath(new Vector3(2,0,3), new Vector3(50,0,50), new List<IObstacle>(), new List<IDefense>(), 100, 10);

            //image = ExportMapToImage.Instance.ConvertToBitMap(map.GetMap(), map.Size, path);
            //image.Save(@"C:\temp\mapWithPath.bmp");
        }

        //[TestMethod]
        //public void Check_Path_Aliens2 ()
        //{
        //    IDifficultController difficultController = new DifficultController(600);
        //    IObjectManager objectManager = new ObjectManager(difficultController);
        //    IDefenseController defenseController = new DefenseController(difficultController, objectManager);
        //    IMapController mapController = new MapController(defenseController, difficultController, objectManager);
        //    IMap map = mapController.GetEmptyMap(300);
        //    mapController.InitMap(map);

        //    List<Position> sourcePositions = map.GetFreePositionsForAlien().ToList();
        //    int index = RandomManager.GetRandomNumber(0, sourcePositions.Count - 1);
        //    Position sourcePosition = sourcePositions[index];

        //    List<Vector3> targetPositions = map.Defenses.ToList().ConvertAll(x => x.Position);
        //    index = RandomManager.GetRandomNumber(0, targetPositions.Count - 1);
        //    Vector3 targetPosition = targetPositions[index];
        //    targetPosition.x--;
        //    targetPosition.y--;

        //    Bitmap image = ExportMapToImage.Instance.ConvertToBitMap(map.GetMap(), map.Size, sourcePosition, targetPosition);
        //    image.Save(@"C:\temp\mapSourceTarget.bmp");

        //    IAlienController alienController = new AlienController();
        //    List<Position> path = alienController.CalculePath(sourcePosition, targetPosition, map.GetMap());

        //    image = ExportMapToImage.Instance.ConvertToBitMap(map.GetMap(), map.Size, path);
        //    image.Save(@"C:\temp\mapWithPath.bmp");
        //}
    }
}
