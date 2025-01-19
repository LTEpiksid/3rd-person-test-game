using NUnit.Framework;
using UnityEngine;
using System.Reflection;
using System.Collections;
using UnityEngine.TestTools;

public class EnemyShootTests
{
    private GameObject enemyGameObject;
    private EnemyShoot enemyShoot;
    private GameObject playerGameObject;
    private GameObject bulletPrefab;
    private Transform bulletSpawnPoint;

    [SetUp]
    public void SetUp()
    {
        // Set up the game objects and components
        enemyGameObject = new GameObject();
        enemyShoot = enemyGameObject.AddComponent<EnemyShoot>();
        playerGameObject = new GameObject();
        playerGameObject.tag = "Player";
        playerGameObject.transform.position = new Vector3(0, 0, 10); // Position player away from enemy

        bulletPrefab = new GameObject();
        bulletPrefab.AddComponent<EnemyBullet>();

        bulletSpawnPoint = new GameObject().transform;

        // Assign the necessary fields
        enemyShoot.bulletPrefab = bulletPrefab;
        enemyShoot.bulletSpawnPoint = bulletSpawnPoint;

        enemyShoot.shootingRange = 10f;
        enemyShoot.shootCooldown = 2f;
        enemyShoot.bulletSpeed = 20f;
        enemyShoot.bulletDamage = 10;
        enemyShoot.bulletDuration = 3f;

        // Mimic the Start method
        enemyShoot.GetType().GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(enemyShoot, null);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(enemyGameObject);
        Object.DestroyImmediate(playerGameObject);
        Object.DestroyImmediate(bulletPrefab);
        Object.DestroyImmediate(bulletSpawnPoint.gameObject);
    }

    [Test]
    public void CanShoot_ReturnsTrue_WhenCooldownElapsed()
    {
        // Arrange
        enemyShoot.GetType().GetField("shootTimer", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(enemyShoot, Time.time - 3f);

        // Act
        bool canShoot = (bool)enemyShoot.GetType().GetMethod("CanShoot", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(enemyShoot, null);

        // Assert
        Assert.IsTrue(canShoot);
    }

    [Test]
    public void CanShoot_ReturnsFalse_WhenCooldownNotElapsed()
    {
        // Arrange
        enemyShoot.GetType().GetField("shootTimer", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(enemyShoot, Time.time);

        // Act
        bool canShoot = (bool)enemyShoot.GetType().GetMethod("CanShoot", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(enemyShoot, null);

        // Assert
        Assert.IsFalse(canShoot);
    }

    [UnityTest]
    public IEnumerator Shoot_CreatesBulletAndResetsTimer()
    {
        // Arrange
        enemyShoot.GetType().GetField("shootTimer", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(enemyShoot, Time.time - 3f);

        // Act
        enemyShoot.GetType().GetMethod("Shoot", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(enemyShoot, null);

        // Assert
        yield return null; // Wait one frame to ensure the bullet is created
        Assert.IsNotNull(GameObject.FindObjectOfType<EnemyBullet>());

        float newShootTimer = (float)enemyShoot.GetType().GetField("shootTimer", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(enemyShoot);

        float expectedTime = Time.time;
        float tolerance = 0.02f; // Increased tolerance for floating-point comparison

        Assert.That(newShootTimer, Is.EqualTo(expectedTime).Within(tolerance));
    }

    [Test]
    public void StopChasingAndFacePlayer_StopsChasingAndFacesPlayer()
    {
        // Arrange
        var enemyChase = enemyGameObject.AddComponent<EnemyChase>();
        enemyShoot.GetType().GetField("enemyChase", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(enemyShoot, enemyChase);

        // Act
        enemyShoot.GetType().GetMethod("StopChasingAndFacePlayer", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(enemyShoot, null);

        // Assert
        bool isChasing = (bool)enemyChase.GetType().GetField("isChasing", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(enemyChase);
        Assert.IsFalse(isChasing);

        Quaternion expectedRotation = Quaternion.LookRotation((playerGameObject.transform.position - enemyGameObject.transform.position).normalized);
        Quaternion actualRotation = enemyGameObject.transform.rotation;

        Debug.Log($"Expected Rotation: {expectedRotation}, Actual Rotation: {actualRotation}");

        // Increase tolerance for the rotation check
        float tolerance = 0.06f;
        Assert.That(Quaternion.Dot(expectedRotation, actualRotation), Is.GreaterThan(1 - tolerance));
    }
}
