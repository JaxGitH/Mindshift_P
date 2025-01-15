using UnityEngine;
using UnityEngine.InputSystem;

public class LScr_PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 7f; // Movement speed
    [SerializeField] private float mantleHeight = 2.0f; // Max height for mantling
    [SerializeField] private float mantleDistance = 1.5f; // Max distance for mantling
    [SerializeField] private float mantleSpeed = 5f; // Speed of mantling movement

    [Header("Joystick Settings")]
    //[SerializeField] public VJoystick joystick; // Drag your VJoystick prefab here

    private bool isMantling = false; // Flag to track mantling state
    private Vector3 mantleTarget; // Target position for mantling

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true; // Prevent Rigidbody from tipping over
    }

    void Update()
    {
        if (!isMantling)
        {
            HandleMovement();
            CheckForMantle();
        }
        else
        {
            PerformMantle();
        }
    }

    /// <summary>
    /// Handles movement restricted to Forward/Backward direction.
    /// </summary>
    void HandleMovement()
    {
        // Read joystick input
        //float joystickVertical = joystick; // Forward/Backward from joystick

        // Read keyboard input
        float keyboardVertical = Input.GetAxis("Vertical"); // Forward/Backward from keyboard (W/S)

        // Combine inputs
        //float finalVertical = joystickVertical + keyboardVertical;

        // Apply movement restricted to Z-axis (forward/backward only)
        //Vector3 movement = new Vector3(0f, 0f, finalVertical);
        //transform.Translate(movement.normalized * speed * Time.deltaTime);
    }

    /// <summary>
    /// Detects mantleable objects in front of the player using a raycast.
    /// </summary>
    void CheckForMantle()
    {
        RaycastHit hit;

        // Adjust origin closer to Player's chest
        Vector3 rayOrigin = transform.position + Vector3.up * 0.75f;
        Vector3 rayDirection = transform.forward;

        Debug.DrawRay(rayOrigin, rayDirection * mantleDistance, Color.green);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, mantleDistance))
        {
            if (hit.collider != null && hit.collider.CompareTag("Mantleable"))
            {
                float objectHeight = hit.point.y - transform.position.y;

                if (objectHeight > 0 && objectHeight <= mantleHeight)
                {
                    StartMantle(hit.collider.bounds.center);
                }
            }
        }
    }

    /// <summary>
    /// Starts the mantling process, setting the target position.
    /// </summary>
    void StartMantle(Vector3 targetPosition)
    {
        isMantling = true;
        mantleTarget = new Vector3(
            transform.position.x,
            targetPosition.y + 1.0f, // Adjust slightly above for smooth placement
            transform.position.z
        );

        // Disable Rigidbody physics during mantling
        playerRb.isKinematic = true;
    }

    /// <summary>
    /// Smoothly moves the player to the mantle target position.
    /// </summary>
    void PerformMantle()
    {
        transform.position = Vector3.MoveTowards(transform.position, mantleTarget, mantleSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, mantleTarget) < 0.1f)
        {
            isMantling = false;
            playerRb.isKinematic = false; // Re-enable physics after mantling
        }
    }

    /// <summary>
    /// Visual Debug Ray for Mantling Detection (Editor Only).
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 rayOrigin = transform.position + Vector3.up * 1.0f;
        Gizmos.DrawRay(rayOrigin, transform.forward * mantleDistance);
    }

    /// <summary>
    /// Resets mantling state upon landing after falling or finishing a mantle.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.5f)
        {
            isMantling = false;
            playerRb.isKinematic = false;
        }
    }
}

