using UnityEngine;

[CreateAssetMenu(fileName = "New Hazard", menuName = "Mindshift/Hazard")]
public class Hazard : ScriptableObject
{
    public string hazardName;
    public GameObject prefab;
    public string interactionDescription;
}
