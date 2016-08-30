using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityInvaders.Controllers;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Managers
{
    public class GameManager : MonoBehaviour
    {
        public int level;
        public GameObject obstacle;
        public GameObject defense;
        public GameObject floor;
        public GameObject alien;
        public GameObject spaceCamera;
        public SelectionManager selectionManager;
        public Text timeText;

        private const int MIN_OBSTACLE_RADIO = 15;
        private const int MAX_OBSTACLE_RADIO = 30;
        private const float MAX_OBSTACLES_PER_AREA_UNIT = 0.0005f;
        private const float MAX_DEFENSES_PER_AREA_UNIT = 0.0005f;
        private const int DEFAULT_DEFENSE_RADIO = 5;
        private float difficulty;
        private int numObstacles;
        private int numDefenses;
        private int size;
        private List<GameObject> obstacles;
        private List<GameObject> defenses;

        // Use this for initialization
        void Start ()
        {
            IDifficultController difficultController = new DifficultController(999);
            IObjectManager objectManager = new ObjectManager(difficultController, defense, obstacle);
            IDefenseController defenseController = new DefenseController(difficultController, objectManager);
            IMapController mapController = new MapController(defenseController, difficultController, objectManager, floor);
            IMap map = mapController.GetEmptyMap(700);
            mapController.InitMap(map);
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
        }

        public void OnGUI ()
        {
            //GUILayout.Button("Nex state");    
        }
    }
}
