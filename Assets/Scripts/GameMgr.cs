using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public soRef gameData; // Assign your soRef ScriptableObject in the inspector

    void Start()
    {
        // Example: Spawn all physics objects
        foreach (var physObj in gameData.physicsObjects)
        {
            Instantiate(physObj.prefab, Vector3.zero, Quaternion.identity);
        }
    }
}
