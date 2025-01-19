using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Health playerHealth;
    public Text healthText;

    private void Update()
    {
        UpdateHealthText();
    }

    public void UpdateHealthText()
    {
        if (playerHealth != null && healthText != null)
        {
            healthText.text = "Health: " + playerHealth.GetCurrentHealth().ToString();
        }
    }
}
