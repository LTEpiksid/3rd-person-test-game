using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damageAmount = 10f;
    public float damageInterval = 1f;
    public float damageRadius = 5f;

    private float lastDamageTime;

    private void Update()
    {
        if (Time.time - lastDamageTime >= damageInterval)
        {
            DealDamage();
        }
    }

    public void DealDamage()  // Changed to public
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        bool damageDealt = false;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                float distanceToPlayer = Vector3.Distance(collider.transform.position, transform.position);
                Debug.Log($"Player found at distance: {distanceToPlayer}");

                if (distanceToPlayer <= damageRadius)
                {
                    int damageToInt = Mathf.RoundToInt(damageAmount);

                    Health playerHealth = collider.GetComponent<Health>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damageToInt);
                        damageDealt = true; // Set flag to true when damage is dealt
                    }

                    break;
                }
            }
        }

        Debug.Log($"Damage Dealt: {damageDealt}");
        Debug.Log($"lastDamageTime before update: {lastDamageTime}");

        if (damageDealt)
        {
            lastDamageTime = Time.time; // Update only if damage was actually dealt
            Debug.Log($"lastDamageTime updated: {lastDamageTime}");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
