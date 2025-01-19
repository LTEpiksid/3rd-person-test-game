using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;

public class EnemyRespawnTests
{
    private GameObject enemyRespawnGameObject;
    private EnemyRespawn enemyRespawn;
    private GameObject respawnPrefab;

    [SetUp]
    public void SetUp()
    {
        // Ensure a clean state by destroying any existing respawnPrefab instances
        CleanupRespawnPrefabs();

        // Create GameObject and add EnemyRespawn component
        enemyRespawnGameObject = new GameObject("EnemyRespawn");
        enemyRespawn = enemyRespawnGameObject.AddComponent<EnemyRespawn>();

        // Create a prefab for respawning enemies
        respawnPrefab = new GameObject("RespawnPrefab");
        enemyRespawn.respawnPrefab = respawnPrefab;

        // Set spawn count range
        enemyRespawn.minSpawnCount = 1;
        enemyRespawn.maxSpawnCount = 3;

        Debug.Log("Setup completed.");
    }

    [TearDown]
    public void TearDown()
    {
        CleanupRespawnPrefabs();
        if (enemyRespawnGameObject != null)
        {
            Object.DestroyImmediate(enemyRespawnGameObject);
        }
        if (respawnPrefab != null)
        {
            Object.DestroyImmediate(respawnPrefab);
        }
        Debug.Log("Teardown completed.");
    }

    private void CleanupRespawnPrefabs()
    {
        var existingPrefabs = GameObject.FindObjectsOfType<GameObject>();
        foreach (var prefab in existingPrefabs)
        {
            if (prefab.name.StartsWith("RespawnPrefab"))
            {
                Object.DestroyImmediate(prefab);
            }
        }
    }

    [Test]
    public void OnDestroy_SpawnsEnemies_WhenNotQuitting()
    {
        // Arrange
        enemyRespawn.isQuitting = false;

        // Act
        Object.DestroyImmediate(enemyRespawnGameObject);

        // Assert
        int spawnCount = GameObject.FindObjectsOfType<GameObject>().Count(obj => obj.name.StartsWith("RespawnPrefab"));
        Debug.Log($"Spawn Count: {spawnCount}");
        Assert.GreaterOrEqual(spawnCount, enemyRespawn.minSpawnCount);
        Assert.LessOrEqual(spawnCount, enemyRespawn.maxSpawnCount);
    }

    [Test]
    public void SpawnEnemies_SpawnsBetweenMinAndMaxCount()
    {
        // Arrange
        enemyRespawn.isQuitting = false;

        // Act
        enemyRespawn.SpawnEnemies();

        // Assert
        int spawnCount = GameObject.FindObjectsOfType<GameObject>().Count(obj => obj.name.StartsWith("RespawnPrefab"));
        Debug.Log($"Spawn Count: {spawnCount}");
        Assert.GreaterOrEqual(spawnCount, enemyRespawn.minSpawnCount);
        Assert.LessOrEqual(spawnCount, enemyRespawn.maxSpawnCount);
    }
}
