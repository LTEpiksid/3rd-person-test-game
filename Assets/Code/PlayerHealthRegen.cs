using UnityEngine;

public class HealthRegeneration : MonoBehaviour
{
    private Health healthScript;

    public int healthRegenAmount = 5; 
    public float healthRegenInterval = 2f; 

    private void Start()
    {
        healthScript = GetComponent<Health>();

        if (healthScript == null)
        {
            Debug.LogError("Health script not found on the attached object.");
            enabled = false; 
        }
        else
        {
            InvokeRepeating("RegenerateHealth", healthRegenInterval, healthRegenInterval);
        }
    }

    private void RegenerateHealth()
    {
        if (healthScript != null)
        {
            healthScript.RegenerateHealth(healthRegenAmount);
        }
    }
}
