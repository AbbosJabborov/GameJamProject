using UnityEngine;

namespace Enemy
{
    public class PoisonShooterEnemy : BaseEnemy
    {
        [SerializeField] private GameObject poisonProjectile;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireInterval = 3f;
        [SerializeField] private float projectileSpeed = 5f; // Speed of the projectile
        private float _fireTimer;

        private void Update()
        {
            _fireTimer += Time.deltaTime;

            if (Player != null && Vector2.Distance(transform.position, Player.position) < 6f)
            {
                if (_fireTimer >= fireInterval)
                {
                    Attack();
                    _fireTimer = 0;
                }
            }
        }

        public override void KillPlayer()
        {
            Debug.Log("Player killed");
        }

        public override void Attack()
        {
            // Instantiate the projectile
            GameObject projectile = Instantiate(poisonProjectile, firePoint.position, Quaternion.identity);

            // Get the Rigidbody2D of the projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Calculate the direction to the player
                Vector2 direction = (Player.position - firePoint.position).normalized;

                // Apply velocity to the projectile
                rb.velocity = direction * projectileSpeed;
            }

            Debug.Log("Poison projectile fired");
        }
    }
}