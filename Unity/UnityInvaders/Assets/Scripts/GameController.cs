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
using Assets.Scripts.Interfaces;
using Assets.Scripts.Manager;

namespace UnityInvaders.Managers
{
    public class GameController : MonoBehaviour
    {
        public AudioSource mainMusic;
        public AudioSource finalMusic;
        public GameObject obstaclePrefab;
        public GameObject defensePrefab;
        public GameObject floorPrefab;
        public GameObject alienPrefab;
        public GameObject spaceCamera;
        public Canvas UIGame;
        public Canvas BattleResume;
        public Canvas PauseMenu;
        public Canvas BattleInfo;
        public Canvas LoadScreen;
        public Text BattleInfoInform;
        public SelectionManager selectionManager;
        public GameStatistics gameStatistics;
        public Text timeText;
        public Text TotalTime;
        public Text TotalDefensesDestroyed;
        public Text TotalAliensDestroyed;
        public Text VelocityText;

        private GameConfiguration gameConfiguration;
        private float startTime = 0;
        private float timeForNextAlien = 0;
        private float timeForNextPulsation = 1;
        private IMap map;
        private IMapController mapController;

        void Awake()
        {
            gameConfiguration = GameConfiguration.gameConfiguration;
            Time.timeScale = (int)gameConfiguration.Velocity;
        }

        // Use this for initialization
        void Start ()
        {
            mainMusic.Play();

            BattleResume.gameObject.SetActive(false);
            BattleInfo.gameObject.SetActive(false);
            PauseMenu.gameObject.SetActive(false);
            LoadScreen.gameObject.SetActive(true);

            VelocityText.text = string.Format("Game Velocity: {0}", (GameConfiguration.GameVelocity)Time.timeScale);            
            
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

        private void InitBattle()
        {
            IStrategyFactory strategyFactory = new StrategyFactory(gameConfiguration);
            IObjectFactory objectManager = new ObjectFactory(strategyFactory, defensePrefab, obstaclePrefab, alienPrefab);

            mapController = new MapController(objectManager, strategyFactory, gameConfiguration, floorPrefab);

            map = mapController.GetEmptyMap(gameConfiguration.MapSize, 
                gameConfiguration.CellMapSize);

            mapController.InitMap(map);

            timeForNextAlien = 1 / gameConfiguration.NumAliensPerSecond;
        }

        IEnumerator SimulateBattle()
        {
            yield return null;

            InitBattle();

            LoadScreen.gameObject.SetActive(false);
            UIGame.gameObject.SetActive(true);

            startTime = Time.time;

            while (map.Defenses.Any() && GetTime() <= gameConfiguration.MaxDurationBattleInSeconds)
            {
                float timeInSecond = GetTime();
                int timeInMinute = (int)(timeInSecond / 60);
                timeInSecond = timeInSecond % 60;

                timeText.text = string.Format("Time: {0:00}:{1:00}", timeInMinute, timeInSecond);

                if (timeForNextAlien < GetTime())
                {
                    mapController.AddAlien(map);

                    timeForNextAlien = GetTime() + 1 / gameConfiguration.NumAliensPerSecond;
                }

                yield return null;
            }

            Time.timeScale = 0;
            TotalTime.text += string.Format(" {0:00} seconds", GetTime());
            TotalDefensesDestroyed.text += " " + gameStatistics.NumberOfDestroyedDefenses;
            TotalAliensDestroyed.text += " " + gameStatistics.NumberOfDiedAliens;

            BattleResume.gameObject.SetActive(true);
            UIGame.gameObject.SetActive(false);

            mainMusic.Stop();
            finalMusic.Play();
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
                sb.AppendFormat("{0} {1} died in position ({2:0.00},{3:0.00}) in {4:0.00} second.\n", 
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
            Time.timeScale = (int)gameConfiguration.Velocity;
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
