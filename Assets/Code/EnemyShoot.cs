using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float shootingRange = 10f;
    public float shootCooldown = 2f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    public float bulletSpeed = 20f;
    public int bulletDamage = 10;
    public float bulletDuration = 3f;

    private Transform player;
    private bool isShooting = false;
    private float shootTimer = 0f;

    private EnemyChase enemyChase;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyChase = GetComponent<EnemyChase>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= shootingRange)
        {
            StopChasingAndFacePlayer();
            enemyChase.SetDetectionRadius(0f);

            if (CanShoot())
            {
                Shoot();
            }
        }
        else
        {
            enemyChase.SetDetectionRadius(enemyChase.originalDetectionRadius);
        }
    }

    private void StopChasingAndFacePlayer()
    {
        enemyChase.StopChasing();
        Debug.Log("Facing player");
        transform.LookAt(player); // Ensure the object faces the player
    }

    private bool CanShoot()
    {
        return Time.time - shootTimer >= shootCooldown;
    }

    private void Shoot()
    {
        isShooting = true;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();

        if (enemyBullet != null)
        {
            enemyBullet.SetProperties(bulletSpeed, bulletDamage, bulletDuration, player.GetComponent<Health>());
        }

        isShooting = false;
        shootTimer = Time.time;
    }
}
