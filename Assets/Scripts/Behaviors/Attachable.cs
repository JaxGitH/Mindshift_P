using UnityEngine;

public class Attachable : MonoBehaviour
{
    [Header("Attachment Settings")]
    [SerializeField] private bool canBeAttached = true; // Can the object be attached to a Balloon?
    [SerializeField] private float weight = 1.0f;       // The object's weight for floating logic
    [SerializeField] private float liftOffset = 1.0f;  // Offset for positioning the balloon during attachment

    /// <summary>
    /// Determines if the object can be attached to a Balloon.
    /// </summary>
    public bool IsAttachable()
    {
        return canBeAttached;
    }

    /// <summary>
    /// Gets the object's weight, which affects how it floats.
    /// </summary>
    public float GetWeight()
    {
        return weight;
    }

    /// <summary>
    /// Gets the lift offset for this object.
    /// </summary>
    public float GetLiftOffset()
    {
        return liftOffset;
    }

    /// <summary>
    /// (Optional) Handle any custom logic when the object is attached to a Balloon.
    /// </summary>
    public virtual void OnAttach()
    {
        Debug.Log($"{gameObject.name} has been attached to a Balloon.");
    }

    /// <summary>
    /// (Optional) Handle any custom logic when the object is detached from a Balloon.
    /// </summary>
    public virtual void OnDetach()
    {
        Debug.Log($"{gameObject.name} has been detached from a Balloon.");
    }
}
