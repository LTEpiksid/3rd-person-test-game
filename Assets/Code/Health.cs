using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public ParticleSystem damageParticlesPrefab;
    private EnemyChase enemyChase;

    private void Start()
    {
        Debug.Log("Health Start method called");
        currentHealth = maxHealth;
        Debug.Log($"Initial currentHealth: {currentHealth}");

        if (gameObject.CompareTag("Enemy"))
        {
            enemyChase = GetComponent<EnemyChase>();
            Debug.Log("EnemyChase component found");
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"TakeDamage called with damage: {damage}");
        currentHealth -= damage;
        Debug.Log($"Current Health after damage: {currentHealth}");

        if (damageParticlesPrefab != null)
        {
            Instantiate(damageParticlesPrefab, transform.position, Quaternion.identity);
        }

        if (gameObject.CompareTag("Enemy") && currentHealth > 0)
        {
            if (enemyChase != null)
            {
                Debug.Log("Starting to chase player");
                enemyChase.StartChasing();
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        Debug.Log($"Final Current Health after TakeDamage method: {currentHealth}");
    }

    public void SetCurrentHealth(int health)
    {
        currentHealth = health;
        Debug.Log($"SetCurrentHealth called, Current Health: {currentHealth}");
    }

    void Die()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            if (Application.isPlaying)
            {
                Destroy(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }
        else if (gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has died.");
        }
    }

    public int GetCurrentHealth()
    {
        Debug.Log($"GetCurrentHealth called, Current Health: {currentHealth}");
        return currentHealth;
    }

    public void RegenerateHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log($"RegenerateHealth called, Current Health: {currentHealth}");
    }
}
