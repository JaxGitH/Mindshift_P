using UnityEngine;

namespace Mindshift
{
    public class DragAndDrop : MonoBehaviour
    {
        protected Rigidbody objectRb;
        protected Camera mainCamera;
        protected bool isBeingDragged = false;

        [Header("Dragging Settings")]
        [SerializeField] protected float dragHoldTime = 0f;
        [SerializeField] protected float longPressThreshold = 1.0f;

        protected virtual void Start()
        {
            objectRb = GetComponent<Rigidbody>();
            mainCamera = Camera.main;
        }

        protected virtual void Update()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            HandleMouseInput();
#elif UNITY_ANDROID || UNITY_IOS
            HandleTouchInput();
#endif
        }

        protected virtual void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
                {
                    isBeingDragged = true;
                    objectRb.isKinematic = true;
                    dragHoldTime = 0f;
                }
            }

            if (Input.GetMouseButton(0) && isBeingDragged)
            {
                dragHoldTime += Time.deltaTime;

                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);

                if (dragHoldTime >= longPressThreshold)
                {
                    OnLongPress();
                }
            }

            if (Input.GetMouseButtonUp(0) && isBeingDragged)
            {
                isBeingDragged = false;
                objectRb.isKinematic = false;
            }
        }

        protected virtual void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = mainCamera.ScreenPointToRay(touch.position);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
                        {
                            isBeingDragged = true;
                            objectRb.isKinematic = true;
                            dragHoldTime = 0f;
                        }
                        break;

                    case TouchPhase.Moved:
                        if (isBeingDragged)
                        {
                            dragHoldTime += Time.deltaTime;

                            Vector3 touchPosition = touch.position;
                            touchPosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
                            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
                            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);

                            if (dragHoldTime >= longPressThreshold)
                            {
                                OnLongPress();
                            }
                        }
                        break;

                    case TouchPhase.Ended:
                        if (isBeingDragged)
                        {
                            isBeingDragged = false;
                            objectRb.isKinematic = false;
                        }
                        break;
                }
            }
        }

        protected virtual void OnLongPress()
        {
            Debug.Log($"{name}: Long press detected.");
        }
    }
}
