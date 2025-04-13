using UnityEngine;

namespace Enemy
{
    public class PoisonProjectile : MonoBehaviour
    {
        [SerializeField] private int damage = 1; // Damage dealt to the player
        [SerializeField] private LayerMask groundLayer; // LayerMask for the ground

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
            else if (IsGround(collision)) // Check if the collision is with the ground
            {
                Destroy(gameObject);
            }
        }

        private bool IsGround(Collider2D collision)
        {
            // Check if the collided object is in the ground layer
            return ((1 << collision.gameObject.layer) & groundLayer) != 0;
        }
    }
}