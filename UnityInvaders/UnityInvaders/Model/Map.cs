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

        #endregion

        #region Properties

        public IReadOnlyList<IObstacle> Obstacles { get { return obstacles; } }
        public int Width { get; private set; }
        public int Height { get; private set; }

        #endregion

        #region Constructors

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            obstacles = new List<IObstacle>();

            InitMap(width, height);
        }

        #endregion

        #region Methods

        public bool AddObstacle(IObstacle obstacle)
        {
            if (!IsValidPosition(obstacle.Position, obstacle.Width, obstacle.Height))
                return false;
            
            int xEnd = obstacle.Position.X + obstacle.Width;
            int yEnd = obstacle.Position.Y + obstacle.Height;

            for (int x = obstacle.Position.X; x < xEnd; x++)
                for (int y = obstacle.Position.Y; y < yEnd; y++)
                    map[x, y] = 1;

            obstacles.Add(obstacle);

            return true;
        }

        #endregion

        #region Private Methods

        public bool IsValidPosition(Position position, int width, int height)
        {
            if (position.X < 0 || position.X >= Width ||
                position.Y < 0 || position.Y >= Height)
                return false;
            
            int xEnd = position.X + width;
            int yEnd = position.Y + height;

            for (int x = position.X; x < xEnd; x++)
                for (int y = position.Y; y < yEnd; y++)
                    if (map[x, y] != 0 && map[x,y] != 1)
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

        public bool AddDefense(IDefense defense)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
