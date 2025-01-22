using UnityEngine;

namespace Mindshift
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody playerRb;
        private BoxCollider playerCollider;

        [Header("Movement Settings")]
        [SerializeField] private float speed = 7f;
        [SerializeField] private float mantleHeight = 2.0f;
        [SerializeField] private float mantleDistance = 1.5f;
        [SerializeField] private float mantleSpeed = 5f;
        [SerializeField] private float checkMantleHeight = 0.2f;

        [Header("Joystick Settings")]
        [SerializeField] public VJoystick joystick;

        [Header("Layer Settings")]
        [SerializeField] private LayerMask Interactable;

        private bool isMantling = false;
        private Vector3 mantleTarget;

        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            playerCollider = GetComponent<BoxCollider>();

            if (playerRb == null || playerCollider == null)
            {
                Debug.LogError("PlayerController requires a Rigidbody and BoxCollider!");
                return;
            }

            playerRb.freezeRotation = true;
        }

        void Update()
        {
            if (!isMantling)
            {
                HandleMovement();
                CheckForMantle();
            }
            else
            {
                PerformMantle();
            }
        }

        void HandleMovement()
        {
            float joystickVertical = joystick.Vertical;
            float keyboardVertical = Input.GetAxis("Vertical");

            float finalVertical = joystickVertical + keyboardVertical;
            Vector3 movement = new Vector3(0f, 0f, finalVertical);
            transform.Translate(movement.normalized * speed * Time.deltaTime);
        }

        void CheckForMantle()
        {
            RaycastHit hit;

            // Dynamic ray origin based on the BoxCollider's dimensions
            float rayStartHeight = playerCollider.bounds.center.y + (playerCollider.size.y / 2) * checkMantleHeight;
            Vector3 rayOrigin = new Vector3(transform.position.x, rayStartHeight, transform.position.z);
            Vector3 rayDirection = transform.forward;

            Debug.DrawRay(rayOrigin, rayDirection * mantleDistance, Color.green);

            if (Physics.Raycast(rayOrigin, rayDirection, out hit, mantleDistance, Interactable))
            {
                Mantleable mantleable = hit.collider.GetComponent<Mantleable>();
                if (mantleable != null)
                {
                    float objectHeight = hit.point.y - transform.position.y;

                    if (objectHeight > 0 && objectHeight <= mantleHeight)
                    {
                        StartMantle(hit.collider.bounds.center + Vector3.up * mantleable.GetMantleOffset());
                        mantleable.OnMantle();
                    }
                }
            }
        }

        void StartMantle(Vector3 targetPosition)
        {
            isMantling = true;
            mantleTarget = targetPosition;

            playerRb.isKinematic = true;

            // Disable the BoxCollider to prevent physics interactions
            playerCollider.enabled = false;
        }

        void PerformMantle()
        {
            transform.position = Vector3.MoveTowards(transform.position, mantleTarget, mantleSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, mantleTarget) < 0.1f)
            {
                FinishMantle();
            }
        }

        void FinishMantle()
        {
            isMantling = false;
            playerRb.isKinematic = false;

            // Re-enable the BoxCollider after mantling
            playerCollider.enabled = true;
        }
    }
}
