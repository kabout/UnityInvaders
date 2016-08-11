using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Controllers
{
    public class AlienController : IAlienController
    {
        public List<Position> CalculePath (Position source, Position target, IMap map)
        {
            List<AStar> positionWithValue = new List<AStar>();
            List<AStar> path = new List<AStar>();
            Stack<Position> inversePath = new Stack<Position>();

            int[,] mapMatrix = map.GetMap();

            positionWithValue.Add(new AStar(source, 0, 0, null));

            while (positionWithValue.Count != 0)
            {
                AStar node = positionWithValue.OrderBy(x => x.F).First();

                path.Add(node);
                positionWithValue.Remove(node);

                if(node.Position.Equals(target))
                {
                    AStar actualNode = node;

                    while(actualNode.Parent != null)
                    {
                        inversePath.Push(actualNode.Position);
                        actualNode = actualNode.Parent;
                    }

                    break;
                }

                if (node.Position.Y > 0 && mapMatrix[node.Position.X, node.Position.Y - 1] == 0)
                {
                    Position position = new Position(node.Position.X, node.Position.Y - 1);
                    CheckNewAStarNode(node, position, target, positionWithValue, path);
                }

                if (node.Position.X > 0 && mapMatrix[node.Position.X - 1, node.Position.Y] == 0)
                {
                    Position position = new Position(node.Position.X - 1, node.Position.Y);
                    CheckNewAStarNode(node, position, target, positionWithValue, path);
                }

                if (node.Position.Y < map.Size - 1 && mapMatrix[node.Position.X, node.Position.Y + 1] == 0)
                {
                    Position position = new Position(node.Position.X, node.Position.Y + 1);
                    CheckNewAStarNode(node, position, target, positionWithValue, path);
                }

                if (node.Position.X < map.Size - 1 &&  mapMatrix[node.Position.X + 1, node.Position.Y] == 0)
                {
                    Position position = new Position(node.Position.X + 1, node.Position.Y);
                    CheckNewAStarNode(node, position, target, positionWithValue, path);
                }
            }

            return inversePath.ToList();
        }

        private void CheckNewAStarNode (AStar node, Position newPosition,Position target, List<AStar> positionWithValue, List<AStar> path)
        {
            if (path.Exists(x => x.Equals(newPosition)))
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

        private int manhattanHeuristic (Position newNode, Position end)
        {
            return (Math.Abs(newNode.X - end.X) + Math.Abs(newNode.Y - end.Y));
        }

        private class AStar
        {
            #region Properties

            public AStar Parent { get; set; }

            public Position Position { get; set; }

            public int F { get { return G + H; } }
            public int G { get; set; }

            public int H { get; set; }

            #endregion

            #region Constructors

            public AStar (Position position, int g, int h, AStar parent)
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
                    return this.Position.X == objAsPart.Position.X && this.Position.Y == objAsPart.Position.Y;
            }
            public override int GetHashCode ()
            {
                return Convert.ToInt32(string.Format("{0}{1}", this.Position.X, this.Position.Y));
            }

            #endregion
        }
    }
}
