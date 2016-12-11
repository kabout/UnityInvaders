using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StrategyLocationDefenses
{
    public class ManagerDefenses : IStrategyLocationDefenses
    {
        #region Fields

        private List<KeyValuePair<Vector3,int>> weightedPositions = new List<KeyValuePair<Vector3, int>>();
        private List<Vector2> obstaclePositions = new List<Vector2>();
        private int mapSize, cellSize, defenseRadius;
        private int[,] map;
        private IList<IObstacle> obstacles;

        #endregion

        #region IStrategyLocationDefenses

        public void InitStrategy(IList<IObstacle> obstacles, int defenseRadius, int mapSize, int cellSize)
        {
            this.obstacles = obstacles;
            this.mapSize = mapSize;
            this.cellSize = cellSize;
            this.defenseRadius = defenseRadius;

            InitMap();
        }

        public Vector3 GetPositionForDefense(IList<IDefense> defenses)
        {
            bool placed = false;

            while (!placed && weightedPositions.Count > 0)
            {
                Vector3 position = weightedPositions.FirstOrDefault().Key * cellSize;

                if (IsValidPosition(position, defenses))
                {
                    weightedPositions.RemoveAt(0);
                    return position;
                }

                weightedPositions.RemoveAt(0);
            }

            return Vector3.zero;
        }

        #endregion

        #region Methods

        private void InitMap()
        {
            int numCells = mapSize / cellSize;
            map = new int[numCells, numCells];

            for (int i = 0; i < numCells; i++)
                for (int j = 0; j < numCells; j++)
                    map[i, j] = 0;

            foreach (IObstacle obstacle in obstacles)
            {
                int xInMap = Mathf.RoundToInt(obstacle.Position.x / cellSize);
                int zInMap = Mathf.RoundToInt(obstacle.Position.z / cellSize);

                int obstacleRadius = Mathf.RoundToInt(obstacle.Radius / cellSize);

                for (int i = Mathf.Min(0, xInMap - obstacleRadius); i < Mathf.Max(numCells, xInMap + obstacleRadius); i++)
                    for (int j = Mathf.Min(0, zInMap - obstacleRadius); j < Mathf.Max(numCells, zInMap + obstacleRadius); j++)
                        map[i, j] = 1;

                obstaclePositions.Add(new Vector2(xInMap, zInMap));
            }

            int defenseRadiusInMap = defenseRadius / cellSize;

            for (int x = 0; x < numCells; x++)
            {
                if (x <= defenseRadiusInMap || x >= (numCells - defenseRadiusInMap))
                    continue;

                for (int z = 0; z < numCells; z++)
                {
                    if (z <= defenseRadiusInMap || z >= (numCells - defenseRadiusInMap))
                        continue;

                    List<int> distances = new List<int>();

                    int factorPos = Mathf.Abs((numCells / 2) - (numCells - x)) +
                            Mathf.Abs((numCells / 2) - (numCells - z));

                    foreach (Vector2 position in obstaclePositions)
                        distances.Add(distanceDistanceToObstacle(x, z, position.x, position.y) + factorPos);

                    weightedPositions.Add(new KeyValuePair<Vector3, int>(new Vector3(x, 0, z), distances.Min()));
                }
            }

            weightedPositions = weightedPositions.OrderBy(x => x.Value).ToList();
        }

        private int distanceDistanceToObstacle(int x1, int y1, float x2, float y2)
        {
            return Mathf.RoundToInt(Mathf.Abs(x1 - x2) + Mathf.Abs(y1 - y2));
        }

        private bool IsValidPosition(Vector3 position, IList<IDefense> defenses)
        {
            if (position.x - defenseRadius <= 0 || position.x + defenseRadius >= mapSize ||
                position.z - defenseRadius <= 0 || position.z + defenseRadius >= mapSize)
                return false;

            foreach(IObstacle obstacle in obstacles)
            {
                float distance = Distance(obstacle.Position, position);

                if (distance < (obstacle.Radius + defenseRadius))
                    return false;
            }

            foreach (IDefense defense in defenses)
            {
                float distance = Distance(defense.Position, position);

                if(distance < (defense.Radius + defenseRadius))
                    return false;
            }

            // Comprobar que se puede llegar a algún borde desde la posición
            

            return true;
        }

        private float Distance(Vector3 position1, Vector3 position2)
        {
            return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(position2.x - position1.x), 2) + Mathf.Pow(Mathf.Abs(position2.z - position1.z), 2));
        }

        #endregion
    }
}
