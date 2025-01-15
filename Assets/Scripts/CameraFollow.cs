using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player's Transform
    [SerializeField] private Vector3 offset;   // Offset to maintain relative camera position
    [SerializeField] private float smoothSpeed = 5.0f; // How smoothly the camera follows the player

    void LateUpdate()
    {
        if (player != null)
        {
            // Desired position based on player's position and offset
            Vector3 desiredPosition = new Vector3(player.position.x + offset.x, offset.y, offset.z);

            // Smoothly interpolate to the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
