using UnityEngine;

public class PlayerTrickController : MonoBehaviour
{
    private PlayerAnimationController _animController;

    private float _kickflipScoreValue, _shuvitScoreValue;

    private bool _canDoTrick = true;
    
    [SerializeField] private float trickCooldownTime;
    
    [field: SerializeField]
    public float ShuvitHoldTime { get; private set; }

    private void Awake()
    {
        _animController = GetComponentInChildren<PlayerAnimationController>();
    }

    public void Kickflip()
    {
        if(!_canDoTrick)
            return;
        
        _animController.PlayKickflipAnimation();
        _canDoTrick = false;
        Invoke(nameof(AllowTricks), trickCooldownTime);
        //At the moment this is all this script does, once a score system is in place I will come back and add scoring functionality to this section
    }

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
