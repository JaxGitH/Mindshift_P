using UnityEngine;

public class Scr_Killbox : MonoBehaviour
{
    [Header("List of Tags to Destroy")]
    [Tooltip("Add the tags of objects that should be destroyed when they overlap with this trigger.")]
    [SerializeField] private string[] destroyableTags;

    [Header("Trigger Options")]
    [Tooltip("Should this script destroy objects immediately on entering the trigger?")]
    [SerializeField] private bool destroyOnEnter = true;

    [Tooltip("Should this script destroy objects if they remain in the trigger?")]
    [SerializeField] private bool destroyOnStay = false;

    [Tooltip("Should this script destroy objects if they exit the trigger?")]
    [SerializeField] private bool destroyOnExit = false;

    private void Start()
    {
        // Ensure the object has a trigger collider
        Collider collider = GetComponent<Collider>();
        if (collider == null || !collider.isTrigger)
        {
            Debug.LogError("The GameObject requires a Collider set as a Trigger for the Killbox to work.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destroyOnEnter)
        {
            CheckAndDestroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (destroyOnStay)
        {
            CheckAndDestroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (destroyOnExit)
        {
            CheckAndDestroy(other.gameObject);
        }
    }

    private void CheckAndDestroy(GameObject obj)
    {
        // Check if the object's tag is in the list of destroyable tags
        foreach (string tag in destroyableTags)
        {
            if (obj.CompareTag(tag))
            {
                Destroy(obj);
                Debug.Log($"Destroyed {obj.name} because its tag '{obj.tag}' is in the destroyable tags list.");
                return; // Exit the loop and method once destroyed
            }
        }

        // If the object's tag is not in the list
        Debug.Log($"{obj.name} is safe and will not be destroyed. Its tag is '{obj.tag}'.");
    }
}
