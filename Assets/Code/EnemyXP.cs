using UnityEngine;

public class EnemyXP : MonoBehaviour
{
    public float experienceValue = 10f; 

    private bool isDead = false;
    private ExperienceSystem playerXP;

    private void Start()
    {
        playerXP = FindObjectOfType<ExperienceSystem>();
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;

            if (playerXP != null)
            {
                playerXP.AddExperience(experienceValue);
            }

            Destroy(gameObject);
        }
    }

    public void CheckAndDie()
    {
        if (!isDead && IsDefeated())
        {
            Die();
        }
    }

    private bool IsDefeated()
    {
        return true;
    }

    private void OnDisable()
    {
        if (!isDead && playerXP != null)
        {
            playerXP.AddExperience(experienceValue);
        }
    }
}
