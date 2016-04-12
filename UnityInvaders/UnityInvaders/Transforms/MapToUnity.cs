using System.Collections.Generic;
using UnityEngine;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Transforms
{
    class MapToUnity
    {
        public GameObject modelTerrain;

        public UnityMap Convert(IMap map)
        {
            UnityMap unityMap;

            Terrain terrain = (Terrain)GameObject.Instantiate(modelTerrain, new Vector3(0, 0, 0), Quaternion.identity);
            terrain.transform.localScale += new Vector3(map.Width, 0, map.Height);

            List<UnityObstacle> obstacles = new List<UnityObstacle>();
            ObstacleToUnity obstacleToUnity = new ObstacleToUnity();

            foreach(IObstacle obstacle in map.Obstacles)
            {
                UnityObstacle unityObstacle = obstacleToUnity.Convert(obstacle);
                obstacles.Add(unityObstacle);
            }

            unityMap = new UnityMap(obstacles, terrain);

            return unityMap;
        }
    }
}
