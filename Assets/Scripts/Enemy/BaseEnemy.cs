using UnityEngine;

namespace Enemy
{
    public abstract class BaseEnemy : MonoBehaviour, IEnemy
    {
        protected Transform Player;

        public virtual void DetectPlayer(Transform target)
        {
            Player = target;
            Debug.Log($"{gameObject.name} detected player: {Player?.name}");
        }

        public abstract void KillPlayer();
        public abstract void Attack();
    }
}
