using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerTrickController : MonoBehaviour
{
    private ScoreHandler _scoreHandler;
    
    [Header("Trick Stats")]
    [SerializeField] private float trickCooldownTime;

    [SerializeField] private int kickflipScoreValue, shuvitScoreValue, grindScoreTickValue;
    [SerializeField] private float grindScoreTickRate;
    [field: SerializeField] public float ShuvitHoldTime { get; private set; }
    
    //Other trick variables
    private PlayerAnimationController _animController;
    private bool _canDoTrick = true;
    
    [Space(10f), Header("Effects")]
    [SerializeField] private ParticleSystem grindParticles;

    private WaitForFixedUpdate _waitForFixedUpdate;
    
    public bool IsGrinding { get; private set; }

    private PlayerController _playerController;
    
    private void Awake()
    {
        _animController = GetComponentInChildren<PlayerAnimationController>();
        _playerController = GetComponent<PlayerController>();
        _scoreHandler = FindObjectOfType<ScoreHandler>();
    }

    //Logic for kickflip tricks
    public void Kickflip()
    {
        if(!_canDoTrick)
            return;
        
        _scoreHandler.AddToTrickScore(kickflipScoreValue);
        
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
        
        _scoreHandler.AddToTrickScore(shuvitScoreValue);
        
        _animController.PlayShuvitAnimation();
        _canDoTrick = false;
        Invoke(nameof(AllowTricks), trickCooldownTime);
        //At the moment this is all this script does, once a score system is in place I will come back and add scoring functionality to this section
    }

    public void StartGrind()
    {
        IsGrinding = true;
        InvokeRepeating(nameof(Grinding), 0, grindScoreTickRate);
    }

    public void Grinding()
    {
        if (!grindParticles.isPlaying)
        {
            grindParticles.Play();
        }
        _scoreHandler.AddToTrickScore(grindScoreTickValue);
    }

    public void StopGrind()
    {
        CancelInvoke(nameof(Grinding));
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
}
