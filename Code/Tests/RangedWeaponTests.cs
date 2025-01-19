using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;
using System.Collections;

public class RangedWeaponTests
{
    private GameObject weaponGameObject;
    private RangedWeapon rangedWeapon;
    private GameObject projectilePrefab;
    private Transform playerTransform;

    [SetUp]
    public void SetUp()
    {
        // Create GameObject and add RangedWeapon component
        weaponGameObject = new GameObject();
        rangedWeapon = weaponGameObject.AddComponent<RangedWeapon>();

        // Create a prefab for the projectile
        projectilePrefab = new GameObject();
        projectilePrefab.AddComponent<ProjectileController>();
        rangedWeapon.projectilePrefab = projectilePrefab;

        // Create a GameObject for the player
        GameObject playerGameObject = new GameObject();
        playerGameObject.tag = "Player";
        playerTransform = playerGameObject.transform;

        // Assign the necessary fields
        rangedWeapon.shootingDelay = 0.5f;
        rangedWeapon.projectileSpeed = 10f;
        rangedWeapon.damage = 20;
        rangedWeapon.shootingRange = 10f;

        // Create AudioSource and AudioClip
        rangedWeapon.shootSound = AudioClip.Create("TestShootSound", 44100, 1, 44100, false);
        rangedWeapon.shootingVolume = 1.0f;

        // Mimic the Start method
        rangedWeapon.GetType().GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.Invoke(rangedWeapon, null);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(weaponGameObject);
        Object.DestroyImmediate(projectilePrefab);
        Object.DestroyImmediate(playerTransform.gameObject);
    }

    [UnityTest]
    public IEnumerator Shoot_CreatesProjectileAndPlaysSound()
    {
        // Arrange
        rangedWeapon.GetType().GetField("lastShootTime", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(rangedWeapon, Time.time - 1f);

        // Act
        rangedWeapon.GetType().GetMethod("Shoot", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.Invoke(rangedWeapon, null);

        yield return null; // Wait one frame to ensure the projectile is created

        // Assert
        Assert.IsNotNull(GameObject.FindObjectOfType<ProjectileController>());

        AudioSource audioSource = rangedWeapon.GetType().GetField("audioSource", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(rangedWeapon) as AudioSource;
        Assert.IsTrue(audioSource.isPlaying);
    }

    [Test]
    public void InitializeAudio_SetsUpAudioSource()
    {
        // Arrange
        AudioSource audioSource = rangedWeapon.GetType().GetField("audioSource", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(rangedWeapon) as AudioSource;

        // Assert
        Assert.IsNotNull(audioSource);
        Assert.AreEqual(rangedWeapon.shootSound, audioSource.clip);
        Assert.AreEqual(rangedWeapon.shootingVolume, audioSource.volume);
    }

    [Test]
    public void InitializePlayerTransform_FindsPlayerTransform()
    {
        // Arrange
        Transform playerTransform = rangedWeapon.GetType().GetField("playerTransform", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(rangedWeapon) as Transform;

        // Assert
        Assert.IsNotNull(playerTransform);
    }
}
