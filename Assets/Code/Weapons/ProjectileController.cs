using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed;
    public int damage;
    public float range;
    private float initialPosition;

    private void Start()
    {
        // Save the initial position for range calculation
        initialPosition = transform.position.magnitude;
    }

    private void Update()
    {
        MoveProjectile();
        CheckRange();
    }

    public void SetProperties(float projectileSpeed, int projectileDamage, float projectileRange)
    {
        speed = projectileSpeed;
        damage = projectileDamage;
        range = projectileRange;
    }

    private void MoveProjectile()
    {
        // Move the projectile
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void CheckRange()
    {
        // Check if the projectile reached its maximum range
        if (transform.position.magnitude - initialPosition >= range)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is not on the "PlayerLayer"
        if (other.gameObject.layer != LayerMask.NameToLayer("PlayerLayer"))
        {
            HandleCollision(other);
        }
    }

    private void HandleCollision(Collider other)
    {
        // Check if the collided object has a Health component
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage); // Apply damage to the target
        }

        DestroyProjectile(); // Destroy the projectile on collision
    }

    private void DestroyProjectile()
    {
        // Destroy the projectile
        if (Application.isEditor)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
