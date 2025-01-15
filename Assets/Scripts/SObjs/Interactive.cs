using UnityEngine;

[CreateAssetMenu(fileName = "New Interactive", menuName = "Mindshift/Interactive")]
public class Interactive : ScriptableObject
{
    public string interactiveName;
    public GameObject prefab;
    public string activationMethod;
}
