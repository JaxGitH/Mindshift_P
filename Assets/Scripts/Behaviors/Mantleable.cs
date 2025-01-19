using UnityEngine;

public class Mantleable : MonoBehaviour
{
    [Header("Mantle Settings")]
    [SerializeField] private float maxMantleHeight = 2.0f; // Maximum height the player can mantle
    [SerializeField] private float mantleOffset = 1.0f;    // Offset for smoother placement during mantling

    /// <summary>
    /// Gets the maximum mantle height for this object.
    /// </summary>
    public float GetMaxMantleHeight()
    {
        return maxMantleHeight;
    }

    /// <summary>
    /// Gets the mantle offset to adjust the player's final position.
    /// </summary>
    public float GetMantleOffset()
    {
        return mantleOffset;
    }

    /// <summary>
    /// (Optional) Handle any custom logic when the object is mantled.
    /// </summary>
    public virtual void OnMantle()
    {
        Debug.Log($"{gameObject.name} has been mantled.");
    }
}
