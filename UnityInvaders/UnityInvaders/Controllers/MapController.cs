using System;
using System.Drawing;
using UnityEngine;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;
using UnityInvaders.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace UnityInvaders.Controllers
{
    public class MapController : IMapController
    {
        #region Fields

        GameObject mapPrefab;
        IDifficultController difficultController;
        IObjectManager objectManager;
        IDefenseController defenseController;

        #endregion

        #region Constructors

        public MapController(IDefenseController defenseController, IDifficultController difficultController, IObjectManager objectManager, GameObject mapPrefab)
        {
            this.difficultController = difficultController;
            this.objectManager = objectManager;
            this.defenseController = defenseController;
            this.mapPrefab = mapPrefab;
        }

        #endregion

        #region Methods

        public IMap GetEmptyMap(int size)
        {
            GameObject map = GameObject.Instantiate(mapPrefab, new Vector3(size / 2, 0, size / 2), Quaternion.identity) as GameObject;

            if (map == null)
                return null;

            map.transform.localScale = new Vector3(size, 0, size);

            // Inicializar los valores de la defensa
            UnityMap unityMap = map.GetComponent(typeof(UnityMap)) as UnityMap;
            unityMap.margin = 15; // Mathf.RoundToInt(size * 0.1f);
            unityMap.InitMap();

            return unityMap;
        }

        public void InitMap(IMap map)
        {
            PlaceObstacles(map);
            map.CorrectCellUnReachables();
            defenseController.PlaceDefenses(map);
            Bitmap image = ExportMapToImage.Instance.ConvertToBitMap(map.GetMap(), map.Size);
            image.Save(@"C:\temp\map.bmp");
        }       
      

        private void PlaceObstacles(IMap map)
        {
            int numOfObstacles = difficultController.GetNumberOfObstacles(map);

            while (numOfObstacles > 0)
            {
                IObstacle obstacle = objectManager.GenerateObstacle(map, Constants.MIN_OBSTACLE_RADIUS, Constants.MAX_OBSTACLE_RADIUS);

                if (obstacle == null)
                    return;

                map.AddObstacle(obstacle);

                numOfObstacles--;
            }
        }

        private void PlaceDefenses(IMap map)
        {
            int numDefenses = difficultController.GetNumberOfDefenses(map);

            while (numDefenses > 0)
            {
                IDefense defense = objectManager.GenerateDefense(map, Constants.DEFAULT_DEFENSE_RADIO);

                if (defense == null)
                    return;

                map.AddDefense(defense);

                numDefenses--;
            }
        }

        #endregion
    }
}
