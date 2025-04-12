using UnityEngine;

namespace Enemy
{
    public class EnemyStats : MonoBehaviour
    {
        public int maxHealth = 3;
        private int _currentHealth;

        public GameObject deathEffect; // Optional death VFX prefab
        public float deathDelay = 0.5f; // Time before destroying object

        void Start()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            Debug.Log($"{gameObject.name} took {damage} damage. HP left: {_currentHealth}");

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            Debug.Log($"{gameObject.name} died.");

            if (deathEffect)
                Instantiate(deathEffect, transform.position, Quaternion.identity);

            // Optionally trigger animation here
            Destroy(gameObject, deathDelay);
        }
    }
}