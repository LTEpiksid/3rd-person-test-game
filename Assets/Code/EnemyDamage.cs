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

    private void DealDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                int damageToInt = Mathf.RoundToInt(damageAmount);

                Health playerHealth = collider.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageToInt);
                }

                lastDamageTime = Time.time;
                break;
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
