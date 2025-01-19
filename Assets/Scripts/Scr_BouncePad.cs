using UnityEngine;

public class Scr_BouncePad : MonoBehaviour
{
    [Header("Bounce Settings")]
    [Tooltip("Force applied to objects when they touch this bouncy surface.")]
    public float bounceForce = 10f;

    [Tooltip("Optional: Only apply the bounce to objects with this tag. Leave empty to apply to all objects.")]
    public string targetTag = "";

    [Tooltip("If enabled, objects will only move up and down while bouncing.")]
    public bool restrictToVerticalMovement = true;

    [Tooltip("Enable this if you want the bounce pad to react when forces are applied to it.")]
    public bool allowPadMovement = true;

    private Rigidbody bouncePadRigidbody;

    private void Start()
    {
        // Get the Rigidbody of the bounce pad itself
        bouncePadRigidbody = GetComponent<Rigidbody>();

        // Ensure the Rigidbody exists
        if (bouncePadRigidbody == null)
        {
            Debug.LogError("The bounce pad must have a Rigidbody component!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Get the Rigidbody of the colliding object
        Rigidbody collidingRigidbody = collision.rigidbody;

        // Ignore if there's no Rigidbody on the other object or the object is the bounce pad itself
        if (collidingRigidbody == null || collidingRigidbody == bouncePadRigidbody)
        {
            return;
        }

        // If a target tag is specified, ensure the object matches
        if (!string.IsNullOrEmpty(targetTag) && !collision.gameObject.CompareTag(targetTag))
        {
            return;
        }

        // Apply an upward bounce force to the colliding object
        Vector3 bounceDirection = Vector3.up; // Always bounce upward
        collidingRigidbody.linearVelocity = new Vector3(0, 0, 0); // Reset velocity before applying bounce
        collidingRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);

        // Optionally allow the bounce pad itself to react to the collision
        if (allowPadMovement)
        {
            // Apply an equal and opposite reaction force to the bounce pad
            Vector3 reactionForce = -bounceDirection * bounceForce * 0.5f; // Dampened reaction
            bouncePadRigidbody.AddForce(reactionForce, ForceMode.Impulse);
        }

        // Optional: Start restricting horizontal movement for the bouncing object
        if (restrictToVerticalMovement)
        {
            // Start the restriction logic (enable horizontal locking while bouncing)
            BounceController bounceController = collision.gameObject.GetComponent<BounceController>();
            if (bounceController == null)
            {
                // Add the BounceController script to the colliding object if it doesn't already exist
                bounceController = collision.gameObject.AddComponent<BounceController>();
            }
            bounceController.StartRestrictingMovement();
        }
    }
}
