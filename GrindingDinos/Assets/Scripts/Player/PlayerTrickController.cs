using UnityEngine;

public class PlayerTrickController : MonoBehaviour
{
    [Header("Trick Stats")]
    [SerializeField] private float trickCooldownTime;
    [SerializeField] float kickflipScoreValue, shuvitScoreValue;
    [field: SerializeField] public float ShuvitHoldTime { get; private set; }
    
    //Other trick variables
    private PlayerAnimationController _animController;
    private bool _canDoTrick = true;
    
    private void Awake()
    {
        _animController = GetComponentInChildren<PlayerAnimationController>();
    }

    //Logic for kickflip tricks
    public void Kickflip()
    {
        if(!_canDoTrick)
            return;
        
        _animController.PlayKickflipAnimation();
        _canDoTrick = false;
        Invoke(nameof(AllowTricks), trickCooldownTime);
        //At the moment this is all this script does, once a score system is in place I will come back and add scoring functionality to this section
    }

    //Logic for pop shuv tricks
    public void PopShuv()
    {
        if(!_canDoTrick)
            return;
        
        _animController.PlayShuvitAnimation();
        _canDoTrick = false;
        Invoke(nameof(AllowTricks), trickCooldownTime);
        //At the moment this is all this script does, once a score system is in place I will come back and add scoring functionality to this section
    }

    private void AllowTricks()
    {
        _canDoTrick = true;
    }
}
