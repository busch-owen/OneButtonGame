using System;
using System.Collections;
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
    
    [Space(10f), Header("Effects")]
    [SerializeField] private ParticleSystem grindParticles;

    private WaitForFixedUpdate _waitForFixedUpdate;
    
    public bool IsGrinding { get; private set; }
    
    [Space(10f), Header("Ground Detection Attributes")]
    [SerializeField] Vector2 boxSize;
    [SerializeField] float castDistance;
    [SerializeField] LayerMask groundLayer;

    
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

    public IEnumerator StartGrind()
    {
        IsGrinding = true;
        while (true)
        {
            //Nothing here yet, but you'd just call an "AddScore" function or something of the sort to keep adding score while grinding.
            if (!grindParticles.isPlaying)
            {
                grindParticles.Play();
            }
            yield return _waitForFixedUpdate;
        }
    }

    public void StopGrind()
    {
        IsGrinding = false;
        if (grindParticles.isPlaying)
        {
            grindParticles.Stop();
        }
    }

    private void AllowTricks()
    {
        _canDoTrick = true;
    }
    
    public bool WillBail()
    {
        RaycastHit2D boxCast = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
        if (boxCast)
        {
            Debug.Log("You will bail");
            return true;
        }
        Debug.Log("Nahh you good!");
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
