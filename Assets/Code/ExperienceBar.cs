using UnityEngine;
using UnityEngine.UI;

public class ExperienceSystem : MonoBehaviour
{
    public Text xpText;
    public GameObject levelUpPanel;
    public Button abilityButton1;
    public Button abilityButton2;
    public Button abilityButton3;
    public RangedWeapon playerWeapon; // Reference to the player's weapon

    public float maxExperience = 100f;
    private float currentExperience = 0f;

    private void Start()
    {
        UpdateUI();
        levelUpPanel.SetActive(false); // Hide the level-up panel initially

        // Assign button listeners
        abilityButton1.onClick.AddListener(() => ChooseAbility(1));
        abilityButton2.onClick.AddListener(() => ChooseAbility(2));
        abilityButton3.onClick.AddListener(() => ChooseAbility(3));
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

        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }

    private void UpdateUI()
    {
        float fillAmount = currentExperience / maxExperience;
        xpText.text = $"XP: {currentExperience} / {maxExperience}";
    }

    private void LevelUp()
    {
        levelUpPanel.SetActive(true); // Show the level-up panel when max experience is reached
    }

    private void ChooseAbility(int abilityIndex)
    {
        switch (abilityIndex)
        {
            case 1:
                ApplyUpgrade1();
                break;
            case 2:
                ApplyUpgrade2();
                break;
            case 3:
                // Implement other abilities here
                break;
        }

        // Reset experience or proceed with other logic after choosing an ability
        currentExperience = 0f;
        UpdateUI();
        levelUpPanel.SetActive(false); // Hide the level-up panel after choosing an ability
    }

    private void ApplyUpgrade1()
    {
        if (playerWeapon != null)
        {
            playerWeapon.UpgradeProjectile();
        }
    }

    private void ApplyUpgrade2()
    {
        if (playerWeapon != null)
        {
            playerWeapon.EnableDualShooting();
        }
    }
}
