using UnityEngine;

public class AttachOnCollision : MonoBehaviour
{
    [Header("Attachment Settings")]
    [Tooltip("The type of joint to create for the attachment.")]
    [SerializeField] private JointType jointType = JointType.Fixed;

    [Tooltip("If true, this will shift objects closer to the origin to avoid precision issues.")]
    [SerializeField] private bool shiftToOriginOnAttach = true;

    private void OnCollisionEnter(Collision collision)
    {
        // Ensure the collided object has a Rigidbody
        Rigidbody otherRigidbody = collision.rigidbody;
        if (otherRigidbody == null)
        {
            Debug.LogWarning($"No Rigidbody found on {collision.gameObject.name}. Cannot attach.");
            return;
        }

        // Optional: Shift the objects closer to the origin to avoid precision issues
        if (shiftToOriginOnAttach)
        {
            HandleFloatingOrigin(collision.gameObject);
        }

        // Create the joint based on the selected joint type
        Joint joint = CreateJoint(otherRigidbody);

        // Position the joint at the contact point
        if (joint != null && collision.contacts.Length > 0)
        {
            joint.anchor = transform.InverseTransformPoint(collision.contacts[0].point);
            Debug.Log($"Attached {gameObject.name} to {collision.gameObject.name} at {collision.contacts[0].point}");
        }
    }

    private Joint CreateJoint(Rigidbody otherRigidbody)
    {
        Joint joint = null;

        // Create the appropriate type of joint
        switch (jointType)
        {
            case JointType.Fixed:
                joint = gameObject.AddComponent<FixedJoint>();
                break;
            case JointType.Hinge:
                joint = gameObject.AddComponent<HingeJoint>();
                break;
            case JointType.Spring:
                joint = gameObject.AddComponent<SpringJoint>();
                break;
        }

        if (joint != null)
        {
            joint.connectedBody = otherRigidbody; // Attach the joint to the other Rigidbody
        }

        return joint;
    }

    private void HandleFloatingOrigin(GameObject otherObject)
    {
        Vector3 averagePosition = (transform.position + otherObject.transform.position) / 2;

        // Shift both objects closer to the origin
        Vector3 offset = averagePosition;
        transform.position -= offset;
        otherObject.transform.position -= offset;

        Debug.Log($"Shifted {gameObject.name} and {otherObject.name} closer to the origin to avoid floating-point precision issues.");
    }

    public enum JointType
    {
        Fixed,
        Hinge,
        Spring
    }
}


