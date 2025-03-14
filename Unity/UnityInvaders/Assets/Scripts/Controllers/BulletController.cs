﻿using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float Dispersion;
    public float Damage;
    public GameObject Owner;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 2);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!Owner || !other.gameObject)
            return;

        if (other.gameObject == Owner)
            return;

        if (other.gameObject.CompareTag("Alien") && !Owner.CompareTag("Alien"))
        {
            other.gameObject.GetComponent<UnityAlien>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Defense") && !Owner.CompareTag("Defense"))
        {
            other.gameObject.GetComponent<UnityDefense>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Grid"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, Dispersion, LayerMask.GetMask("Alien"));

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("Alien"))
                    collider.gameObject.GetComponent<UnityAlien>().TakeDamage(CalculateDamage(collider.gameObject.transform.position));
            }
        }
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        if (Dispersion == 0)
            return 0;

        // Create a vector from the shell to the target.
        Vector3 explosionToTarget = targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float explosionDistance = explosionToTarget.sqrMagnitude;

        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float relativeDistance = (Dispersion - explosionDistance) / Dispersion;

        // Calculate damage as this proportion of the maximum possible damage.
        float damage = relativeDistance * Damage;

        // Make sure that the minimum damage is always 0.
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}