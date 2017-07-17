using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class MapController : IMapController
{
    #region Fields

    GameObject mapPrefab;
    IDifficultController difficultController;
    IObjectManager objectManager;
    IStrategyAlienAttack strategyAlienAttack;
    IStrategyLocationDefenses strategyLocationDefenses;

    #endregion

    #region Constructors

    public MapController(IDifficultController difficultController,
        IObjectManager objectManager, IStrategyAlienAttack strategyAlienAttack, IStrategyLocationDefenses strategyLocationDefenses, GameObject mapPrefab)
    {
        this.difficultController = difficultController;
        this.objectManager = objectManager;
        this.strategyAlienAttack = strategyAlienAttack;
        this.strategyLocationDefenses = strategyLocationDefenses;
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
        unityMap.margin = 15; // Mathf.RoundToInt(size * 0.1f);
        unityMap.cellSize = cellSize;
        unityMap.InitMap();

        return unityMap;
    }

    public void AddAliens(IMap map)
    {
        int alienMargin = map.Margin + Constants.ALIEN_SIZE + 1;

        List<Vector3> freePostionForAliens = map.GetFreePositions(Constants.ALIEN_SIZE).Where(p => p.x == alienMargin ||
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
            
        Bitmap image = ExportMapToImage.Instance.ConvertToBitMap(map.GetMap(), map.Size);
        image.Save(@"C:\temp\map.bmp");
    }       
      

    private void PlaceObstacles(IMap map)
    {
        int numOfObstacles = difficultController.GetNumberOfObstacles(map);

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

        int numDefenses = difficultController.GetNumberOfDefenses(map);

        //List<Vector3> freePositions = map.GetFreePositions(Constants.DEFAULT_DEFENSE_RADIO).ToList();



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
