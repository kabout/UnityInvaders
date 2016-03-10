using System;
using System.Collections.Generic;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class Map : IMap
    {
        #region Fields

        private int[,] map;

        #endregion

        #region Properties

        public List<IObstacle> Obstacles { get; private set; }
        public uint Width { get; private set; }
        public uint Height { get; private set; }

        #endregion

        #region Constructors

        public Map(uint width, uint height)
        {
            Width = width;
            Height = height;
            Obstacles = new List<IObstacle>();

            InitMap(width, height);
        }

        #endregion

        #region Methods

        public bool AddObstacle(IObstacle obstacle)
        {
            if (!IsValidPosition(obstacle.Position, obstacle.Width, obstacle.Height))
                return false;
            
            uint xEnd = obstacle.Position.X + obstacle.Width;
            uint yEnd = obstacle.Position.Y + obstacle.Height;

            for (uint x = obstacle.Position.X; x < xEnd; x++)
                for (uint y = obstacle.Position.Y; y < yEnd; y++)
                    map[x, y] = 1;

            Obstacles.Add(obstacle);

            return true;
        }

        #endregion

        #region Private Methods

        private bool IsValidPosition(Position position, uint width, uint height)
        {
            if (position.X < 0 || position.X >= Width ||
                position.Y < 0 || position.Y >= Height)
                return false;
            
            uint xEnd = position.X + width;
            uint yEnd = position.Y + height;

            for (uint x = position.X; x < xEnd; x++)
                for (uint y = position.Y; y < yEnd; y++)
                    if (map[x, y] != 0)
                        return false;

            return true;
        }

        private void InitMap(uint width, uint height)
        {
            map = new int[width, height];

            for (uint x = 0; x < width; x++)
                for (uint y = 0; y < height; y++)
                    map[x, y] = 0;
        }

        #endregion
    }
}
