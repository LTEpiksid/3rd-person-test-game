using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject respawnPrefab; 
    public int minSpawnCount = 1;     
    public int maxSpawnCount = 3;    

    public bool isQuitting = false; // Public for testing

    void OnApplicationQuit()
    {
        isQuitting = true;
        Debug.Log("Application quitting. isQuitting set to true.");
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy called. isQuitting: " + isQuitting);
        if (!isQuitting)
        {
            SpawnEnemies();
        }
        else
        {
            Debug.Log("OnDestroy: Not spawning enemies because isQuitting is true.");
        }
    }

    public void SpawnEnemies() // Changed to public for testing
    {
        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount); // Changed to make upper bound exclusive

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = transform.position + Random.onUnitSphere * 2f;
            spawnPosition.y = 0f;

            GameObject newPrefab = Instantiate(respawnPrefab, spawnPosition, Quaternion.identity);
            newPrefab.name = "RespawnPrefab_" + i; // Unique naming
            Debug.Log($"Enemy spawned at: {spawnPosition} with name: {newPrefab.name}"); // Log creation
        }
    }
}
