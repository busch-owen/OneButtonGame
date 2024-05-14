using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Push = Animator.StringToHash("Push");
    private static readonly int Land = Animator.StringToHash("Land");
    private static readonly int Grounded = Animator.StringToHash("Grounded");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    //Play jumping animation when you get the jump input on the player controller
    public void PlayJumpAnimation()
    {
        _animator.SetTrigger(Jump);
    }

    //Play the pushing animation when you gain speed
    public void PlayPushAnimation()
    {
        _animator.SetTrigger(Push);
    }

    //Change the ground state so that the game knows when to play the landing animation
    public void ChangeGroundState(bool state)
    {
        _animator.SetBool(Grounded, state);
    }
}