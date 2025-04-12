using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int AirSpeedY = Animator.StringToHash("AirSpeedY");
    private static readonly int AnimState = Animator.StringToHash("AnimState");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Roll = Animator.StringToHash("Roll");
    private static readonly int Attack1 = Animator.StringToHash("Attack1");
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetGrounded(bool grounded)
    {
        _animator.SetBool(Grounded, grounded);
    }

    public void SetAirSpeed(float ySpeed)
    {
        _animator.SetFloat(AirSpeedY, ySpeed);
    }

    public void SetRun(bool isRunning)
    {
        _animator.SetInteger(AnimState, isRunning ? 1 : 0); // 1 = run, 0 = idle
    }

    public void PlayJump()
    {
        _animator.SetTrigger(Jump);
    }

    public void PlayRoll()
    {
        _animator.SetTrigger(Roll);
    }

    public void PlayAttack()
    {
        _animator.SetTrigger(Attack1);
    }
}