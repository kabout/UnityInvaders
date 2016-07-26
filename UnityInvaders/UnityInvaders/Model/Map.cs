using System;
using System.Collections.Generic;
using System.Linq;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class Map : IMap
    {
        #region Enums

        private enum ObjectType
        {
            Obstacle = 0,
            Defense = 1
        }

        #endregion

        #region Fields

        private int[,] map;
        private List<IObstacle> obstacles;
        private List<IDefense> defenses;
        private Dictionary<ObjectType, List<Position>> freePositions = new Dictionary<ObjectType, List<Position>>();

        #endregion

        #region Properties

        public IList<IObstacle> Obstacles { get { return obstacles; } }
        public IList<IDefense> Defenses { get { return defenses; } }
        public int Width { get; private set; }
        public int Height { get; private set; }

        #endregion

        #region Constructors

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            obstacles = new List<IObstacle>();
            defenses = new List<IDefense>();
            freePositions.Add(ObjectType.Obstacle, new List<Position>());
            freePositions.Add(ObjectType.Defense, new List<Position>());

            InitMap(width, height);
        }

        #endregion

        #region Methods

        public bool AddObstacle(IObstacle obstacle)
        {
            if (!IsValidPosition(obstacle))
                return false;

            obstacles.Add(obstacle);

            #region Update defense free positions

            int xEnd = obstacle.Position.X + obstacle.Width;
            int yEnd = obstacle.Position.Y + obstacle.Height;

            for (int x = obstacle.Position.X; x < xEnd; x++)
                for (int y = obstacle.Position.Y; y < yEnd; y++)
                {
                    map[x, y] = 1;
                    freePositions[ObjectType.Defense].RemoveAll(p => p.X == x && p.Y == y);
                }

            for (int x = Math.Max(0, obstacle.Position.X - (Constants.DEFENSE_SIZE - 1)); x < obstacle.Position.X; x++)
                for (int y = obstacle.Position.Y; y < yEnd; y++)
                    freePositions[ObjectType.Defense].RemoveAll(p => p.X == x && p.Y == y);

            for (int x = obstacle.Position.X; x < xEnd; x++)
                for (int y = Math.Max(0, obstacle.Position.Y - (Constants.DEFENSE_SIZE - 1)); y < yEnd; y++)
                    freePositions[ObjectType.Defense].RemoveAll(p => p.X == x && p.Y == y);

            for (int x = Math.Max(0, obstacle.Position.X - (Constants.DEFENSE_SIZE - 1)); x < obstacle.Position.X; x++)
                for (int y = Math.Max(0, obstacle.Position.Y - (Constants.DEFENSE_SIZE - 1)); y < yEnd; y++)
                    freePositions[ObjectType.Defense].RemoveAll(p => p.X == x && p.Y == y);

            #endregion

            #region Update obstacle free positions

            freePositions[ObjectType.Obstacle].RemoveAll(p => p.X == obstacle.Position.X && p.Y == obstacle.Position.Y);

            #endregion

            return true;
        }

        public bool AddDefense(IDefense defense)
        {
            if (!IsValidPosition(defense))
                return false;

            int xEnd = defense.Position.X + defense.Width;
            int yEnd = defense.Position.Y + defense.Height;

            for (int x = defense.Position.X; x < xEnd; x++)
                for (int y = defense.Position.Y; y < yEnd; y++)
                    map[x, y] = 2;

            defenses.Add(defense);

            #region Update obstacle free positions

            freePositions[ObjectType.Obstacle].RemoveAll(p => p.X >= defense.Position.X && p.X < xEnd && p.Y >= defense.Position.Y && p.Y < yEnd);

            #endregion

            return true;
        }

        public bool IsValidPosition(IDefense defense)
        {
            return freePositions[ObjectType.Defense].Exists(p => p.X == defense.Position.X && p.Y == defense.Position.Y);
        }

        public bool IsValidPosition(IObstacle obstacle)
        {
            return freePositions[ObjectType.Obstacle].Exists(p => p.X == obstacle.Position.X && p.Y == obstacle.Position.Y);
        }

        #endregion

        #region Private Methods

        private void InitMap(int width, int height)
        {
            map = new int[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = 0;
                    freePositions[ObjectType.Obstacle].Add(new Position(x, y));

                    if ((x + Constants.DEFENSE_SIZE) <= width && (y + Constants.DEFENSE_SIZE) <= height)
                        freePositions[ObjectType.Defense].Add(new Position(x, y));
                }           
        }

        public IList<Position> GetFreePositionsForObstacle(int width, int height)
        {
            int maxWidth = Width - width;
            int maxHeight = Height - height;

            List<Position> positions = new List<Position>(freePositions[ObjectType.Obstacle].Where(x => x.X < maxWidth && x.Y < maxHeight));

            // Mejoras
            //foreach(IDefense defense in defenses)
            //{

            //}

            return positions;
        }

        public IList<Position> GetFreePositionsForDefense()
        {
            return freePositions[ObjectType.Defense];
        }

        #endregion
    }
}
