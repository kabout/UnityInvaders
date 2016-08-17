using UnityEngine;
using System.Collections;

public class Bloque : MonoBehaviour {

    public GameObject Particles;
    public Puntos puntos;

    // IsTrigger = false
    void OnCollisionEnter()
    {
        Instantiate(Particles, transform.position, Quaternion.identity);
        gameObject.transform.SetParent(null);
        Destroy(gameObject);
        puntos.AddScore(1);
    }
}
