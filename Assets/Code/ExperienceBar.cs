using UnityEngine;
using UnityEngine.UI;

public class ExperienceSystem : MonoBehaviour
{
    public Text xpText;

    public float maxExperience = 100f; 
    private float currentExperience = 0f;

    private void Start()
    {
        UpdateUI();
    }

    public void AddExperience(float amount)
    {
        if (amount < 0f)
        {
            Debug.LogError("Cannot add negative experience.");
            return;
        }

        currentExperience += amount;
        currentExperience = Mathf.Clamp(currentExperience, 0f, maxExperience);
        UpdateUI();
    }

    private void UpdateUI()
    {
        float fillAmount = currentExperience / maxExperience;
        xpText.text = $"XP: {currentExperience} / {maxExperience}";
    }
}
