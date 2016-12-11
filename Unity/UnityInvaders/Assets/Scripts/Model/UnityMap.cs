using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System;
using Assets.Scripts.Utils;

public class UnityMap : MonoBehaviour, IMap
{
    #region Fields

    public int margin;
    public int cellSize;

    private IList<IObstacle> obstacles;
    private IList<IDefense> defenses;
    private IList<IAlien> aliens;
    private List<Vector3> freePositions;
    private int[,] map;
    private char[,] correctedMap;
    private List<Vector3> positionsUnReachables;
    private int upperBorder;

    #endregion

    #region Properties

    /// <summary>
    /// Anchura del mapa
    /// </summary>
    public int Width { get { return (int)transform.localScale.x; } }
    /// <summary>
    /// Altura del mapa
    /// </summary>
    public int Height { get { return (int)transform.localScale.z; } }

    public int Size { get { return (int)transform.localScale.x; } }

    public IList<IObstacle> Obstacles { get { return obstacles; } }

    public IList<IDefense> Defenses { get { return defenses; } }

    public IList<IAlien> Aliens { get { return aliens; } }

    public int Margin { get { return margin; } }

    public int CellSize
    {
        get
        {
            return cellSize;
        }
    }

    #endregion

    #region Methods

    public void InitMap()
    {
        obstacles = new List<IObstacle>();
        defenses = new List<IDefense>();
        aliens = new List<IAlien>();
        freePositions = new List<Vector3>();
        map = new int[Size, Size];
        correctedMap = new char[Size, Size];
        positionsUnReachables = new List<Vector3>();

        for (int x = 0; x < Size; x++)
            for (int y = 0; y < Size; y++)
            {
                map[x, y] = 0;
                correctedMap[x, y] = ' ';
                freePositions.Add(new Vector3(x, 0, y));
            }

        upperBorder = Size - Margin;
        freePositions.RemoveAll(p => p.x < Margin || p.x > upperBorder || p.z < Margin || p.z > upperBorder);
    }

    public bool AddObstacle(IObstacle obstacle)
    {
        if (!IsValidPosition(obstacle))
            return false;

        obstacles.Add(obstacle);
        int xInit = Mathf.Max(0, Mathf.RoundToInt(obstacle.Position.X - obstacle.Radius));
        int yInit = Mathf.Max(0, Mathf.RoundToInt(obstacle.Position.Z - obstacle.Radius));
        int xEnd = Mathf.Min(Mathf.RoundToInt(obstacle.Position.X + obstacle.Radius), upperBorder);
        int yEnd = Mathf.Min(Mathf.RoundToInt(obstacle.Position.Z + obstacle.Radius), upperBorder);

        for (int x = xInit; x < xEnd; x++)
            for (int y = yInit; y < yEnd; y++)
            {
                map[x, y] = 1;
                correctedMap[x, y] = 'x';
            }

        freePositions.RemoveAll(p => p.x >= xInit && p.x < xEnd && p.z >= yInit && p.z < yEnd);

        UpdateCorrectedMap(obstacle);

        return true;
    }

    /// <summary>
    /// Lanza unas líneas sobre un mapa hacia todas las direcciones donde se encuentre otro obstáculo.
    /// Así conseguimos descubrir cuando termine de inicializar el mapa las casillas inalcanzables
    /// </summary>
    /// <param name="obstacle">Obstáculo que se está poniendo en el mapa</param>
    private void UpdateCorrectedMap(IObstacle obstacle)
    {
        Vector3 position = ConvertPosition.Convert(obstacle.Position);

        int xEnd = Mathf.Min(upperBorder, Mathf.RoundToInt(obstacle.Position.X + obstacle.Radius));
        int yEnd = Mathf.Min(upperBorder, Mathf.RoundToInt(obstacle.Position.Z + obstacle.Radius));
        int xInit = Mathf.Max(0, Mathf.RoundToInt(position.x - obstacle.Radius));
        int yInit = Mathf.Max(0, Mathf.RoundToInt(position.z - obstacle.Radius));

        for (int x = xInit; x > xEnd; x++)
            for (int y = yInit; y > Margin; y--)
            {
                if (correctedMap[x, y] == 'x')
                {
                    for (int i = y + 1; i < yInit; i++)
                        if (correctedMap[x, i] != 'x')
                            correctedMap[x, i] = 'a';

                    continue;
                }
            }

        for (int y = yInit; y < yEnd; y++)
            for (int x = xEnd; x < upperBorder; x++)
            {
                if (correctedMap[x, y] == 'x')
                {
                    for (int i = x - 1; i >= xEnd; i--)
                        if (correctedMap[i, y] != 'x')
                            correctedMap[i, y] = 'a';

                    continue;
                }
            }

        for (int x = xInit; x < xEnd; x++)
            for (int y = yEnd; y < upperBorder; y++)
            {
                if (correctedMap[x, y] == 'x')
                {
                    for (int i = y - 1; i >= yEnd; i--)
                        if (correctedMap[x, i] != 'x')
                            correctedMap[x, i] = 'a';

                    continue;
                }
            }

        for (int y = yInit; y < yEnd; y++)
            for (int x = xInit; x > Margin; x--)
            {
                if (correctedMap[x, y] == 'x')
                {
                    for (int i = x + 1; i < xInit; i++)
                        if (correctedMap[i, y] != 'x')
                            correctedMap[i, y] = 'a';

                    continue;
                }
            }
    }

