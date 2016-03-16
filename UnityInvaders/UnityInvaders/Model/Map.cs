using System;
using System.Collections.Generic;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class Map : IMap
    {
        #region Fields

        private int[,] map;
        private List<IObstacle> obstacles;
        private List<IDefense> defenses;

        #endregion

        #region Properties

        public IReadOnlyList<IObstacle> Obstacles { get { return obstacles; } }
        public IReadOnlyList<IDefense> Defenses { get { return defenses; } }
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

            InitMap(width, height);
        }

        #endregion

        #region Methods

        public bool AddObstacle(IObstacle obstacle)
        {
            if (!IsValidPosition(obstacle))
                return false;
            
            int xEnd = obstacle.Position.X + obstacle.Width;
            int yEnd = obstacle.Position.Y + obstacle.Height;

            for (int x = obstacle.Position.X; x < xEnd; x++)
                for (int y = obstacle.Position.Y; y < yEnd; y++)
                    map[x, y] = 1;

            obstacles.Add(obstacle);

            return true;
        }

        public bool AddDefense(IDefense defense)
        {
            throw new NotImplementedException();
        }

        public bool IsValidPosition(IDefense defense)
        {
            if (!IsValidLimits(defense.Position.X, defense.Position.Y))
                return false;

            int xEnd = defense.Position.X + defense.Width;
            int yEnd = defense.Position.Y + defense.Height;

            for (int x = defense.Position.X; x < xEnd; x++)
                for (int y = defense.Position.Y; y < yEnd; y++)
                    if (map[x, y] != 0)
                        return false;

            return true;
        }

        public bool IsValidPosition(IObstacle obstacle)
        {
            if (!IsValidLimits(obstacle.Position.X, obstacle.Position.Y))
                return false;

            int xEnd = obstacle.Position.X + obstacle.Width;
            int yEnd = obstacle.Position.Y + obstacle.Height;

            for (int x = obstacle.Position.X; x < xEnd; x++)
                for (int y = obstacle.Position.Y; y < yEnd; y++)
                    if (map[x, y] != 0 && map[x, y] != 1)
                        return false;

            return true;
        }

        #endregion

        #region Private Methods

        private bool IsValidLimits(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        private void InitMap(int width, int height)
        {
            map = new int[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    map[x, y] = 0;
        }

        #endregion
    }
}
