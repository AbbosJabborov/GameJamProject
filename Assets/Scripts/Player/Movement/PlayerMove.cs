using UnityEngine;

namespace Player.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMove : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Sliding = Animator.StringToHash("Sliding");

        [Header("Movement")]
        public float moveSpeed = 5f;
        public float runMultiplier = 1.5f;
        public float acceleration = 10f;

        [Header("Slide")]
        public float slideSpeed = 10f;
        public float slideDuration = 0.4f;
        public float slideCooldown = 1.2f;
        private float _slideTimer;
        private bool _isSliding;

        [Header("Components")]
        private Rigidbody2D _rb;
        private Animator _anim;
        private SpriteRenderer _sr;

        private float _moveInput;
        private bool _isRunning;
        private bool _canSlide = true;
        

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _moveInput = Input.GetAxisRaw("Horizontal");

            // Running toggle (hold Left Shift)
            _isRunning = Input.GetKey(KeyCode.LeftShift);

            // Slide input
            if(Input.GetKeyDown(KeyCode.LeftControl) && _canSlide && _moveInput != 0 && !_isSliding)
            {
                StartCoroutine(Slide());
            }

            // Flip sprite
            if(_moveInput != 0 && !_isSliding)
                _sr.flipX = _moveInput < 0;

            // Animation triggers (if you have them)
            if(_anim != null)
            {
                _anim.SetFloat(Speed, Mathf.Abs(_rb.velocity.x));
                _anim.SetBool(Sliding, _isSliding);
            }

        }

        private void FixedUpdate()
        {
            if (!_isSliding)
            {
                float targetSpeed = moveSpeed * (_isRunning ? runMultiplier : 1f) * _moveInput;
                float speedDiff = targetSpeed - _rb.velocity.x;
                float movement = speedDiff * acceleration;

                _rb.AddForce(Vector2.right * movement);
            }
        }

        private System.Collections.IEnumerator Slide()
        {
            _isSliding = true;
            _canSlide = false;

            float originalDrag = _rb.drag;
            _rb.drag = 0f;

            float dir = Mathf.Sign(_moveInput);
            _rb.velocity = new Vector2(dir * slideSpeed, _rb.velocity.y);

            yield return new WaitForSeconds(slideDuration);

            _isSliding = false;
            _rb.drag = originalDrag;

            yield return new WaitForSeconds(slideCooldown);
            _canSlide = true;
        }
    }
}
