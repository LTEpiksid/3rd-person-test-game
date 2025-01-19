using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyChase : EnemyMovement
{
    public float originalDetectionRadius = 10f;
    private float currentDetectionRadius;

    private Transform player;
    private bool isChasing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentDetectionRadius = originalDetectionRadius;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= currentDetectionRadius)
        {
            StartChasing();
        }

        if (isChasing)
        {
            Chase(player.position);
        }
    }

    private void Chase(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        RotateEnemy(targetPosition - transform.position);
    }

    public void StartChasing()
    {
        isChasing = true;
    }

    public void StopChasing()
    {
        isChasing = false;
    }

    public void SetDetectionRadius(float newRadius)
    {
        currentDetectionRadius = newRadius;
    }

    public void EnterShootingRange()
    {
        SetDetectionRadius(0f);
    }

    public void ExitShootingRange()
    {
        SetDetectionRadius(originalDetectionRadius);
    }
}
