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

    private void Start()
    {
        InitializeAudio();
        InitializePlayerTransform();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
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

    private void Shoot()
    {
        if (Time.time - lastShootTime > shootingDelay)
        {
            lastShootTime = Time.time;
            PlayShootingSound();
            InstantiateProjectile();
        }
    }

    private void InstantiateProjectile()
    {
        if (playerTransform != null)
        {
            Vector3 playerDirection = playerTransform.forward;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(playerDirection));
            SetProjectileProperties(projectile);
        }
    }

    private void SetProjectileProperties(GameObject projectile)
    {
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        if (projectileController != null)
        {
            projectileController.SetProperties(projectileSpeed, damage, shootingRange);
        }
    }
}