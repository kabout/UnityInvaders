using StrategyAlienAttack;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Threading;

namespace UnityInvaders.Managers
{
    public class GameManager : MonoBehaviour
    {
        public int level;
        public GameObject obstaclePrefab;
        public GameObject defensePrefab;
        public GameObject floorPrefab;
        public GameObject alienPrefab;
        public GameObject spaceCamera;
        public SelectionManager selectionManager;
        public GameConfiguration gameConfiguration;
        public Text timeText;
        private float timeForNextAlien = 0;
        private IMap map;
        private IMapController mapController;

        void Awake()
        {
            Time.timeScale = (int)gameConfiguration.Velocity;
        }

        // Use this for initialization
        void Start ()
        {
            Assembly strategyAliensAssembly = Assembly.LoadFile(gameConfiguration.StrategyAttackAliensDllPath);
            IStrategyAlienAttack strategyAlienAttack = (IStrategyAlienAttack)strategyAliensAssembly.CreateInstance("StrategyAlienAttack.StrategyAlienAttack");
            
            IObjectManager objectManager = new ObjectManager(strategyAlienAttack, defensePrefab, obstaclePrefab, alienPrefab);

            Assembly strategyLocationDefensesAssembly = Assembly.LoadFile(gameConfiguration.StrategyLocationDefensesDllPath);
            IStrategyLocationDefenses strategyLocationDefenses = (IStrategyLocationDefenses)strategyLocationDefensesAssembly.CreateInstance("StrategyLocationDefenses.ManagerDefenses");
            mapController = new MapController(objectManager, strategyAlienAttack, strategyLocationDefenses, gameConfiguration, floorPrefab);
            map = mapController.GetEmptyMap(gameConfiguration.SizeMap, gameConfiguration.CellMap);
            mapController.InitMap(map);

            int[,] mapBit = map.GetMap();

            string mapString = string.Empty;

            for (int i = 0; i < map.Size; i++)
            {
                for (int j = 0; j < map.Size; j++)
                    mapString += mapBit[i, j].ToString() + " ";
                mapString += "\n";
            }

            timeForNextAlien = 1 / gameConfiguration.NumUcosPerSecond; 
        }

        // Update is called once per frame
        void Update()
        {
            while (map.Defenses.Any() || Time.time > gameConfiguration.MaxDurationBattleInSeconds)
            {
                float timeInSecond = Time.time;
                int timeInMinute = (int)(timeInSecond / 60);
                timeInSecond = timeInSecond % 60;

                timeText.text = string.Format("Time: {0:00}:{1:00}", timeInMinute, timeInSecond);

                if (timeForNextAlien >= Time.time)
                    return;

                mapController.AddAliens(map);

                timeForNextAlien = Time.time + 1 / gameConfiguration.NumUcosPerSecond;
            }

            Time.timeScale = 0;
            Thread.Sleep(1);
        }

        public void SetFastVelocity()
        {
            Time.timeScale = (int)GameConfiguration.GameVelocity.Fast;
        }
    }
}
