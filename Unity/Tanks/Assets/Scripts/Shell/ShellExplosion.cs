using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask TankMask;
    public ParticleSystem ExplosionParticles;       
    public AudioSource ExplosionAudio;              
    public float MaxDamage = 100f;                  
    public float ExplosionForce = 1000f;            
    public float MaxLifeTime = 2f;                  
    public float ExplosionRadius = 5f;              


    private void Start()
    {
        Destroy(gameObject, MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, TankMask);

        foreach(Collider collider in colliders)
        {
            Rigidbody targetRigidBody = collider.GetComponent<Rigidbody>();

            if (targetRigidBody == null)
                continue;

            targetRigidBody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);
            TankHealth targetHealth = collider.GetComponent<TankHealth>();

            if (targetHealth == null)
                continue;

            float damage = CalculateDamage(targetRigidBody.position);
            targetHealth.TakeDamage(damage);
        }
        ExplosionParticles.transform.SetParent(null);
        ExplosionParticles.Play();
        ExplosionAudio.Play();

        Destroy(gameObject);
        Destroy(ExplosionParticles, ExplosionParticles.duration);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistance = Mathf.Min(explosionToTarget.magnitude, ExplosionRadius);
        float relativeDistance = (ExplosionRadius - explosionDistance) / ExplosionRadius;

        return relativeDistance * MaxDamage;
    }
}