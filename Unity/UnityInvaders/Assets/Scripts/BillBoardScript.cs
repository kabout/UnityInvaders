using UnityEngine;
using System.Collections;

public class BillBoardScript : MonoBehaviour
{
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }
	
	void Update ()
    {
         transform.LookAt(mainCamera.transform);	
        //transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.back, 
        //    mainCamera.transform.rotation * Vector3.down);
    }
}
