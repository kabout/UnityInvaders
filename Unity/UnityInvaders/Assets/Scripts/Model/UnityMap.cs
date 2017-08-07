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

    public IList<IDefense> Defenses { get { return defenses.Where(x => x.IsAlive()).ToList(); } }

    public IList<IAlien> Aliens { get { return aliens.Where(x =>x.IsAlive()).ToList(); } }

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
            for (int z = 0; z < Size; z++)
            {
                map[x, z] = 0;
                correctedMap[x, z] = ' ';
                freePositions.Add(new Vector3(x, 0, -z));
            }

        upperBorder = Size - Margin;
        freePositions.RemoveAll(p => p.x < Margin || p.x > upperBorder || Mathf.Abs(p.z) < Margin || Mathf.Abs(p.z) > upperBorder);
    }

    public bool AddObstacle(IObstacle obstacle)
    {
        if (!IsValidPosition(obstacle))
            return false;

        float zPosition = Mathf.Abs(obstacle.Position.Z);

        obstacles.Add(obstacle);
        int xInit = Mathf.FloorToInt(obstacle.Position.X - obstacle.Radius);
        int zInit = Mathf.FloorToInt(zPosition - obstacle.Radius);
        int xEnd = Mathf.CeilToInt(obstacle.Position.X + obstacle.Radius);
        int zEnd = Mathf.CeilToInt(zPosition + obstacle.Radius);

        for (int x = xInit; x < xEnd; x++)
            for (int z = zInit; z < zEnd; z++)
            {
                map[x, z] = 1;
                correctedMap[x, z] = 'x';
            }

        freePositions.RemoveAll(p => p.x >= xInit && p.x < xEnd && Mathf.Abs(p.z) >= zInit && Mathf.Abs(p.z) < zEnd);

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
        float zPosition = Mathf.Abs(obstacle.Position.Z);

        int xInit = Mathf.FloorToInt(obstacle.Position.X - obstacle.Radius);
        int zInit = Mathf.FloorToInt(zPosition - obstacle.Radius);
        int xEnd = Mathf.CeilToInt(obstacle.Position.X + obstacle.Radius);
        int zEnd = Mathf.CeilToInt(zPosition + obstacle.Radius);

        for (int z = zInit; z > Margin; z--)
            for (int x = xInit; x > xEnd; x++)
            {
                if (correctedMap[x, z] == 'x')
                {
                    for (int i = z + 1; i < zInit; i++)
                        if (correctedMap[x, i] != 'x')
                            correctedMap[x, i] = 'a';

                    continue;
                }
            }

        for (int z = zInit; z < zEnd; z++)
            for (int x = xEnd; x < upperBorder; x++)
            {
                if (correctedMap[x, z] == 'x')
                {
                    for (int i = x - 1; i >= xEnd; i--)
                        if (correctedMap[i, z] != 'x')
                            correctedMap[i, z] = 'a';

                    continue;
                }
            }

        for (int x = xInit; x < xEnd; x++)
            for (int z = zEnd; z < upperBorder; z++)
            {
                if (correctedMap[x, z] == 'x')
                {
                    for (int i = z - 1; i >= zEnd; i--)
                        if (correctedMap[x, i] != 'x')
                            correctedMap[x, i] = 'a';

                    continue;
                }
            }

        for (int z = zInit; z < zEnd; z++)
            for (int x = xInit; x > Margin; x--)
            {
                if (correctedMap[x, z] == 'x')
                {
                    for (int i = x + 1; i < xInit; i++)
                        if (correctedMap[i, z] != 'x')
                            correctedMap[i, z] = 'a';

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
        float zPosition = Mathf.Abs(defense.Position.Z);

        int xInit = Mathf.FloorToInt(defense.Position.X - defense.Radius);
        int zInit = Mathf.FloorToInt(zPosition - defense.Radius);
        int xEnd = Mathf.CeilToInt(defense.Position.X + defense.Radius);
        int zEnd = Mathf.CeilToInt(zPosition + defense.Radius);

        for (int x = xInit; x < xEnd; x++)
            for (int z = zInit; z < zEnd; z++)
            {
                map[x, z] = 2;
                freePositions.RemoveAll(p => p.x == x && Mathf.Abs(p.z) == z);
            }

        defenses.Add(defense);

        return true;
    }

    public bool AddAlien(IAlien alien)
    {
        aliens.Add(alien);
        return true;
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

    public List<Vector3> GetFreePositions(float radius)
    {
        int upperBorderObstacle = upperBorder - Mathf.RoundToInt(radius);
        int bottomBorderObstacle = Margin + Mathf.RoundToInt(radius);

        List<Vector3> freePositionsForRadius = new List<Vector3>();

        foreach (Vector3 position in freePositions)
        {
            float zPosition = Mathf.Abs(position.z);

            if (position.x > bottomBorderObstacle && zPosition > bottomBorderObstacle &&
                position.x < upperBorderObstacle && zPosition < upperBorderObstacle)
                freePositionsForRadius.Add(position);
        }

        return freePositionsForRadius;
    }

    #endregion
}
