using StrategyAlienAttack;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

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

        private const int MIN_OBSTACLE_RADIO = 15;
        private const int MAX_OBSTACLE_RADIO = 30;
        private const float MAX_OBSTACLES_PER_AREA_UNIT = 0.0005f;
        private const float MAX_DEFENSES_PER_AREA_UNIT = 0.0005f;
        private const int DEFAULT_DEFENSE_RADIO = 5;
        private float timeForNextAlien = 0;
        private float difficulty;
        private int numObstacles;
        private int numDefenses;
        private int size;
        private IMap map;
        private IMapController mapController;
        private List<GameObject> obstacles;
        private List<GameObject> defenses;

        // Use this for initialization
        void Start ()
        {
            Assembly strategyAliensAssembly = Assembly.LoadFile(gameConfiguration.StrategyAttackAliensDllPath);
            IStrategyAlienAttack strategyAlienAttack = (IStrategyAlienAttack)strategyAliensAssembly.CreateInstance("StrategyAlienAttack.StrategyAlienAttack");

            IDifficultController difficultController = new DifficultController(400);
            IObjectManager objectManager = new ObjectManager(difficultController, strategyAlienAttack, defensePrefab, obstaclePrefab, alienPrefab);

          Assembly strategyLocationDefensesAssembly = Assembly.LoadFile(gameConfiguration.StrategyLocationDefensesDllPath);
            IStrategyLocationDefenses strategyLocationDefenses = (IStrategyLocationDefenses)strategyLocationDefensesAssembly.CreateInstance("StrategyLocationDefenses.ManagerDefenses");
            mapController = new MapController(difficultController, objectManager, strategyAlienAttack, strategyLocationDefenses, floorPrefab);
            map = mapController.GetEmptyMap(400, 10);
            mapController.InitMap(map);

            timeForNextAlien = 1 / Constants.ALIEN_PER_SECOND;            
            
            //MapToUnity mapToUnity = new MapToUnity(floor, obstacle, defense);
            //UnityMap unityMap = mapToUnity.Convert(map);

            //float defenseSize = DEFAULT_DEFENSE_RADIO * 2;

            //Vector3 position = new Vector3(5, 0, 5);
            //GameObject gameObject = Instantiate(defense, position, Quaternion.identity) as GameObject;
            //gameObject.transform.localScale = new Vector3(defenseSize, defenseSize, defenseSize);
            //UnityDefense unityDefense = gameObject.GetComponent(typeof(UnityDefense)) as UnityDefense;

            //if (unityDefense != null)
            //{
            //    unityDefense.id = 1;
            //    unityDefense.health = 100;
            //    unityDefense.damage = 30;
            //    unityDefense.dispersion = 1;
            //    unityDefense.range = 10;
            //    unityDefense.cost = 100;
            //}

            //position = new Vector3(15, 0, 15);
            //GameObject gameObject1 = Instantiate(defense, position, Quaternion.identity) as GameObject;
            //gameObject1.transform.localScale = new Vector3(defenseSize, defenseSize, defenseSize);
            //UnityDefense unityDefense1 = gameObject1.GetComponent(typeof(UnityDefense)) as UnityDefense;

            //if (unityDefense1 != null)
            //{
            //    unityDefense1.id = 2;
            //    unityDefense1.health = 150;
            //    unityDefense1.damage = 40;
            //    unityDefense1.dispersion = 2;
            //    unityDefense1.range = 12;
            //    unityDefense1.cost = 200;
            //}
        }

        // Update is called once per frame
        void Update ()
        {
            float timeInSecond = Time.time;
            int timeInMinute = (int)(timeInSecond / 60);
            timeInSecond = timeInSecond % 60;

            timeText.text = string.Format("Time: {0:00}:{1:00}", timeInMinute, timeInSecond);

            if (timeForNextAlien >= Time.time)
                return;
            
            mapController.AddAliens(map);

            timeForNextAlien = Time.time + 1/Constants.ALIEN_PER_SECOND; 
        }

        public void OnGUI ()
        {
            //GUILayout.Button("Nex state");    
        }
    }
}
