using UnityEngine;

public class Scr_Killbox : MonoBehaviour
{
    [Header("List of Prefabs to Destroy")]
    [Tooltip("Add the prefabs or objects that should be destroyed when they overlap with this trigger.")]
    public GameObject[] destroyablePrefabs;

    [Tooltip("Should this script destroy objects immediately on entering the trigger?")]
    public bool destroyOnEnter = true;

    [Tooltip("Should this script destroy objects if they remain in the trigger?")]
    public bool destroyOnStay = false;

    [Tooltip("Should this script destroy objects if they exit the trigger?")]
    public bool destroyOnExit = false;

    private void Start()
    {
        // Ensure the object has a trigger collider
        Collider collider = GetComponent<Collider>();
        if (collider == null || !collider.isTrigger)
        {
            Debug.LogError("The GameObject requires a Collider set as a Trigger for the TriggerDestroyer to work.");
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
        // Check if the object matches any prefab in the destroyablePrefabs array
        foreach (GameObject prefab in destroyablePrefabs)
        {
            // Compare the object using its prefab
            if (prefab != null && prefab.name == obj.name.Replace("(Clone)", "").Trim())
            {
                Destroy(obj); // Destroy the object
                Debug.Log($"Destroyed {obj.name} because it matched a prefab in the array.");
                return; // Exit the loop once destroyed
            }
        }

        // If the object is not in the array, it's safe
        Debug.Log($"{obj.name} is safe and will not be destroyed.");
    }
}
