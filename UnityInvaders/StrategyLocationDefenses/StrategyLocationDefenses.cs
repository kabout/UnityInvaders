using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyLocationDefenses
{
    //Random
    public class StrategyLocationDefenses1 : IStrategyLocationDefenses
    {
        #region Fields

        private List<IPosition> positions = new List<IPosition>();
        private List<IPosition> obstaclePositions = new List<IPosition>();
        private int mapSize, cellSize, defenseRadius;
        private int[,] map;
        private IList<IObstacle> obstacles;
        private string StrategyName = "Random";

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

        public IPosition GetPositionForDefense(IList<IDefense> defenses)
        {
            Random random = new Random((int)DateTime.Now.Ticks);

            do
            {
                int index = random.Next(0, positions.Count);

                IPosition position = positions.ElementAt(index);
                positions.RemoveAt(index);

                if (IsValidPosition(position, defenses))
                    return position;

            } while (positions.Count > 0);

            return null;
        }

        #endregion

        #region Methods

        private bool IsValidPosition(IPosition position, IList<IDefense> defenses)
        {
            if (position.X - defenseRadius <= 0 || position.X + defenseRadius >= mapSize ||
                Math.Abs(position.Z) - defenseRadius <= 0 || Math.Abs(position.Z) + defenseRadius >= mapSize)
                return false;

            foreach (IObstacle obstacle in obstacles)
            {
                float distance = Distance(obstacle.Position, position);

                if (distance < (obstacle.Radius + defenseRadius))
                    return false;
            }

            foreach (IDefense defense in defenses)
            {
                float distance = Distance(defense.Position, position);

                if (distance < (defense.Radius + defenseRadius))
                    return false;
            }

            // Comprobar que se puede llegar a algún borde desde la posición


            return true;
        }

        private void InitMap()
        {
            int numCells = mapSize / cellSize;
            map = new int[numCells, numCells];

            for (int i = 0; i < numCells; i++)
                for (int j = 0; j < numCells; j++)
                    map[i, j] = 0;

            foreach (IObstacle obstacle in obstacles)
            {
                float xInMap = obstacle.Position.X / cellSize;
                float zInMap = Math.Abs(obstacle.Position.Z) / cellSize;

                float obstacleRadius = obstacle.Radius / cellSize;

                int xInit = Convert.ToInt32(Math.Floor(xInMap - obstacleRadius));
                int zInit = Convert.ToInt32(Math.Floor(zInMap - obstacleRadius));
                int xEnd = Convert.ToInt32(Math.Ceiling(xInMap + obstacleRadius));
                int zEnd = Convert.ToInt32(Math.Ceiling(zInMap + obstacleRadius));

                for (int x = xInit; x < xEnd; x++)
                    for (int z = zInit; z < zEnd; z++)
                        map[x, z] = 1;

                obstaclePositions.Add(new Position(xInMap, 0, zInMap));

#if DEBUG
                string mapString = string.Empty;

                for (int i = 0; i < numCells; i++)
                {
                    for (int j = 0; j < numCells; j++)
                        mapString += map[j, i].ToString() + " ";
                    mapString += "\n";
                }
#endif
            }

            float defenseRadiusInMap = (float)defenseRadius / cellSize;

            int intDefenseRadiusInMap = Convert.ToInt32(Math.Ceiling(defenseRadiusInMap));
            int maxPosition = numCells - intDefenseRadiusInMap;

            for (int x = 0; x < numCells; x++)
            {
                if (x < intDefenseRadiusInMap || x >= maxPosition)
                    continue;

                for (int z = 0; z < numCells; z++)
                {
                    if (z < intDefenseRadiusInMap || z >= maxPosition)
                        continue;

                    if (map[x, z] == 1)
                        continue;

                    positions.Add(new Position(x, 0, -z));
                }
            }
        }

        private float Distance(IPosition position1, IPosition position2)
        {
            return (float)Math.Sqrt(Math.Pow(Math.Abs(position2.X - position1.X), 2) + Math.Pow(Math.Abs(Math.Abs(position2.Z) - Math.Abs(position1.Z)), 2));
        }

        #endregion
    }
}
