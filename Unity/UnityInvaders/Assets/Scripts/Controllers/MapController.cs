using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class MapController : IMapController
{
    #region Fields

    GameObject mapPrefab;
    IObjectManager objectManager;
    IStrategyAlienAttack strategyAlienAttack;
    IStrategyLocationDefenses strategyLocationDefenses;
    GameConfiguration gameConfiguration;

    #endregion

    #region Constructors

    public MapController(IObjectManager objectManager, IStrategyAlienAttack strategyAlienAttack,
        IStrategyLocationDefenses strategyLocationDefenses, GameConfiguration gameConfiguration, GameObject mapPrefab)
    {
        this.objectManager = objectManager;
        this.strategyAlienAttack = strategyAlienAttack;
        this.strategyLocationDefenses = strategyLocationDefenses;
        this.gameConfiguration = gameConfiguration;
        this.mapPrefab = mapPrefab;
    }

    #endregion

    #region Methods

    public IMap GetEmptyMap(int size, int cellSize)
    {
        GameObject map = GameObject.Instantiate(mapPrefab, new Vector3(size / 2, 0, size / 2), Quaternion.identity) as GameObject;

        if (map == null)
            return null;

        map.transform.localScale = new Vector3(size, 0, size);

        // Inicializar los valores de la defensa
        UnityMap unityMap = map.GetComponent(typeof(UnityMap)) as UnityMap;
        unityMap.margin = Constants.MARGIN_MAP;
        unityMap.cellSize = cellSize;
        unityMap.InitMap();

        return unityMap;
    }

    public void AddAliens(IMap map)
    {
        int alienMargin = map.Margin + Constants.DEFAULT_ALIEN_RADIO + 1;

        List<Vector3> freePostionForAliens = map.GetFreePositions(Constants.DEFAULT_ALIEN_RADIO).Where(p => p.x == alienMargin ||
            p.y == alienMargin || p.x == map.Size - alienMargin || p.y == map.Size - alienMargin).ToList();

        if (!freePostionForAliens.Any())
            return;

        int index = RandomManager.GetRandomNumber(0, freePostionForAliens.Count - 1);
        Vector3 position = freePostionForAliens[index];
        IPosition alienPosition = new Position(position.x, 3, position.z);

        IAlien alien = objectManager.GenerateAlien(alienPosition);
        map.AddAlien(alien);
    }

    public void InitMap(IMap map)
    {
        PlaceObstacles(map);
        map.CorrectCellUnReachables();
        PlaceDefenses(map);

#if DEBUG
        Bitmap image = ExportMapToImage.Instance.ConvertToBitMap(map.GetMap(), map.Size);
        image.Save(@"C:\temp\map.bmp");
#endif
    }    
    
    private int GetNumberOfObstacles(int mapSize)
    {
        int maxCellForObstacle = (int)Math.Pow(Constants.MAX_OBSTACLE_RADIUS * 2, 2);
        int minCellForObstacle = (int)Math.Pow(Constants.MIN_OBSTACLE_RADIUS * 2, 2);
        int meanCellForObstacle = (maxCellForObstacle + minCellForObstacle) / 2;

        return (int)Math.Floor((Math.Pow(mapSize, 2) * gameConfiguration.DensityObstacles) / meanCellForObstacle);
    }

    private int GetNumberOfDefenses(int mapSize)
    {
        int maxCellForObstacle = (int)Math.Pow(Constants.MAX_OBSTACLE_RADIUS * 2, 2);
        int minCellForObstacle = (int)Math.Pow(Constants.MIN_OBSTACLE_RADIUS * 2, 2);
        int meanCellForObstacle = (maxCellForObstacle + minCellForObstacle) / 2;

        return (int)Math.Floor((Math.Pow(mapSize, 2) * gameConfiguration.DensityDefenses) / meanCellForObstacle);
    }

    private void PlaceObstacles(IMap map)
    {
        int numOfObstacles =  GetNumberOfObstacles(map.Size);

        while (numOfObstacles > 0)
        {
            IObstacle obstacle = objectManager.GenerateObstacle(map, Constants.MIN_OBSTACLE_RADIUS, Constants.MAX_OBSTACLE_RADIUS);

            if (obstacle == null)
                return;

            map.AddObstacle(obstacle);

            numOfObstacles--;
        }
    }

    private void PlaceDefenses(IMap map)
    {
        strategyLocationDefenses.InitStrategy(map.Obstacles, Constants.DEFAULT_DEFENSE_RADIO, map.Size, map.CellSize);

        int numDefenses = GetNumberOfDefenses(map.Size);

        while (numDefenses > 0)
        {
            IPosition position = strategyLocationDefenses.GetPositionForDefense(map.Defenses);

            IPosition positionZero = new Position(0, 0, 0);

            if (position == positionZero)
                return;

            while (strategyAlienAttack.CalculatePath(position, positionZero, map.Obstacles, map.Defenses, map.Size, map.CellSize / 2).Count == 0)
                position = strategyLocationDefenses.GetPositionForDefense(map.Defenses);

            IDefense defense = objectManager.GenerateDefense(position);

            if (defense == null)
                return;

            map.AddDefense(defense);

            numDefenses--;
        }
    }

    #endregion
}
