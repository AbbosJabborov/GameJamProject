using Player.Movement;
using UnityEngine;

namespace Enemy
{
    public class EnemyDetectionAttack : MonoBehaviour
    {
        public float detectionRadius = 5f;
        public float attackRadius = 1.5f;
        public int attackDamage = 1;
        public float attackCooldown = 1f;

        private float _nextAttackTime = 0f;
        private EnemyMovement _movementScript;
        private Transform _player;
        private PlayerStats _playerStats;

        void Start()
        {
            _movementScript = GetComponent<EnemyMovement>();
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
            {
                _player = playerObj.transform;
                _playerStats = playerObj.GetComponent<PlayerStats>();
            }
        }

        void Update()
        {
            if (_player == null || _playerStats == null) return;

            float distance = Vector2.Distance(transform.position, _player.position);

            if (distance <= detectionRadius)
            {
                _movementScript.shouldChase = distance > attackRadius;
                _movementScript.target = _player;

                if (distance <= attackRadius && Time.time >= _nextAttackTime)
                {
                    _nextAttackTime = Time.time + attackCooldown;
                    AttackPlayer();
                }
            }
            else
            {
                _movementScript.shouldChase = false;
            }
        }

        void AttackPlayer()
        {
            Debug.Log("Enemy attacks!");
            _playerStats.TakeDamage(attackDamage);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}