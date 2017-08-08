using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

    void Awake()
    {
        GameObject map = GameObject.FindGameObjectWithTag("Floor");
        UnityMap iMap = map.GetComponent<UnityMap>();
        iMap.InitMap();
        MoveAlien moveAlien = gameObject.GetComponent(typeof(MoveAlien)) as MoveAlien;
        //moveAlien.strategyAlienAttack = new StrategyAlienAttack.StrategyAlienAttack();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
