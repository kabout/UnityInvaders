using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityInvaders.Interfaces;
using UnityInvaders.Controllers;
using UnityInvaders.Managers;
using UnityInvaders.Transforms;
using UnityInvaders.Model;

public class GameManager : MonoBehaviour 
{
	public int level;
	public GameObject obstacle;
	public GameObject defense;
	public GameObject floor;
	public GameObject alien;
	public GameObject spaceCamera;

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
    void Start()
    {
        IDifficultController difficultController = new DifficultController(UnityInvaders.Model.DifficultLevel.God);
        IObjectManager objectManager = new ObjectManager(difficultController);
		IDefenseController defenseController = new DefenseController(difficultController, objectManager);
        IMapController mapController = new MapController(defenseController, difficultController, objectManager);
        IMap map = mapController.GetEmptyMap(100, 100);
        mapController.InitMap(map);
        MapToUnity mapToUnity = new MapToUnity(floor, obstacle, defense);
        UnityMap unityMap = mapToUnity.Convert(map);
    }
	
	// Update is called once per frame
	void Update () 
	{
	
	}

    public void OnGUI()
    {
        GUILayout.Button("Nex state");    
    }
}
