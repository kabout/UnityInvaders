using UnityEngine;
using System.Collections;

public class GameManagerTest : MonoBehaviour {

    public GameObject cube;
    public GameObject floor;

	// Use this for initialization
	void Start () {

        floor.transform.localScale = new Vector3(100, 1, 100); 
	
        for(int i = 0; i < 100; i++)
            for(int j = 0; j < 100; j++)
            {
                GameObject.Instantiate(cube, new Vector3(i + 0.5f, 1, -j - 0.5f), Quaternion.identity);
            }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
