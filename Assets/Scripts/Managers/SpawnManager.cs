using UnityEngine;

namespace Mindshift
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject obstaclePrefab;
        public Transform player; // Reference to the player's Transform
        [SerializeField] private Vector3 spawnOffset = new Vector3(0, 0, 0); // Distance in front of the player
        [SerializeField] private float startDelay = 2f;
        [SerializeField] private float repeatRate = 2f;
        [SerializeField] private GameObject[] physicsObjects; // Array of physics prefabs


        void Start()
        {
            if (player == null)
            {
                player = GameObject.FindWithTag("Player").transform;
            }

            InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        }

        void SpawnObstacle()
        {
            if (player != null)
            {
                // Spawn the obstacle at a fixed offset in front of the player's current position
                Vector3 spawnPosition = player.position + spawnOffset;
                Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);
            }
        }
        void SpawnPhysicsObject()
        {
            int index = Random.Range(0, physicsObjects.Length);
            Vector3 spawnPosition = player.position + spawnOffset;
            Instantiate(physicsObjects[index], spawnPosition, Quaternion.identity);
        }
    }
}
