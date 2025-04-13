using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerHealthService : MonoBehaviour
    {
        [Header("Health Settings")]
        public int maxHealth = 3;
        private int _currentHealth;

        [Header("Death Settings")]
        public float deathDelay = 1f;
        public GameObject deathEffect; // Optional death VFX prefab

        [Header("UI Events")]
        public UnityEvent<int> onHealthChanged; // Event to update health in UI
        public UnityEvent onPlayerDied; // Event to notify UI of player death

        private void Start()
        {
            _currentHealth = maxHealth;
            onHealthChanged?.Invoke(_currentHealth); // Initialize UI with max health
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            Debug.Log($"Player took {damage} damage. HP left: {_currentHealth}");

            onHealthChanged?.Invoke(_currentHealth); // Notify UI of health change

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void KillPlayer()
        {
            Debug.Log("Player killed instantly!");
            _currentHealth = 0;
            onHealthChanged?.Invoke(_currentHealth); // Notify UI of health change
            Die();
        }

        private void Die()
        {
            Debug.Log("Player died!");

            if (deathEffect)
                Instantiate(deathEffect, transform.position, Quaternion.identity);

            onPlayerDied?.Invoke(); // Notify UI of player death

            // Optionally disable player controls here
            Invoke(nameof(RestartLevel), deathDelay);
        }

        private void RestartLevel()
        {
            // Restart the level or handle game over logic
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}