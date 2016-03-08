using System;
using System.Collections.Generic;

namespace UnityInvaders
{
    public class Map : IMap
    {
        #region Fields

        private int[,] map;

        #endregion

        #region Properties

        public List<IObstacle> Obstacles { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        #endregion

        #region Constructors

        public Map(int width, int height)
        {
            Width = width;
            Height = height;

            InitMap(width, height);
        }

        #endregion

        #region Methods

        public bool AddObstacle(IObstacle obstacle)
        {
            if (!IsValidPosition(obstacle.Position, obstacle.Width, obstacle.Height))
                return false;

            int x = obstacle.Position.x;
            int y = obstacle.Position.y;
            int xEnd = obstacle.Position.x + obstacle.Width;
            int yEnd = obstacle.Position.y + obstacle.Height;

            for (; x < xEnd; x++)
                for (; y < yEnd; y++)
                    map[x, y] = 1;

            return true;
        }

        #endregion

        #region Private Methods

        private bool IsValidPosition(Position position, int width, int height)
        {
            if (position.x < 0 || position.x >= Width ||
                position.y < 0 || position.y >= Height)
                return false;

            int x = position.x;
            int y = position.y;
            int xEnd = position.x + width;
            int yEnd = position.y + height;

            for (; x < xEnd; x++)
                for (; y < yEnd; y++)
                    if (map[x, y] != 0)
                        return false;

            return true;
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
