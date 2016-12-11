using UnityEngine;
using System.Collections;

public class EnterInRadius : MonoBehaviour
{
    public GameObject Defense;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Alien"))
        {
            UnityDefense unityDefense = Defense.GetComponent<UnityDefense>();

            if (!unityDefense.Target)
                unityDefense.Target = other.gameObject.transform;
        }
    }
}
