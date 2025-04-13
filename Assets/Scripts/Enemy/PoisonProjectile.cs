using UnityEngine;

namespace Enemy
{
    public class PoisonProjectile : MonoBehaviour
    {
        [SerializeField] private int damage = 1; // Damage dealt to the player

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if the projectile hits the player
            if (collision.CompareTag("Player"))
            {
                // Deal damage to the player
                collision.GetComponent<Player.PlayerHealthService>()?.TakeDamage(damage);

                // Destroy the projectile
                Destroy(gameObject);
            }
            else if (collision.CompareTag($"Ground")) // Optional: Destroy on hitting the ground
            {
                Destroy(gameObject);
            }
        }
    }
}