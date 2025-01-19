using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 2f, -5f);
    public float rotationSpeed = 2f;
    private float mouseX, mouseY;
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        FollowPlayer();
    }

    public void FollowPlayer()
    {
        RotateCameraAroundPlayer();
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(player);
    }

    void RotateCameraAroundPlayer()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseY = Mathf.Clamp(mouseY, -20f, 60f);
            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
            offset = rotation * new Vector3(0, 2f, -5f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
