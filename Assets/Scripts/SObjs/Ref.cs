using UnityEngine;

[CreateAssetMenu(fileName = "soRef", menuName = "Mindshift/ReferenceObject")]
public class soRef : ScriptableObject
{
    [Header("Physics Objects")]
    public PhysObj[] physicsObjects;

    [Header("Hazards")]
    public Hazard[] hazards;

    [Header("Interactives")]
    public Interactive[] interactives;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject cameraPrefab;
}
