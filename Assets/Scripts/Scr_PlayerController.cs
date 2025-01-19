using UnityEngine;

public class Scr_PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;         // Left/right movement speed
    public float stepHeight = 0.5f;      // Maximum height the player can step up
    public float mantleHeight = 1.5f;    // Maximum height the player can mantle
    public float mantleSpeed = 5f;       // Speed of mantling over obstacles

    [Header("References")]
    public Rigidbody rb;                // Reference to the player's Rigidbody
    public Mindshift.VJoystick joystick; // Reference to the UI joystick (optional)

    private bool isMantling = false;    // Tracks whether the player is currently mantling

    private void Start()
    {
        // Ensure Rigidbody is assigned
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        // Handle movement
        Move();

        // Perform step-up and mantle checks if not currently mantling
        if (!isMantling)
        {
            StepUpOrMantle();
        }
    }

    private void Move()
    {
        // Combine input from the keyboard and joystick
        float inputX = Input.GetAxis("Horizontal");

        if (joystick != null)
        {
            inputX += joystick.Horizontal; // Add joystick input
        }

        // Apply movement to Rigidbody
        Vector3 velocity = rb.linearVelocity;
        velocity.x = inputX * moveSpeed;
        rb.linearVelocity = velocity;
    }

    private void StepUpOrMantle()
    {
        RaycastHit hit;

        // Raycast forward from slightly above the ground
        Vector3 rayStart = transform.position + Vector3.up * 0.1f; // Just above ground level
        Vector3 rayDirection = transform.forward;                 // Forward direction

        float rayDistance = 0.6f; // Adjust as needed for obstacle detection range

        // Perform a raycast to detect obstacles in front of the player
        if (Physics.Raycast(rayStart, rayDirection, out hit, rayDistance))
        {
            float obstacleHeight = hit.point.y - transform.position.y; // Height difference

            // Debugging: Visualize the raycast in the scene view
            Debug.DrawRay(rayStart, rayDirection * rayDistance, Color.green);

            if (obstacleHeight > 0 && obstacleHeight <= stepHeight)
            {
                // Step up if the obstacle is within the step height
                Vector3 stepUpPosition = new Vector3(rb.position.x, hit.point.y + 0.1f, rb.position.z);
                rb.position = stepUpPosition; // Snap the Rigidbody to the step height
            }
            else if (obstacleHeight > stepHeight && obstacleHeight <= mantleHeight)
            {
                // Mantle over the obstacle if it's too tall to step up but within mantle height
                StartCoroutine(MantleOver(hit.point));
            }
        }
    }

    private System.Collections.IEnumerator MantleOver(Vector3 mantlePoint)
    {
        isMantling = true;

        // Target position for mantling
        Vector3 mantleTarget = new Vector3(transform.position.x, mantlePoint.y + 1f, transform.position.z);

        // Smoothly move the player to the mantle target position
        while (Vector3.Distance(transform.position, mantleTarget) > 0.1f)
        {
            rb.MovePosition(Vector3.Lerp(transform.position, mantleTarget, Time.deltaTime * mantleSpeed));
            yield return null;
        }

        // Ensure the player is exactly on top of the ledge
        rb.MovePosition(mantleTarget);

        isMantling = false;
    }
}
