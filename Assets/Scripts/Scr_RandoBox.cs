using UnityEngine;

public class Scr_RandoBox : MonoBehaviour
{
    [Header("Spawnable Objects")]
    [Tooltip("Add the prefabs or objects that can spawn randomly.")]
    [SerializeField] private GameObject[] spawnableObjects;

    [Header("Override Settings")]
    [Tooltip("Set this to override random selection and spawn a specific object. Use -1 for random.")]
    [SerializeField] private int overrideIndex = -1;


    private bool hasSpawned = false;

    void Start()
    {
        Collider collider = GetComponent<Collider>();
        if (collider == null || !collider.isTrigger)
        {
            Debug.LogError("The GameObject requires a Collider set as a Trigger for the Scr_RandoBox to work.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has the "Player" tag and if an item has already spawned
        if (hasSpawned) return;

        if (other.CompareTag("Player"))
        {
            SpawnRandomObject();
        }
    }

    private void SpawnRandomObject()
    {
        if (spawnableObjects.Length == 0)
        {
            Debug.LogWarning("No objects in spawnableObjects array. Nothing will spawn.");
            return;
        }

        GameObject objectToSpawn;

        // Check if override is set
        if (overrideIndex >= 0 && overrideIndex < spawnableObjects.Length)
        {
            // Use the overridden index
            objectToSpawn = spawnableObjects[overrideIndex];
            Debug.Log($"Override index set. Spawning: {objectToSpawn.name}");
        }
        else
        {
            // Pick a random object from the array
            int randomIndex = Random.Range(0, spawnableObjects.Length);
            objectToSpawn = spawnableObjects[randomIndex];
            Debug.Log($"Random selection. Spawning: {objectToSpawn.name}");
        }

        // Spawn the selected object at the current position and rotation
        Instantiate(objectToSpawn, transform.position, transform.rotation);

        // Destroy this object after spawning
        Destroy(gameObject);
    }
}
