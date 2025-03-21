﻿using UnityEngine;
using System.Drawing;
using System.Collections.Generic;

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

    public static ExportMapToImage GetInstance()
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
                    image.SetPixel(x, y, System.Drawing.Color.Gray);
                    break;
                case 1:
                    image.SetPixel(x, y, System.Drawing.Color.Black);
                    break;
                case 2:
                    image.SetPixel(x, y, System.Drawing.Color.Red);
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

    public Bitmap ConvertToBitMap(int[,] map, int size, List<Vector3> path)
    {
        Bitmap image = new Bitmap(size, size);

        int x = 0;
        int y = 0;

        foreach (int point in map)
        {
            switch (point)
            {
                case 0:
                    if (path.Contains(new Vector3(y, 0, x)))
                        image.SetPixel(x, y, System.Drawing.Color.Blue);
                    else
                        image.SetPixel(x, y, System.Drawing.Color.Gray);
                    break;
                case 1:
                    image.SetPixel(x, y, System.Drawing.Color.Black);
                    break;
                case 2:
                    image.SetPixel(x, y, System.Drawing.Color.Red);
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

    public Bitmap ConvertToBitMap(int[,] map, int size, Vector3 source, Vector3 target)
    {
        Bitmap image = new Bitmap(size, size);

        int x = 0;
        int y = 0;

        foreach (int point in map)
        {
            switch (point)
            {
                case 0:
                    if (source.Equals(new Vector3(y, x)))
                        image.SetPixel(x, y, System.Drawing.Color.White);
                    else if (target.Equals(new Vector3(y, x)))
                        image.SetPixel(x, y, System.Drawing.Color.Yellow);
                    else
                        image.SetPixel(x, y, System.Drawing.Color.Gray);
                    break;
                case 1:
                    image.SetPixel(x, y, System.Drawing.Color.Black);
                    break;
                case 2:
                    image.SetPixel(x, y, System.Drawing.Color.Red);
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

    public Bitmap ConvertToBitMap(char[,] map, int size)
    {
        Bitmap image = new Bitmap(size, size);

        int x = 0;
        int y = 0;

        foreach (char point in map)
        {
            switch (point)
            {
                case ' ':
                    image.SetPixel(x, y, System.Drawing.Color.Gray);
                    break;
                case 'x':
                    image.SetPixel(x, y, System.Drawing.Color.Black);
                    break;
                default:
                    image.SetPixel(x, y, System.Drawing.Color.Red);
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
