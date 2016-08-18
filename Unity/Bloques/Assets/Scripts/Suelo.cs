using UnityEngine;
using System.Collections;

public class Suelo : MonoBehaviour
{
    public Vidas vidas;

    void OnTriggerEnter()
    {
        vidas.LostLife();
    }
}
