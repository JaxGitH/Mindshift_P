using UnityEngine;

namespace Mindshift
{
    public class ObstacleInteractionManager : MonoBehaviour
    {
        [Header("Interaction Settings")]
        [SerializeField] private LayerMask obstacleLayer; // Only interact with obstacles
        [SerializeField] private float dragSpeed = 10f;

        private Camera mainCamera;
        private GameObject selectedObstacle;
        private Rigidbody selectedRigidbody;
        private Vector3 offset;

        void Start()
        {
            mainCamera = Camera.main;
        }

        void Update()
        {
            HandleInput();
        }

        void HandleInput()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            HandleMouseInput();
#elif UNITY_ANDROID || UNITY_IOS
            HandleTouchInput();
#endif
        }

        /// <summary>
        /// Handles Mouse Interaction (Windows).
        /// </summary>
        void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0)) // Left Click
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, obstacleLayer))
                {
                    selectedObstacle = hit.collider.gameObject;
                    selectedRigidbody = selectedObstacle.GetComponent<Rigidbody>();
                    selectedRigidbody.isKinematic = true;
                    offset = selectedObstacle.transform.position - GetMouseWorldPosition();
                }
            }

            if (Input.GetMouseButton(0) && selectedObstacle != null) // Drag
            {
                Vector3 targetPosition = GetMouseWorldPosition() + offset;
                selectedObstacle.transform.position = Vector3.Lerp(
                    selectedObstacle.transform.position,
                    targetPosition,
                    Time.deltaTime * dragSpeed
                );
            }

            if (Input.GetMouseButtonUp(0) && selectedObstacle != null) // Release
            {
                selectedRigidbody.isKinematic = false;
                selectedObstacle = null;
            }
        }

        /// <summary>
        /// Handles Touch Interaction (Mobile).
        /// </summary>
        void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = mainCamera.ScreenPointToRay(touch.position);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, obstacleLayer))
                        {
                            selectedObstacle = hit.collider.gameObject;
                            offset = selectedObstacle.transform.position - GetTouchWorldPosition(touch);
                        }
                        break;

                    case TouchPhase.Moved:
                        if (selectedObstacle != null)
                        {
                            Vector3 targetPosition = GetTouchWorldPosition(touch) + offset;
                            selectedObstacle.transform.position = Vector3.Lerp(
                                selectedObstacle.transform.position,
                                targetPosition,
                                Time.deltaTime * dragSpeed
                            );
                        }
                        break;

                    case TouchPhase.Ended:
                        selectedObstacle = null;
                        break;
                }
            }
        }

        /// <summary>
        /// Converts mouse position to world position.
        /// </summary>
        Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.nearClipPlane + 5f; // Distance from camera
            return mainCamera.ScreenToWorldPoint(mousePosition);
        }

        /// <summary>
        /// Converts touch position to world position.
        /// </summary>
        Vector3 GetTouchWorldPosition(Touch touch)
        {
            Vector3 touchPosition = touch.position;
            touchPosition.z = mainCamera.nearClipPlane + 5f; // Distance from camera
            return mainCamera.ScreenToWorldPoint(touchPosition);
        }
    }
}
