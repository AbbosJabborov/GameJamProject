using UnityEngine;

namespace Enemy
{
    public class EnemyDetectionAttack : MonoBehaviour
    {
        public float detectionRadius = 5f;

        private EnemyMovement _movementScript;
        private Transform _player;
        private BaseEnemy _baseEnemy;

        void Start()
        {
            _movementScript = GetComponent<EnemyMovement>();
            _baseEnemy = GetComponent<BaseEnemy>();
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
            {
                _player = playerObj.transform;
            }
        }

        void Update()
        {
            if (_player == null) return;

            float distance = Vector2.Distance(transform.position, _player.position);

            if (distance <= detectionRadius)
            {
                // Notify BaseEnemy that the player has been detected
                _baseEnemy?.DetectPlayer(_player);

                // Enable chasing behavior
                if (_movementScript != null)
                {
                    _movementScript.shouldChase = true;
                    _movementScript.target = _player;
                }
            }
            else
            {
                // Stop chasing if the player is out of range
                if (_movementScript != null)
                {
                    _movementScript.shouldChase = false;
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}