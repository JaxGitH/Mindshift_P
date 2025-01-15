using UnityEngine;

public class LScr_Obstacle : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color selectedColor = Color.yellow;

    private Renderer obstacleRenderer;

    void Start()
    {
        obstacleRenderer = GetComponent<Renderer>();
        SetDefaultColor();
    }

    public void OnSelect()
    {
        if (obstacleRenderer != null)
        {
            obstacleRenderer.material.color = selectedColor;
        }
    }

    public void OnDeselect()
    {
        if (obstacleRenderer != null)
        {
            obstacleRenderer.material.color = defaultColor;
        }
    }

    private void SetDefaultColor()
    {
        if (obstacleRenderer != null)
        {
            obstacleRenderer.material.color = defaultColor;
        }
    }
}
