using NUnit.Framework;
using UnityEngine;

public class EnemyDamageTests
{
    private GameObject enemyGameObject;
    private EnemyDamage enemyDamage;
    private GameObject playerGameObject;
    private Health playerHealth;

    [SetUp]
    public void SetUp()
    {
        // Set up the game objects and components
        enemyGameObject = new GameObject();
        enemyDamage = enemyGameObject.AddComponent<EnemyDamage>();

        playerGameObject = new GameObject();
        playerGameObject.tag = "Player";
        playerHealth = playerGameObject.AddComponent<Health>();
        playerHealth.maxHealth = 100;

        enemyDamage.damageAmount = 10f;
        enemyDamage.damageInterval = 1f;
        enemyDamage.damageRadius = 5f;

        // Ensure the player's health is initialized
        playerHealth.GetType().GetMethod("Start", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(playerHealth, null);

        playerGameObject.transform.position = enemyGameObject.transform.position + new Vector3(0, 0, 1); // Within radius
        Debug.Log($"Player initial health: {playerHealth.GetCurrentHealth()}");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(enemyGameObject);
        Object.DestroyImmediate(playerGameObject);
    }

    [Test]
    public void DealDamage_DoesNotDamagePlayer_WhenOutOfRadius()
    {
        // Arrange
        playerGameObject.transform.position = enemyGameObject.transform.position + new Vector3(0, 0, 10); // Outside radius
        playerHealth.SetCurrentHealth(100); // Ensure health is set to max
        int initialHealth = playerHealth.GetCurrentHealth();

        // Act
        enemyDamage.DealDamage();  // Directly calling the public method

        // Assert
        Assert.AreEqual(initialHealth, playerHealth.GetCurrentHealth());
    }

    [Test]
    public void DealDamage_UpdatesLastDamageTime_WhenDamageIsDealt()
    {
        // Arrange
        playerGameObject.transform.position = enemyGameObject.transform.position + new Vector3(0, 0, 1); // Within radius
        enemyDamage.GetType().GetField("lastDamageTime", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(enemyDamage, Time.time - enemyDamage.damageInterval);
        playerHealth.SetCurrentHealth(100); // Ensure health is set to max

        // Act
        enemyDamage.DealDamage();  // Directly calling the public method

        // Assert
        float lastDamageTime = (float)enemyDamage.GetType().GetField("lastDamageTime", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.GetValue(enemyDamage);
        Assert.AreEqual(Time.time, lastDamageTime);
    }
}
