using UnityEngine;

public class ContinuousForceApplier : MonoBehaviour
{
    [Header("Force Settings")]
    [Tooltip("The direction of the force to apply.")]
    [SerializeField] private Vector3 forceDirection = Vector3.forward;

    [Tooltip("The magnitude of the force to apply.")]
    [SerializeField] private float forceMagnitude = 10f;

    [Tooltip("Should the force be applied relative to the object's rotation?")]
    [SerializeField] private bool relativeToRotation = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is missing. Please add a Rigidbody to this GameObject.");
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            ApplyContinuousForce();
        }
    }

    private void ApplyContinuousForce()
    {
        // Calculate the force vector
        Vector3 force = forceDirection.normalized * forceMagnitude;

        // Apply force, optionally relative to the object's rotation
        if (relativeToRotation)
        {
            rb.AddForce(transform.TransformDirection(force));
        }
        else
        {
            rb.AddForce(force);
        }
    }
}

