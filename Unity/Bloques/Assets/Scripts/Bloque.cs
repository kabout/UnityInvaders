using UnityEngine;
using System.Collections;

public class Bloque : MonoBehaviour {

    public GameObject Particles;

    // IsTrigger = false
    void OnCollisionEnter()
    {
        Instantiate(Particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
