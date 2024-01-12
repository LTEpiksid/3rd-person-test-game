using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  
    public Transform spawnPoint;    
    public float spawnInterval = 5f; 
    public int maxSpawnedEnemies = 10; 
    public float spawnRadius = 5f;   

    private int spawnedEnemiesCount = 0; 
    private GameObject currentEnemy; 

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (spawnedEnemiesCount >= maxSpawnedEnemies)
        {
            return; 
        }

        if (currentEnemy == null)
        {
            Vector3 randomSpawnPosition = spawnPoint.position + Random.insideUnitSphere * spawnRadius;
            randomSpawnPosition.y = spawnPoint.position.y; 

            currentEnemy = Instantiate(enemyPrefab, randomSpawnPosition, spawnPoint.rotation);

            currentEnemy.SendMessage("SetSpawner", this, SendMessageOptions.DontRequireReceiver);

            spawnedEnemiesCount++;
        }
    }

    public void DecreaseSpawnedEnemiesCount()
    {
        spawnedEnemiesCount = Mathf.Max(0, spawnedEnemiesCount - 1);
        currentEnemy = null; 
    }
}
