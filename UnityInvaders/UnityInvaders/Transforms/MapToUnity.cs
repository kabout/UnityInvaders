using System.Collections.Generic;
using UnityEngine;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Transforms
{
    public class MapToUnity
    {
        #region Fields

        public GameObject floor;
        private GameObject obstacleModel;

        #endregion

        #region Constructors

        public MapToUnity(GameObject floor, GameObject obstacleModel)
        {
            this.floor = floor;
            this.obstacleModel = obstacleModel;
        }

        #endregion

        #region Methods

        public UnityMap Convert(IMap map)
        {
            UnityMap unityMap;

            GameObject floorUnity = (GameObject)GameObject.Instantiate(floor, new Vector3(map.Width / 2, 0, map.Height / 2), Quaternion.identity);
            floorUnity.transform.localScale += new Vector3(map.Width, 1, map.Height);

            List<UnityObstacle> obstacles = new List<UnityObstacle>();
            ObstacleToUnity obstacleToUnity = new ObstacleToUnity(obstacleModel);

            foreach (IObstacle obstacle in map.Obstacles)
            {
                UnityObstacle unityObstacle = obstacleToUnity.Convert(obstacle);
                obstacles.Add(unityObstacle);
            }

            unityMap = new UnityMap(obstacles, floorUnity);

            return unityMap;
        }

        #endregion
    }
}
