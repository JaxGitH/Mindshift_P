using UnityEngine;

[CreateAssetMenu(fileName = "New PhysObj", menuName = "Mindshift/PhysicsObject")]
public class PhysObj : ScriptableObject
{
    [Header("General Properties")]
    public string objName; // Name of the object
    public GameObject prefab; // Prefab reference
    public string behaviorDescription; // Description of the object's behavior

    [Header("Physics Properties")]
    public bool isGrabbed = false; // Tracks if the object is being grabbed
    public bool usesGravity = true; // Does the object obey gravity?
    public float mass = 1.0f; // Mass of the object

    [Header("Attachment Properties")]
    public bool canAttachObjects = false; // Can this object have other objects attached to it?
    public bool isAttached = false; // Is this object currently attached?
    public float floatStrength = 0.0f; // If attached, how much lift does it provide?
}
