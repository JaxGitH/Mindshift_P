using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class LScr_InvisibleInGame : MonoBehaviour
{
    private Renderer rend;
    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

}
