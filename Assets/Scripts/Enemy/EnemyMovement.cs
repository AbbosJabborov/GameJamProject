using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        [HideInInspector] public Transform target;
        [HideInInspector] public bool shouldChase;

        public float moveSpeed = 2f;

        private Rigidbody2D _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (shouldChase && target != null)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                _rb.velocity = new Vector2(direction.x * moveSpeed, _rb.velocity.y);

                // Flip sprite
                if (direction.x > 0)
                    transform.localScale = new Vector3(1, 1, 1);
                else if (direction.x < 0)
                    transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
            }
        }
    }
}