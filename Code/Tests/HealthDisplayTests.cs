using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayTests
{
    private GameObject healthDisplayGameObject;
    private HealthDisplay healthDisplay;
    private Health playerHealth;
    private Text healthText;

    [SetUp]
    public void SetUp()
    {
        // Create GameObject and add HealthDisplay component
        healthDisplayGameObject = new GameObject();
        healthDisplay = healthDisplayGameObject.AddComponent<HealthDisplay>();

        // Create the Health component and set the initial health
        GameObject healthGameObject = new GameObject();
        playerHealth = healthGameObject.AddComponent<Health>();
        playerHealth.SetCurrentHealth(100);

        // Create the Text component
        GameObject textGameObject = new GameObject();
        healthText = textGameObject.AddComponent<Text>();
        healthText.text = string.Empty;

        // Assign components to HealthDisplay
        healthDisplay.playerHealth = playerHealth;
        healthDisplay.healthText = healthText;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(healthDisplayGameObject);
        Object.DestroyImmediate(playerHealth.gameObject);
        Object.DestroyImmediate(healthText.gameObject);
    }

    [Test]
    public void UpdateHealthText_SetsHealthText_WhenPlayerHealthIsNotNull()
    {
        // Act
        healthDisplay.UpdateHealthText();

        // Assert
        Assert.AreEqual("Health: 100", healthText.text);
    }

    [Test]
    public void UpdateHealthText_DoesNotThrowException_WhenPlayerHealthOrHealthTextIsNull()
    {
        // Arrange
        healthDisplay.playerHealth = null;

        // Act & Assert
        Assert.DoesNotThrow(() => healthDisplay.UpdateHealthText());

        // Reset for next test
        healthDisplay.playerHealth = playerHealth;
        healthDisplay.healthText = null;

        // Act & Assert
        Assert.DoesNotThrow(() => healthDisplay.UpdateHealthText());
    }
}
