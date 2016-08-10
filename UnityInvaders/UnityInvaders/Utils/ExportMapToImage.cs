using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Utils
{
    public class ExportMapToImage
    {
        #region Fields

        private static ExportMapToImage instance;

        #endregion

        #region Properties

        public static ExportMapToImage Instance
        {
            get { return GetInstance(); }
        }

        #endregion

        #region Methods

        public static ExportMapToImage GetInstance ()
        {
            if (instance == null)
                instance = new ExportMapToImage();

            return instance;
        }

        public Bitmap ConvertToBitMap(int[,] map, int size)
        {
            Bitmap image = new Bitmap(size, size);
            
            int x = 0;
            int y = 0;

            foreach (int point in map)
            {
                switch (point)
                {
                    case 0:
                    image.SetPixel(x, y, Color.Gray);
                    break;
                    case 1:
                    image.SetPixel(x, y, Color.Black);
                    break;
                    case 2:
                    image.SetPixel(x, y, Color.Red);
                    break;
                }

                if (x == size - 1)
                {
                    x = 0;
                    y++;
                }
                else
                    x++;
            }

            return image;
        }

        public Bitmap ConvertToBitMap (char[,] map, int size)
        {
            Bitmap image = new Bitmap(size, size);

            int x = 0;
            int y = 0;

            foreach (char point in map)
            {
                switch (point)
                {
                    case ' ':
                    image.SetPixel(x, y, Color.Gray);
                    break;
                    case 'x':
                    image.SetPixel(x, y, Color.Black);
                    break;
                    default:
                    image.SetPixel(x, y, Color.Red);
                    break;
                }

                if (x == size - 1)
                {
                    x = 0;
                    y++;
                }
                else
                    x++;
            }

            return image;
        }

        


    #endregion
}
}
