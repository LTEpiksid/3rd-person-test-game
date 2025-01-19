using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab of the projectile to be shot
    public float shootingDelay = 0.5f; // Delay between shots
    public float projectileSpeed = 10f; // Speed of the projectile
    public int damage = 20; // Damage dealt to the target
    public float shootingRange = 10f; // Maximum shooting range

    public AudioClip shootSound; // Sound effect for shooting
    [Range(0.0f, 1.0f)] public float shootingVolume = 1.0f; // Volume of the shooting sound

    private float lastShootTime; // Time of the last shot
    private Transform playerTransform; // Reference to the player's transform
    private AudioSource audioSource; // AudioSource component for playing the sound

    private bool isShooting; // Flag to check if shooting is in progress
    private bool isDualShootingEnabled = false; // Flag to check if dual shooting is enabled

    private void Start()
    {
        InitializeAudio();
        InitializePlayerTransform();
        isShooting = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && CanShoot())
        {
            isShooting = true;
            Shoot();
            isShooting = false;
        }
    }

    private void InitializeAudio()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = shootSound;
        audioSource.volume = shootingVolume;
    }

    private void InitializePlayerTransform()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure the player is tagged as 'Player'.");
        }
    }

    private void PlayShootingSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound, shootingVolume);
        }
    }

    private bool CanShoot()
    {
        return Time.time - lastShootTime > shootingDelay;
    }

    private void Shoot()
    {
        if (CanShoot())
        {
            lastShootTime = Time.time;
            PlayShootingSound();
            if (isDualShootingEnabled)
            {
                InstantiateDualProjectiles();
            }
            else
            {
                InstantiateProjectile(transform.position);
            }
        }
    }

    private void InstantiateProjectile(Vector3 position)
    {
        if (playerTransform != null)
        {
            Vector3 playerDirection = playerTransform.forward;
            GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.LookRotation(playerDirection));
            SetProjectileProperties(projectile);
        }
    }

    private void InstantiateDualProjectiles()
    {
        Vector3 leftPosition = transform.position + transform.right * -0.5f; // Position the left projectile
        Vector3 rightPosition = transform.position + transform.right * 0.5f; // Position the right projectile

        InstantiateProjectile(leftPosition);
        InstantiateProjectile(rightPosition);
    }

    private void SetProjectileProperties(GameObject projectile)
    {
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        if (projectileController != null)
        {
            projectileController.SetProperties(projectileSpeed, damage, shootingRange);
        }
    }

    public void UpgradeProjectile()
    {
        projectilePrefab.transform.localScale *= 3f; // Make the projectile 3x larger
        ProjectileController projectileController = projectilePrefab.GetComponent<ProjectileController>();
        if (projectileController != null)
        {
            int upgradedDamage = projectileController.damage * 2;
            projectileController.SetProperties(projectileController.speed, upgradedDamage, projectileController.range);
        }
    }

    public void EnableDualShooting()
    {
        isDualShootingEnabled = true;
    }
}
