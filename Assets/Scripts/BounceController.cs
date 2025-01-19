using UnityEngine;

public class BounceController : MonoBehaviour
{
    private Rigidbody rb;
    private bool restrictMovement = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("BounceController requires a Rigidbody!");
        }
    }

    private void FixedUpdate()
    {
        // If movement restriction is enabled, lock horizontal motion
        if (restrictMovement && rb != null)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0); // Keep only Y-axis velocity
        }
    }

    // Call this method to start restricting horizontal movement
    public void StartRestrictingMovement()
    {
        restrictMovement = true;
    }

    // Call this method to stop restricting movement (optional)
    public void StopRestrictingMovement()
    {
        restrictMovement = false;
    }
}
