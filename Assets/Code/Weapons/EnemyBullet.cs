using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 20f;
    private int damage = 10;
    private float duration = 3f;
    private float elapsedTime = 0f;
    private Health playerHealth;  

    public void SetProperties(float projectileSpeed, int projectileDamage, float projectileDuration, Health playerHealth)
    {
        speed = projectileSpeed;
        damage = projectileDamage;
        duration = projectileDuration;
        this.playerHealth = playerHealth;
    }

    void Update()
    {
        MoveBullet();
        CheckCollision();
    }

    private void MoveBullet()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        elapsedTime += Time.deltaTime;

        if (duration > 0 && elapsedTime >= duration)
        {
            DestroyProjectile();
        }
    }

    private void CheckCollision()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.5f))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
            {
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }

                DestroyProjectile();
            }
            else if (hit.collider.gameObject.layer != LayerMask.NameToLayer("EnemyLayer"))
            {
                DestroyProjectile();
            }
        }
    }


    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
