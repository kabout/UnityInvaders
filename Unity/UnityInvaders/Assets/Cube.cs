using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

    void Awake()
    {
        GameObject map = GameObject.FindGameObjectWithTag("Floor");
        UnityMap iMap = map.GetComponent<UnityMap>();
        iMap.InitMap();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
