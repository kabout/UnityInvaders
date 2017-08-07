using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : IObjectManager
{
    #region Fields

    public GameObject defensePrefab;
    public GameObject obstaclePrefab;
    public GameObject alienPrefab;
    
    IStrategyAlienAttack strategyAlienAttack;
    private static int nextDefenseId = 1;
    private static int nextObstacleId = 1;
    private static int nextAlienId = 1;

    #endregion

    public ObjectManager(IStrategyAlienAttack strategyAlienAttack, GameObject defensePrefab, GameObject obstaclePrefab, GameObject alienPrefab)
    {
        RandomManager.Seed = DateTime.Now.Millisecond;
        this.strategyAlienAttack = strategyAlienAttack;

        this.defensePrefab = defensePrefab;
        this.obstaclePrefab = obstaclePrefab;
        this.alienPrefab = alienPrefab;
    }

    public IDefense GenerateDefense(IPosition position)
    {
        float defenseSize = Constants.DEFAULT_DEFENSE_RADIO;

        //Instanciar la defensa
        GameObject defense = GameObject.Instantiate(defensePrefab, ConvertPosition.Convert(position), Quaternion.identity) as GameObject;

        if (defense == null)
            return null;

        defense.transform.localScale = new Vector3(defenseSize, defenseSize, defenseSize);

        // Inicializar los valores de la defensa
        UnityDefense unityDefense = defense.GetComponent(typeof(UnityDefense)) as UnityDefense;

        unityDefense.id = nextDefenseId;
        unityDefense.cost = Constants.DEFAULT_DEFENSE_COST;
        unityDefense.damage = Constants.DEFAULT_DEFENSE_DAMAGE;
        unityDefense.dispersion = Constants.DEFAULT_DEFENSE_DISPERSION;
        unityDefense.health = Constants.DEFAULT_DEFENSE_HEALTH;
        unityDefense.range = Constants.DEFAULT_DEFENSE_RANGE;
        unityDefense.selected = false;
        unityDefense.type = 0;

        nextDefenseId++;

        return unityDefense;
    }

    public IAlien GenerateAlien(IPosition position)
    {
        float alienSize = Constants.DEFAULT_ALIEN_RADIO;


        
        GameObject alien = GameObject.Instantiate(alienPrefab, ConvertPosition.Convert(position), Quaternion.identity) as GameObject;

        if (alien == null)
            return null;

        alien.transform.localScale = new Vector3(alienSize, alienSize, alienSize);

        // Inicializar los valores de la defensa
        UnityAlien unityAlien = alien.GetComponent(typeof(UnityAlien)) as UnityAlien;

        unityAlien.id = nextAlienId;
        unityAlien.damage = Constants.DEFAULT_ALIEN_DAMAGE;
        unityAlien.health = Constants.DEFAULT_ALIEN_HEALTH;
        unityAlien.range = Constants.DEFAULT_ALIEN_RANGE;
        unityAlien.selected = false;

        MoveAlien moveAlien = alien.GetComponent(typeof(MoveAlien)) as MoveAlien;
        moveAlien.strategyAlienAttack = strategyAlienAttack;

        nextAlienId++;

        return unityAlien;
    }

    public IObstacle GenerateObstacle(IMap map, int minRadius, int maxRadius)
    {
        float radius = RandomManager.GetRandomNumber(minRadius, maxRadius);
        float sizeObstacle = radius;

        IList<Vector3> freePositions = map.GetFreePositions(radius);
        Vector3 position;
            
        int index = RandomManager.GetRandomNumber(0, freePositions.Count);
        position = freePositions[index];
        freePositions.RemoveAt(index);

        //Instanciar la obstacle
        GameObject obstacle = GameObject.Instantiate(obstaclePrefab, position, Quaternion.identity) as GameObject;

        if (obstacle == null)
            return null;

        obstacle.transform.localScale = new Vector3(sizeObstacle, sizeObstacle, sizeObstacle);

        // Inicializar los valores de la defensa
        UnityObstacle unityObstacle = obstacle.GetComponent(typeof(UnityObstacle)) as UnityObstacle;
        unityObstacle.id = nextObstacleId;

        nextObstacleId++;

        return unityObstacle;
    }
}
