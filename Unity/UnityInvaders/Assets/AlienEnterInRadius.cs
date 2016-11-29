using UnityEngine;
using System.Collections;
using UnityInvaders.Model;

public class AlienEnterInRadius : MonoBehaviour
{
    public GameObject Alien;

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Defense"))
        //{
        //    UnityAlien unityAlien = Alien.GetComponent<UnityAlien>();

        //    if (!unityAlien.Target)
        //        unityAlien.Target = other.transform;
        //}
    }
}
