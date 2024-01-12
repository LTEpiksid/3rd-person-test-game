using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject respawnPrefab; 
    public int minSpawnCount = 1;     
    public int maxSpawnCount = 3;    

    private bool isQuitting = false; 

    void OnApplicationQuit()
    {
        isQuitting = true; 
    }

    void OnDestroy()
    {
        if (!isQuitting)
        {
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = transform.position + Random.onUnitSphere * 2f;
            spawnPosition.y = 0f; 

            Instantiate(respawnPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
