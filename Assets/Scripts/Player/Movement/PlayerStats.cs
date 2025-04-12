using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player.Movement
{
    public class PlayerStats : MonoBehaviour
    {
        public int maxHealth = 5;
        private int _currentHealth;

        public float deathDelay = 1f;
        public GameObject deathEffect; // Optional

        void Start()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            Debug.Log($"Player took {damage} damage. HP left: {_currentHealth}");

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            Debug.Log("Player died!");

            if (deathEffect)
                Instantiate(deathEffect, transform.position, Quaternion.identity);

          
            // GetComponent<PlayerMove>()?.enabled = false;
            // GetComponent<PlayerJump>()?.enabled = false;
            // GetComponent<PlayerAttack>()?.enabled = false;

            Invoke(nameof(RestartLevel), deathDelay);
        }

        void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}