using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;       
    public float jumpForce = 8f;    
    public float jumpCooldown = 1f;   

    private float lastJumpTime;      
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastJumpTime = -jumpCooldown;  
    }

    void Update()
    {
        MovePlayer();
        Jump();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = Camera.main.transform.forward;

        cameraForward.y = 0f;

        cameraForward.Normalize();

        Vector3 moveDirection = cameraForward * vertical + Camera.main.transform.right * horizontal;

        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        if (moveDirection.magnitude > 0f)
        {
            Quaternion toRotation = Quaternion.LookRotation(cameraForward, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 0.15f);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && Time.time - lastJumpTime > jumpCooldown)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

            lastJumpTime = Time.time;
        }
    }
}