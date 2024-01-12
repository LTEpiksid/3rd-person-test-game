using UnityEngine;

public class EnemyDash : MonoBehaviour
{
    public float dashRadius = 5f;      
    public float dashDuration = 1f;    
    public float dashCooldown = 3f;     
    public float dashSpeed = 10f;       

    private Transform player;           
    private bool isDashing = false;   
    private float dashTimer = 0f;       
    private float cooldownTimer = 0f;  
    private Vector3 dashDirection;      

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!isDashing)
        {
            if (Vector3.Distance(transform.position, player.position) <= dashRadius)
            {
                StartDash();
            }
        }
        else
        {
            Dash();
        }
    }

    private void StartDash()
    {
        if (Time.time - cooldownTimer >= dashCooldown)
        {
            isDashing = true;
            dashTimer = 0f;
            dashDirection = (player.position - transform.position).normalized;
        }
    }

    private void Dash()
    {
        if (dashTimer < dashDuration)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            dashTimer += Time.deltaTime;
        }
        else
        {
            isDashing = false;
            cooldownTimer = Time.time;
        }
    }
}
