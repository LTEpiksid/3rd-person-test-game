using UnityEngine;
using UnityEngine.Playables;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public ParticleSystem damageParticlesPrefab;
    private EnemyChase enemyChase;  

    private void Start()
    {
        currentHealth = maxHealth;

        if (gameObject.CompareTag("Enemy"))
        {
            enemyChase = GetComponent<EnemyChase>();  
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (damageParticlesPrefab != null)
        {
            Instantiate(damageParticlesPrefab, transform.position, Quaternion.identity);
        }

        if (gameObject.CompareTag("Enemy") && currentHealth > 0)
        {
            if (enemyChase != null)
            {
                enemyChase.StartChasing();
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has died.");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public void RegenerateHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}
