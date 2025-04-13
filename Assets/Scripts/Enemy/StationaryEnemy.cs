using UnityEngine;

namespace Enemy
{
    public class StationaryEnemy : BaseEnemy
    {   
        [SerializeField] private float killRange = 1.0f;

        private void Update()
        {
            if (Player != null && Vector2.Distance(transform.position, Player.position) < killRange)
            {
                KillPlayer();
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public override void KillPlayer()
        {
            //GameManager.Instance.KillPlayer(transform.position); // you'll implement this
            Debug.Log("Player killed by stationary enemy");
            // Example in an enemy script
            Player.GetComponent<Player.PlayerHealthService>()?.KillPlayer(); // Kill the player
        }

        public override void Attack()
        {
            Debug.Log("attack");
            // play attack anim if needed
        }
    }
}

