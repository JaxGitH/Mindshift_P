using UnityEngine;

public class RespawnOnDestroy : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private GameObject inactiveClone; // Reference to the inactive clone

    void Start()
    {
        // Create a clone of this object
        inactiveClone = Instantiate(gameObject, transform.position, transform.rotation);

        // Disable the clone to make it inactive
        inactiveClone.SetActive(false);

        // Destroy the SelfCloner script on the clone to prevent recursive behavior
        //Destroy(inactiveClone.GetComponent<RespawnOnDestroy>());
    }

    void OnDestroy()
    {
        // Activate the inactive clone
        if (inactiveClone != null)
        {
            inactiveClone.SetActive(true);
            Debug.Log($"{inactiveClone.name} has been activated!");
        }
    }
}
