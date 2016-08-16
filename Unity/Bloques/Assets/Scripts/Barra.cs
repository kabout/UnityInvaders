using UnityEngine;
using System.Collections;

public class Barra : MonoBehaviour
{
    public float Speed = 0.4f;
    private Vector3 initialPosition;

	// Use this for initialization
	void Start ()
    {
        initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            float posX = transform.position.x + (horizontal * Speed * Time.deltaTime);
            transform.position = new Vector3(Mathf.Clamp(posX, -8, 8), transform.position.y, transform.position.z);
        }
    }

    public void Reset()
    {
        transform.position = initialPosition;
    }
}
