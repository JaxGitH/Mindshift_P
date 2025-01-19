using UnityEngine;

namespace Mindshift
{
    public class Block : MonoBehaviour
    {
        [Header("Block Settings")]
        public PhysObj blockData; // ScriptableObject containing physics properties
        private Rigidbody rb;
        private bool isFloating = false; // Tracks if the block is floating
        [SerializeField] private LayerMask Interactable;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("Block is missing a Rigidbody component!");
            }
        }

        public void StartFloating()
        {
            if (!isFloating)
            {
                Debug.Log("Block is now floating!");
                isFloating = true;

                // Disable gravity and allow upward movement
                rb.useGravity = false;
            }
        }

        public void MoveWithBalloon(Vector3 floatDirection)
        {
            if (isFloating)
            {
                // Move the block upward along with the balloon
                rb.MovePosition(rb.position + floatDirection);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isFloating)
            {
                Debug.Log("Floating block collided with something.");
                // Additional collision behavior can be added here
            }
        }
    }
}
