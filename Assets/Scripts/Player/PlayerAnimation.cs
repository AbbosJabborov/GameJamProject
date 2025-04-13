using UnityEngine;
using Player.Movement;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Components")]
    private Animator _animator;
    private Rigidbody2D _rb;
    private PlayerMove _playerMove;
    private PlayerJump _playerJump;

    [Header("Animation Parameters")]
    [SerializeField] private float walkAnimationSpeed = 0.5f;
    [SerializeField] private float runAnimationSpeed = 1f;

    // Animation parameter names (existing and new)
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Sliding = Animator.StringToHash("Sliding");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int AnimationSpeed = Animator.StringToHash("AnimationSpeed");
    private static readonly int Land = Animator.StringToHash("Land");

    private bool _wasGrounded;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _playerMove = GetComponent<PlayerMove>();
        _playerJump = GetComponent<PlayerJump>();
    }

    private void Update()
    {
        UpdateAnimationParameters();
        
        // Handle walk/run animation speed
        if (Mathf.Abs(_rb.velocity.x) > 0.1f)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            _animator.SetFloat(AnimationSpeed, isRunning ? runAnimationSpeed : walkAnimationSpeed);
        }
        else
        {
            _animator.SetFloat(AnimationSpeed, 1f);
        }

        // Check for attack input (adjust button as needed)
        if (Input.GetButtonDown("Fire1"))
        {
            TriggerAttack();
        }

        // Handle jump animation trigger
        bool isGroundedNow = CheckIfGrounded();
        if (!_wasGrounded && isGroundedNow)
        {
            // Just landed
            _animator.SetTrigger(Land);
        }
        else if (_wasGrounded && !isGroundedNow && _rb.velocity.y > 0)
        {
            // Just jumped
            _animator.SetTrigger(Jump);
        }
        _wasGrounded = isGroundedNow;
    } 

    private void UpdateAnimationParameters()
    {
        // Update movement parameters (Speed is already set in PlayerMove)
        _animator.SetFloat(VerticalVelocity, _rb.velocity.y);
        
        // Update state parameters
        _animator.SetBool(IsGrounded, CheckIfGrounded());
        
        // Sliding is already handled in PlayerMove
    }

    private bool CheckIfGrounded()
    {
        // Use reflection field to get private field from PlayerJump
        // Or, better approach - create a public property in PlayerJump
        if (_playerJump != null)
        {
            // This uses the ground check from the PlayerJump script
            return Physics2D.OverlapCircle(
                _playerJump.groundCheck.position, 
                _playerJump.groundCheckRadius, 
                _playerJump.groundLayer);
        }
        return false;
    }

    // Public methods to trigger animations from other scripts
    private void TriggerAttack()
    {
        _animator.SetTrigger(Attack);
    }

    public void TriggerJump()
    {
        _animator.SetTrigger(Jump);
    }
}