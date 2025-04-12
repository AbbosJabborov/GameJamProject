using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public KeyCode attackKey = KeyCode.J;
        public float attackRange = 1.2f;
        public int attackDamage = 1;
        public float attackCooldown = 0.5f;
        public Transform attackPoint;
        public LayerMask enemyLayer;

        private float _nextAttackTime = 0f;

        void Update()
        {
            if (Time.time >= _nextAttackTime && Input.GetKeyDown(attackKey))
            {
                Attack();
                _nextAttackTime = Time.time + attackCooldown;
            }
        }

        void Attack()
        {
            // Play animation here if any
            Debug.Log("Player attacks!");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                // Assuming enemy has EnemyStats script
                enemy.GetComponent<EnemyStats>()?.TakeDamage(attackDamage);
            }
        }

        void OnDrawGizmosSelected()
        {
            if (attackPoint == null) return;
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}