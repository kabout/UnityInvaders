using System.Reflection;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Threading;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Text;

namespace UnityInvaders.Managers
{
    public class GameManager : MonoBehaviour
    {
        public GameObject obstaclePrefab;
        public GameObject defensePrefab;
        public GameObject floorPrefab;
        public GameObject alienPrefab;
        public GameObject spaceCamera;
        public Canvas UIGame;
        public Canvas BattleResume;
        public SelectionManager selectionManager;
        public GameConfiguration gameConfiguration;
        public GameStatistics gameStatistics;
        public Text timeText;

        private float startTime = 0;
        private float timeForNextAlien = 0;
        private IMap map;
        private IMapController mapController;
        private IStrategyAlienAttack strategyAlienAttack;
        private IStrategyLocationDefenses strategyLocationDefenses;

        void Awake()
        {
            Time.timeScale = (int)gameConfiguration.Velocity;
        }

        // Use this for initialization
        void Start ()
        {
            BattleResume.enabled = false;
            UIGame.enabled = true;

            LoadStrategies();
            InitBattle();

            StartCoroutine(SimulateBattle());
        }

        private void LoadStrategies()
        {
            Assembly strategyAliensAssembly = Assembly.LoadFile(gameConfiguration.StrategyAttackAliensDllPath);
            strategyAlienAttack = (IStrategyAlienAttack)strategyAliensAssembly.CreateInstance("StrategyAlienAttack.StrategyAlienAttack");
           
            Assembly strategyLocationDefensesAssembly = Assembly.LoadFile(gameConfiguration.StrategyLocationDefensesDllPath);
            strategyLocationDefenses = (IStrategyLocationDefenses)strategyLocationDefensesAssembly.CreateInstance("StrategyLocationDefenses.ManagerDefenses");
        }

        private void InitBattle()
        {
            IObjectManager objectManager = new ObjectManager(strategyAlienAttack, defensePrefab, obstaclePrefab, alienPrefab);
            mapController = new MapController(objectManager, strategyAlienAttack, strategyLocationDefenses, gameConfiguration, floorPrefab, gameStatistics);
            map = mapController.GetEmptyMap(gameConfiguration.MapSize, gameConfiguration.CellMapSize);
            mapController.InitMap(map);

            timeForNextAlien = 1 / gameConfiguration.NumAliensPerSecond;
        }

        IEnumerator SimulateBattle()
        {
            startTime = Time.time;

            while (map.Defenses.Any() || GetTime() > gameConfiguration.MaxDurationBattleInSeconds)
            {
                float timeInSecond = GetTime();
                int timeInMinute = (int)(timeInSecond / 60);
                timeInSecond = timeInSecond % 60;

                timeText.text = string.Format("Time: {0:00}:{1:00}", timeInMinute, timeInSecond);

                if (timeForNextAlien < GetTime())
                {
                    mapController.AddAliens(map);

                    timeForNextAlien = GetTime() + 1 / gameConfiguration.NumAliensPerSecond;
                }

                yield return null;
            }
                        
            Time.timeScale = 0;

            GameObject.Find("TotalTime").GetComponent<Text>().text += string.Format(" {0:00} seconds", GetTime());
            GameObject.Find("TotalDefensesDestroyed").GetComponent<Text>().text += " " + gameStatistics.NumberOfDestroyedDefenses;
            GameObject.Find("TotalAliensDestroyed").GetComponent<Text>().text += " " + gameStatistics.NumberOfDiedAliens;

            BattleResume.enabled = true;
            UIGame.enabled = false;
            Thread.Sleep(1);
        }

        float GetTime()
        {
            return Time.time - startTime;
        }

        public void SetFastVelocity()
        {
            Time.timeScale = (int)GameConfiguration.GameVelocity.Fast;
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void RestartBattle()
        {
            SceneManager.UnloadScene("GameScene");
            SceneManager.LoadScene("GameScene");
        }

        public void SeeFullBattleInfo()
        {
            List<DiedEntity> diedEntities = gameStatistics.GetDiedEntitiesOrderByTime();

            GenerateBattleInfo(diedEntities);
        }

        private void GenerateBattleInfo(List<DiedEntity> diedEntities)
        {
            StringBuilder sb = new StringBuilder();

            foreach(DiedEntity diedEntity in diedEntities)
            {
                sb.AppendFormat("{0} {1} died in position ({2},{3}) in {4} second.\n", 
                    diedEntity.Type == EntityType.Alien ? "Alien" : "Defense",
                    diedEntity.Id,
                    diedEntity.X,
                    diedEntity.Z,
                    diedEntity.Time - startTime);
            } 
        }
    }
}
