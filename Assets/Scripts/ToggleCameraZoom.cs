using UnityEngine;
using UnityEngine.UI;

public class ToggleCameraZoom : MonoBehaviour
{
    [SerializeField] private Camera targetCamera; // Reference to your Camera
    [SerializeField] private float zoomSpeed = 5f;
    private float minFOV; // Zoomed in
    [SerializeField] private float maxFOV = 90f; // Zoomed out
    private bool isZoomedIn = true;

    private void Start()
    {
        minFOV = targetCamera.fieldOfView;
    }

    public void ToggleZoom()
    {
        isZoomedIn = !isZoomedIn;
        float targetFOV = isZoomedIn ? minFOV : maxFOV;
        StopAllCoroutines();
        StartCoroutine(SmoothZoom(targetFOV));
    }

    private System.Collections.IEnumerator SmoothZoom(float targetFOV)
    {
        if (targetCamera == null) yield break;

        float startFOV = targetCamera.fieldOfView;

        while (!Mathf.Approximately(targetCamera.fieldOfView, targetFOV))
        {
            targetCamera.fieldOfView = Mathf.MoveTowards(targetCamera.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
