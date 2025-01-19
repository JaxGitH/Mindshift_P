using UnityEngine;

namespace Mindshift
{
    public class Balloon : MonoBehaviour
    {
        [Header("Balloon Settings")]
        public PhysObj balloonData; // ScriptableObject containing physics properties
        public float floatSpeed = 2f; // Speed at which the balloon floats upward
        private bool isAttached = false; // Tracks if the balloon is attached to a block
        private Rigidbody rb;
        private Block attachedBlock; // Reference to the attached block
        [SerializeField] private LayerMask Interactable;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("Balloon is missing a Rigidbody component!");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Block>(out Block block) && !isAttached)
            {
                AttachToBlock(block);
            }
        }

        private void AttachToBlock(Block block)
        {
            Debug.Log($"Balloon attached to block: {block.name}");
            isAttached = true;
            attachedBlock = block;

            // Notify the block that it is now floating
            block.StartFloating();
        }

        private void FixedUpdate()
        {
            if (isAttached && attachedBlock != null)
            {
                // Move both the balloon and the block upward
                Vector3 floatDirection = Vector3.up * floatSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + floatDirection);
                attachedBlock.MoveWithBalloon(floatDirection);
            }
        }
    }
}
