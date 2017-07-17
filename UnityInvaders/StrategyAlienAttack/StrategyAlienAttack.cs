using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyAlienAttack
{
    public class StrategyAlienAttack : IStrategyAlienAttack
    {
        private float posY = 3;

        public List<IPosition> CalculatePath(IPosition source, IPosition target, IList<IObstacle> obstacles, IList<IDefense> defenses, int sizeMap, int cellSize)
        {
            List<AStar> positionWithValue = new List<AStar>();
            List<AStar> path = new List<AStar>();
            Stack<IPosition> inversePath = new Stack<IPosition>();

            int[,] mapMatrix = InitMap(obstacles, defenses, sizeMap, cellSize);

            int sizeInMatrix = sizeMap / cellSize;
            IPosition targetMap = new Position((int)Math.Round(target.X / cellSize), posY, (int)Math.Round(target.Z / cellSize));
            IPosition sourceMap = new Position((int)Math.Round(source.X / cellSize), posY, (int)Math.Round(source.Z / cellSize));

            positionWithValue.Add(new AStar(sourceMap, 0, 0, null));

            while (positionWithValue.Count != 0)
            {
                AStar node = positionWithValue.OrderBy(p => p.F * 10000000 + p.H).First();

                path.Add(node);
                positionWithValue.Remove(node);

                if(node.Position.Equals(targetMap))
                {
                    AStar actualNode = node;

                    while(actualNode.Parent != null)
                    {
                        inversePath.Push(actualNode.Position);
                        actualNode = actualNode.Parent;
                    }

                    break;
                }

                int x = (int)node.Position.X;
                int z = (int)node.Position.Z;

                if (z > 0)
                {
                    IPosition position = new Position(x, posY, z - 1);

                    if (position.Equals(targetMap) || mapMatrix[x, z - 1] == 0)
                        CheckNewAStarNode(node, position, targetMap, positionWithValue, path);
                }

                if (x > 0)
                {
                    IPosition position = new Position(x - 1, posY, z);

                    if (position.Equals(targetMap) || mapMatrix[x - 1, z] == 0)
                        CheckNewAStarNode(node, position, targetMap, positionWithValue, path);
                }

                if (z < sizeInMatrix - 1)
                {
                    IPosition position = new Position(x, posY, z + 1);

                    if (position.Equals(targetMap) || mapMatrix[x, z + 1] == 0)
                        CheckNewAStarNode(node, position, targetMap, positionWithValue, path);
                }

                if (x < sizeInMatrix - 1)
                {
                    IPosition position = new Position(x + 1, posY, z);

                    if (position.Equals(targetMap) || mapMatrix[x + 1, z] == 0)
                        CheckNewAStarNode(node, position, targetMap, positionWithValue, path);
                }
            }

            return inversePath.ToList();
        }

        private int[,] InitMap(IList<IObstacle> obstacles, IList<IDefense> defenses, int sizeMap, int cellSize)
        {
            int matrixSize = sizeMap / cellSize;

            int[,] map = new int[matrixSize, matrixSize];

            for(int i = 0; i < matrixSize; i++)
                for(int j = 0; j < matrixSize; j++)
                    map[i, j] = 0;

            foreach (IObstacle obstacle in obstacles)
                UpdateMap(obstacle, map, sizeMap, cellSize);

            foreach (IDefense defense in defenses)
                UpdateMap(defense, map, sizeMap, cellSize);

            return map;
        }

        private void UpdateMap(IEntity entity, int[,] map, int sizeMap, int cellSize)
        {
            int xInMap = (int)Math.Round(entity.Position.X / cellSize);
            int zInMap = (int)Math.Round(entity.Position.Z / cellSize);

            int entityRadius = (int)Math.Round(entity.Radius / cellSize);

            for (int i = Math.Max(0, xInMap - entityRadius); i < Math.Min(sizeMap, xInMap + entityRadius); i++)
                for (int j = Math.Max(0, zInMap - entityRadius); j < Math.Min(sizeMap, zInMap + entityRadius); j++)
                    map[i, j] = 1;
        }

        private IPosition CellCenterToPosition(int i, int j, float cellWidth, float cellHeight)
        {
            return new Position((j * cellWidth) + cellWidth * 0.5f, (i * cellHeight) + cellHeight * 0.5f, 0);
        }

        private void CheckNewAStarNode (AStar node, IPosition newPosition, IPosition target, List<AStar> positionWithValue, List<AStar> path)
        {
            if (path.Exists(x => x.Position.Equals(newPosition)))
                return;
            
            AStar aStar = new AStar(newPosition, node.G + 10, ManhattanHeuristic(newPosition, target) * 10, node);

            if (!positionWithValue.Contains(aStar))
                positionWithValue.Add(aStar);
            else
            {
                AStar existentAStar = positionWithValue.Find(x => x.Equals(aStar));

                if (existentAStar.G > aStar.G)
                {
                    existentAStar.G = aStar.G;
                    existentAStar.Parent = aStar.Parent;
                }
            }            
        }

        private int ManhattanHeuristic (IPosition newNode, IPosition end)
        {
            return (Math.Abs((int)Math.Round(newNode.X - end.X)) + Math.Abs((int)Math.Round(newNode.Z - end.Z)));
        }

        public IDefense GetNextDefenseToAttack(IPosition alienPosition, IList<IObstacle> obstacles, IList<IDefense> defenses, int sizeMap, int cellSize)
        {
            IDefense nextDefense = null;
            int minDistance = int.MaxValue;

            foreach (var defense in defenses)
            {
                int distance = ManhattanHeuristic(alienPosition, defense.Position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nextDefense = defense;
                }
            }

            return nextDefense;
        }

        private class AStar
        {
            #region Properties

            public AStar Parent { get; set; }

            public IPosition Position { get; set; }

            public int F { get { return G + H; } }
            public int G { get; set; }

            public int H { get; set; }

            #endregion

            #region Constructors

            public AStar (IPosition position, int g, int h, AStar parent)
            {
                Position = position;
                G = g;
                H = h;
                Parent = parent;
            }

            #endregion

            #region Overrides

            public override bool Equals (object obj)
            {
                if (obj == null)
                    return false;

                AStar objAsPart = obj as AStar;

                if (objAsPart == null)
                    return false;

                else
                    return this.Position.X == objAsPart.Position.X && this.Position.Z == objAsPart.Position.Z;
            }
            public override int GetHashCode ()
            {
                return Convert.ToInt32(string.Format("{0}{1}", this.Position.X, this.Position.Z));
            }

            #endregion
        }
    }
}
