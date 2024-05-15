using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Push = Animator.StringToHash("Push");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Kickflip = Animator.StringToHash("Kickflip");
    private static readonly int Shuvit = Animator.StringToHash("Shuvit");
    private static readonly int GameStarted = Animator.StringToHash("GameStarted");

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
    
    public void PlayKickflipAnimation() 
    {
        _animator.SetTrigger(Kickflip);
    }

    public void PlayShuvitAnimation()
    {
        _animator.SetTrigger(Shuvit);
    }

    public void TransitionToRun()
    {
        _animator.SetBool(GameStarted, true);
    }
}