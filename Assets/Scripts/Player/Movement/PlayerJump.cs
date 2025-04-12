using UnityEngine;

namespace Player.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerJump : MonoBehaviour
    {
        [Header("Jump Settings")]
        public float jumpForce = 12f;
        public float coyoteTime = 0.15f;
        private float _coyoteTimeCounter;

        [Header("Ground Check")]
        public Transform groundCheck;
        public float groundCheckRadius = 0.2f;
        public LayerMask groundLayer;
        
        [Header("Better Jump Settings")]
        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;

        private Rigidbody2D _rb;
        private bool _isGrounded;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CheckGround();

            if (_isGrounded)
            {
                _coyoteTimeCounter = coyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }

            if (Input.GetButtonDown("Jump") && _coyoteTimeCounter > 0f)
            {
                Jump();
            }
            
            // Better gravity control
            if (_rb.velocity.y < 0)
            {
                _rb.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
            }
            else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                _rb.velocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
            }

        }

        private void Jump()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _coyoteTimeCounter = 0f;
        }

        private void CheckGround()
        {
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        private void OnDrawGizmosSelected()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }
}