using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StrategyAlienAttack
{
    public class StrategyAlienAttack : IStrategyAlienAttack
    {
        private float posY = 3;

        public List<Vector3> CalculatePath(Vector3 source, Vector3 target, IList<IObstacle> obstacles, IList<IDefense> defenses, int sizeMap, int cellSize)
        {
            List<AStar> positionWithValue = new List<AStar>();
            List<AStar> path = new List<AStar>();
            Stack<Vector3> inversePath = new Stack<Vector3>();

            int[,] mapMatrix = InitMap(obstacles, defenses, sizeMap, cellSize);

            int sizeInMatrix = sizeMap / cellSize;
            Vector3 targetMap = new Vector3(Mathf.RoundToInt(target.x / cellSize), posY,
                Mathf.RoundToInt(target.z / cellSize));
            Vector3 sourceMap = new Vector3(Mathf.RoundToInt(source.x / cellSize), posY,
                Mathf.RoundToInt(source.z / cellSize));

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

                int x = (int)node.Position.x;
                int z = (int)node.Position.z;

                if (z > 0)
                {
                    Vector3 position = new Vector3(x, posY, z - 1);

                    if (position.Equals(targetMap) || mapMatrix[x, z - 1] == 0)
                        CheckNewAStarNode(node, position, targetMap, positionWithValue, path);
                }

                if (x > 0)
                {
                    Vector3 position = new Vector3(x - 1, posY, z);

                    if (position.Equals(targetMap) || mapMatrix[x - 1, z] == 0)
                        CheckNewAStarNode(node, position, targetMap, positionWithValue, path);
                }

                if (z < sizeInMatrix - 1)
                {
                    Vector3 position = new Vector3(x, posY, z + 1);

                    if (position.Equals(targetMap) || mapMatrix[x, z + 1] == 0)
                        CheckNewAStarNode(node, position, targetMap, positionWithValue, path);
                }

                if (x < sizeInMatrix - 1)
                {
                    Vector3 position = new Vector3(x + 1, posY, z);

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
                {
                    map[i, j] = 0;
                }

            foreach(IObstacle obstacle in obstacles)
            {
                int xInMap = Mathf.RoundToInt(obstacle.Position.x / cellSize);
                int zInMap = Mathf.RoundToInt(obstacle.Position.z / cellSize);

                int obstacleRadius = Mathf.RoundToInt(obstacle.Radius / cellSize);

                for(int i = Mathf.Max(0, xInMap - obstacleRadius); i < Mathf.Min(sizeMap, xInMap + obstacleRadius); i++)
                    for (int j = Mathf.Max(0, zInMap - obstacleRadius); j < Mathf.Min(sizeMap, zInMap + obstacleRadius); j++)
                        map[i, j] = 1;
            }

            foreach (IDefense defense in defenses)
            {
                int xInMap = Mathf.RoundToInt(defense.Position.x / cellSize);
                int zInMap = Mathf.RoundToInt(defense.Position.z / cellSize);

                int defenseRadius = Mathf.RoundToInt(defense.Radius / cellSize);

                for (int i = Mathf.Max(0, xInMap - defenseRadius); i < Mathf.Min(sizeMap, xInMap + defenseRadius); i++)
                    for (int j = Mathf.Max(0, zInMap - defenseRadius); j < Mathf.Min(sizeMap, zInMap + defenseRadius); j++)
                        map[i, j] = 1;
            }

            return map;
        }

        private Vector3 CellCenterToPosition(int i, int j, float cellWidth, float cellHeight)
        {
            return new Vector3((j * cellWidth) + cellWidth * 0.5f, (i * cellHeight) + cellHeight * 0.5f, 0);
        }

        private void CheckNewAStarNode (AStar node, Vector3 newPosition, Vector3 target, List<AStar> positionWithValue, List<AStar> path)
        {
            if (path.Exists(x => x.Position.Equals(newPosition)))
                return;
            
            AStar aStar = new AStar(newPosition, node.G + 10, manhattanHeuristic(newPosition, target) * 10, node);

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

        private int manhattanHeuristic (Vector3 newNode, Vector3 end)
        {
            return (Math.Abs(Mathf.RoundToInt(newNode.x - end.x)) + Math.Abs(Mathf.RoundToInt(newNode.z - end.z)));
        }

        private class AStar
        {
            #region Properties

            public AStar Parent { get; set; }

            public Vector3 Position { get; set; }

            public int F { get { return G + H; } }
            public int G { get; set; }

            public int H { get; set; }

            #endregion

            #region Constructors

            public AStar (Vector3 position, int g, int h, AStar parent)
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
                    return this.Position.x == objAsPart.Position.x && this.Position.z == objAsPart.Position.z;
            }
            public override int GetHashCode ()
            {
                return Convert.ToInt32(string.Format("{0}{1}", this.Position.x, this.Position.z));
            }

            #endregion
        }
    }
}
