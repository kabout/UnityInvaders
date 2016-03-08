using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityInvaders
{
    public class Map : IMap
    {
        #region Properties

        List<IObstacle> Obstacles { get; private set; }
        public int[,] Map { get; private set; }
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

        private void InitMap(int width, int height)
        {
            Map = new int[width, height];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    Map[i, j] = 0;
        }        

        #endregion
    }
}
