using UnityEngine;

namespace Enemy
{
    public interface IEnemy
    {
        void DetectPlayer(Transform player);
        void Attack();
        void KillPlayer();
    }
}