    /// <summary>
    /// Corrige el mapa para dejar sólo las casillas que no son alcanzables para el jugador
    /// </summary>
    public void CorrectCellUnReachables()
    {
        try
        {
            Bitmap image = ExportMapToImage.Instance.ConvertToBitMap(correctedMap, Size);
            image.Save(@"C:\temp\UnCorrectedmap.bmp");

            for (int i = 0; i < 2; i++)
            {

                for (int x = 1; x < Size - 1; x++)
                    for (int y = 1; y < Size - 1; y++)
                    {
                        char value = correctedMap[x, y];

                        if (value != ' ' && value != 'x')
                        {
                            if (correctedMap[x - 1, y] == ' ' ||
                                correctedMap[x + 1, y] == ' ' ||
                                correctedMap[x, y + 1] == ' ' ||
                                correctedMap[x, y - 1] == ' ')
                                correctedMap[x, y] = ' ';
                        }
                    }

                for (int x = Size - 1; x > 1; x--)
                    for (int y = Size - 1; y > 1; y--)
                    {
                        char value = correctedMap[x, y];

                        if (value != ' ' && value != 'x')
                        {
                            if (correctedMap[x - 1, y] == ' ' ||
                                correctedMap[x + 1, y] == ' ' ||
                                correctedMap[x, y + 1] == ' ' ||
                                correctedMap[x, y - 1] == ' ')
                                correctedMap[x, y] = ' ';
                        }
                    }

                for (int x = 1; x < Size - 1; x++)
                    for (int y = Size - 1; y > 1; y--)
                    {
                        char value = correctedMap[x, y];

                        if (value != ' ' && value != 'x')
                        {
                            if (correctedMap[x - 1, y] == ' ' ||
                                correctedMap[x + 1, y] == ' ' ||
                                correctedMap[x, y + 1] == ' ' ||
                                correctedMap[x, y - 1] == ' ')
                                correctedMap[x, y] = ' ';
                        }
                    }

                for (int x = Size - 1; x > 1; x--)
                    for (int y = 1; y < Size - 1; y++)
                    {
                        char value = correctedMap[x, y];

                        if (value != ' ' && value != 'x')
                        {
                            if (correctedMap[x - 1, y] == ' ' ||
                                correctedMap[x + 1, y] == ' ' ||
                                correctedMap[x, y + 1] == ' ' ||
                                correctedMap[x, y - 1] == ' ')
                                correctedMap[x, y] = ' ';
                        }
                    }

                image = ExportMapToImage.Instance.ConvertToBitMap(correctedMap, Size);
                image.Save(@"C:\temp\Correctedmap.bmp");

                for (int x = 1; x < Size - 1; x++)
                    for (int y = 1; y < Size - 1; y++)
                    {
                        char value = correctedMap[x, y];

                        if (value != ' ' && value != 'x')
                            positionsUnReachables.Add(new Vector3(x, 0, y));
                    }
            }
        }
        catch (Exception)
        {

        }
    }

    public bool AddDefense(IDefense defense)
    {
        int xInit = Mathf.Max(0, Mathf.RoundToInt(defense.Position.X - defense.Radius));
        int yInit = Mathf.Max(0, Mathf.RoundToInt(defense.Position.Z - defense.Radius));
        int xEnd = Mathf.Min(Mathf.RoundToInt(defense.Position.X + defense.Radius), upperBorder);
        int yEnd = Mathf.Min(Mathf.RoundToInt(defense.Position.Z + defense.Radius), upperBorder);

        for (int x = xInit; x < xEnd; x++)
            for (int y = yInit; y < yEnd; y++)
            {
                if (map[x, y] != 0)
                { }
                map[x, y] = 2;
                freePositions.RemoveAll(p => p.x == x && p.y == y);
            }

        defenses.Add(defense);

        return true;
    }

    public bool AddAlien(IAlien alien)
    {
        throw new NotImplementedException();
    }

    public bool IsValidPosition(IObstacle obstacle)
    {
        return true;
    }

    public bool IsValidPosition(IDefense defense)
    {
        return IsValidPosition(defense.Position, defense.Radius);
    }

    public bool IsValidPosition(IPosition position, float radius)
    {
        Collider[] collidersDefense = Physics.OverlapSphere(ConvertPosition.Convert(position), radius, LayerMask.GetMask("Defense"));
        Collider[] collidersObstacle = Physics.OverlapSphere(ConvertPosition.Convert(position), radius, LayerMask.GetMask("Obstacle"));
        return !collidersDefense.Any() && !collidersObstacle.Any() && !positionsUnReachables.Exists(p => p.x == position.X && p.z == position.Z);
    }

    public int[,] GetMap()
    {
        return map;
    }

    public IList<Vector3> GetFreePositions(float radius)
    {
        int upperBorderObstacle = upperBorder - Mathf.RoundToInt(radius);
        int bottomBorderObstacle = Margin + Mathf.RoundToInt(radius);

        IList<Vector3> freePositionsForRadius = new List<Vector3>();

        foreach (Vector3 position in freePositions)
            if (position.x > bottomBorderObstacle && position.z > bottomBorderObstacle &&
                position.x < upperBorderObstacle && position.z < upperBorderObstacle)
                freePositionsForRadius.Add(position);

        return freePositionsForRadius;
    }

    #endregion
}
