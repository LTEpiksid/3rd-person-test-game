using NUnit.Framework;
using UnityEngine;
using System.Reflection;

public class HealthTests
{
    private GameObject gameObject;
    private Health health;
    private GameObject playerGameObject;
    private ParticleSystem damageParticles;

    [SetUp]
    public void SetUp()
    {
        gameObject = new GameObject();
        health = gameObject.AddComponent<Health>();
        health.maxHealth = 100;
        health.damageParticlesPrefab = new GameObject().AddComponent<ParticleSystem>();

        playerGameObject = new GameObject();
        playerGameObject.tag = "Player";
        playerGameObject.transform.position = Vector3.zero;

        gameObject.tag = "Enemy";  // Ensure the tag is set before Start is called
        health.GetType().GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(health, null);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(gameObject);
        if (playerGameObject != null)
        {
            Object.DestroyImmediate(playerGameObject);
        }
    }

    [Test]
    public void InitializesCurrentHealthToMaxHealth()
    {
        Assert.AreEqual(health.maxHealth, health.GetCurrentHealth());
    }

    [Test]
    public void TakeDamage_ReducesHealthCorrectly()
    {
        int damage = 30;
        int expectedHealth = health.GetCurrentHealth() - damage;

        health.TakeDamage(damage);

        Assert.AreEqual(expectedHealth, health.GetCurrentHealth());
    }

    [Test]
    public void TakeDamage_InstantiatesDamageParticles()
    {
        Object.DestroyImmediate(GameObject.FindObjectOfType<ParticleSystem>()); // Destroy existing particles to isolate the test

        health.TakeDamage(10);

        Assert.IsNotNull(GameObject.FindObjectOfType<ParticleSystem>());
    }

    [Test]
    public void TakeDamage_WhenHealthAtOrBelowZero_DestroysEnemy()
    {
        health.TakeDamage(health.GetCurrentHealth());

        Assert.IsTrue(gameObject == null); // Object should be destroyed
    }

    [Test]
    public void RegenerateHealth_IncreasesHealthCorrectly()
    {
        health.TakeDamage(50);
        health.RegenerateHealth(30);

        Assert.AreEqual(80, health.GetCurrentHealth());
    }

    [Test]
    public void RegenerateHealth_DoesNotExceedMaxHealth()
    {
        health.TakeDamage(20);
        health.RegenerateHealth(50);

        Assert.AreEqual(health.maxHealth, health.GetCurrentHealth());
    }
}
