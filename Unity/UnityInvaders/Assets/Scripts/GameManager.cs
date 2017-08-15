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
        public Canvas PauseMenu;
        public Canvas BattleInfo;
        public Text BattleInfoInform;
        public SelectionManager selectionManager;
        public GameStatistics gameStatistics;
        public Text timeText;
        public Text TotalTime;
        public Text TotalDefensesDestroyed;
        public Text TotalAliensDestroyed;
        public Text VelocityText;

        private float startTime = 0;
        private float timeForNextAlien = 0;
        private float timeForNextPulsation = 1;
        private IMap map;
        private IMapController mapController;
        private IStrategyAlienAttack strategyAlienAttack;
        private IStrategySelectionDefenses strategySelectionDefenses;
        private IStrategyLocationDefenses strategyLocationDefenses;

        void Awake()
        {
            Time.timeScale = (int)GameConfiguration.gameConfiguration.Velocity;
        }

        // Use this for initialization
        void Start ()
        {
            BattleResume.gameObject.SetActive(false);
            BattleInfo.gameObject.SetActive(false);
            PauseMenu.gameObject.SetActive(false);
            UIGame.gameObject.SetActive(true);

            VelocityText.text = string.Format("Game Velocity: {0}", (GameConfiguration.GameVelocity)Time.timeScale);

            LoadStrategies();
            InitBattle();
            StartCoroutine(SimulateBattle());
        }

        void Update()
        {
            if (timeForNextPulsation > Time.time)
                return;

            if (Input.GetKey(KeyCode.P))
            {
                Time.timeScale = 0;
                PauseMenu.gameObject.SetActive(true);
                timeForNextPulsation = Time.time + 1;
            }
            else if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus))
            {
                int newVelocity = (int)Time.timeScale + 1;
                Time.timeScale = newVelocity < 5 ? newVelocity : Time.timeScale;
                VelocityText.text = string.Format("Game Velocity: {0}", (GameConfiguration.GameVelocity)Time.timeScale);
                timeForNextPulsation = Time.time + 1;
            }
            else if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
            {
                int newVelocity = (int)Time.timeScale - 1;
                Time.timeScale = newVelocity > 0 ? newVelocity : Time.timeScale;
                VelocityText.text = string.Format("Game Velocity: {0}", (GameConfiguration.GameVelocity)Time.timeScale);
                timeForNextPulsation = Time.time + 1;
            }
        }

        private void LoadStrategies()
        {
            Assembly strategyAliensAssembly = Assembly.LoadFile(GameConfiguration.gameConfiguration.StrategyAttackAliensDllPath);
            strategyAlienAttack = (IStrategyAlienAttack)strategyAliensAssembly.CreateInstance("StrategyAlienAttack.StrategyAlienAttack");
           
            Assembly strategyLocationDefensesAssembly = Assembly.LoadFile(GameConfiguration.gameConfiguration.StrategyLocationDefensesDllPath);
            strategyLocationDefenses = (IStrategyLocationDefenses)strategyLocationDefensesAssembly.CreateInstance("StrategyLocationDefenses.ManagerDefenses");

            Assembly strategySelectionDefensesAssembly = Assembly.LoadFile(GameConfiguration.gameConfiguration.StrategySelectionDefensesDllPath);
            strategySelectionDefenses = (IStrategySelectionDefenses)strategySelectionDefensesAssembly.CreateInstance("StrategySelectionDefenses.StrategySelectionDefenses");
        }

        private void InitBattle()
        {
            IObjectManager objectManager = new ObjectManager(strategyAlienAttack, defensePrefab, obstaclePrefab, alienPrefab);

            mapController = new MapController(objectManager, strategySelectionDefenses, strategyLocationDefenses, 
                strategyAlienAttack, GameConfiguration.gameConfiguration, floorPrefab);

            map = mapController.GetEmptyMap(GameConfiguration.gameConfiguration.MapSize, 
                GameConfiguration.gameConfiguration.CellMapSize);

            mapController.InitMap(map);

            timeForNextAlien = 1 / GameConfiguration.gameConfiguration.NumAliensPerSecond;
        }

        IEnumerator SimulateBattle()
        {
            startTime = Time.time;

            while (map.Defenses.Any() && GetTime() <= GameConfiguration.gameConfiguration.MaxDurationBattleInSeconds)
            {
                float timeInSecond = GetTime();
                int timeInMinute = (int)(timeInSecond / 60);
                timeInSecond = timeInSecond % 60;

                timeText.text = string.Format("Time: {0:00}:{1:00}", timeInMinute, timeInSecond);

                if (timeForNextAlien < GetTime())
                {
                    mapController.AddAliens(map);

                    timeForNextAlien = GetTime() + 1 / GameConfiguration.gameConfiguration.NumAliensPerSecond;
                }

                yield return null;
            }

            Time.timeScale = 0;
            TotalTime.text += string.Format(" {0:00} seconds", GetTime());
            TotalDefensesDestroyed.text += " " + gameStatistics.NumberOfDestroyedDefenses;
            TotalAliensDestroyed.text += " " + gameStatistics.NumberOfDiedAliens;

            BattleResume.gameObject.SetActive(true);
            UIGame.gameObject.SetActive(false);
        }

        private float GetTime()
        {
            return Time.time - startTime;
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
            BattleResume.gameObject.SetActive(false);
        }

        private void GenerateBattleInfo(List<DiedEntity> diedEntities)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine();

            foreach(DiedEntity diedEntity in diedEntities)
            {
                sb.AppendFormat("{0} {1} died in position ({2},{3}) in {4} second.\n", 
                    diedEntity.Type == EntityType.Alien ? "Alien" : "Defense",
                    diedEntity.Id,
                    diedEntity.X,
                    diedEntity.Z,
                    diedEntity.Time - startTime);
            }

            BattleInfoInform.text = sb.ToString();
            BattleInfo.gameObject.SetActive(true);
        }

        public void Resume()
        {
            PauseMenu.gameObject.SetActive(false);
            Time.timeScale = (int)GameConfiguration.gameConfiguration.Velocity;
        }

        public void Quit()
        {
            StopCoroutine(SimulateBattle());
            SceneManager.LoadScene("Menu");
        }

        public void BackToResumenBattle()
        {
            BattleInfo.gameObject.SetActive(false);
            BattleResume.gameObject.SetActive(true);

        }
    }
}
