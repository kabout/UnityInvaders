using UnityEngine;
using System.Collections;

public class Pelota : MonoBehaviour {

    public float initSpeed = 600f;

    Rigidbody rigidBody;
    bool inGame = false;
    Vector3 initPosition;
    public Transform parentTransform;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start ()
    {
        initPosition = transform.position;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!inGame && Input.GetButtonDown("Fire1"))
        {
            inGame = true;
            transform.SetParent(null);
            rigidBody.isKinematic = false;
            rigidBody.AddForce(initSpeed, initSpeed, 0);
        }	
	}

    public void Reset()
    {
        transform.position = initPosition;
        transform.parent = parentTransform;
        inGame = false;
        StopMovement();
    }

    public void StopMovement()
    {
        rigidBody.isKinematic = true;
        rigidBody.velocity = Vector3.zero;
    }
}
