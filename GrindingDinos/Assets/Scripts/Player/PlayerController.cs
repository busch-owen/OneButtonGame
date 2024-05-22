using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IButtonListener
{
    
    [Header("Player Physics Attributes")]
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float jumpHeight = 10.0f;
    //raycast for grounded
    
    [Space(10f), Header("Ground Detection Attributes")]
    [SerializeField] Vector2 boxSize;
    [SerializeField] float castDistance;
    [SerializeField] LayerMask groundLayer;

    [Space(10f), Header("Effects")] 
    [SerializeField] private ParticleSystem deathEffect;

    [Space(10f), Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip[] deathSFXs;
    private AudioClip _deathSFX;
    [SerializeField] AudioClip deathCrunch;
    [SerializeField] AudioClip skateboardNoises;
    float SFXVolume = .75f;

    //Controllers and Managers
    private PlayerAnimationController _animationController;
    private PlayerTrickController _playerTrickController;
    private GameManager _gameManager;

    private bool _isDead;
    
    // Start is called before the first frame update
    void Start()
    {
        _isDead = false;
        _gameManager = FindObjectOfType<GameManager>();
        _playerTrickController = GetComponent<PlayerTrickController>();
        _animationController = GetComponentInChildren<PlayerAnimationController>();
        var inputListener = FindObjectOfType<PlayerInputs>();
        inputListener.RegisterListener(this);
    }

    // Update is called once per frame
    private void Update()
    {
        IsGrounded();
        _animationController.ChangeGroundState(IsGrounded());
    }
    
    public void ButtonHeld(ButtonInfo heldInfo)
    {
        if (IsGrounded())
        {
            return;
        }
        
        if (heldInfo.TimeHeld() >= _playerTrickController.ShuvitHoldTime)
        {
            _playerTrickController.PopShuv();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            KillPlayer();
        }
    }

    public void ButtonPressed(ButtonInfo pressedInfo)
    {
        if (!_gameManager.GameStarted)
        {
            _gameManager.StartGame();
            _animationController.TransitionToRun();
            return;
        }

        if (_isDead)
        {
            _gameManager.RestartGame();
        }
        
        //jump
        if(!IsGrounded())
        {
            _playerTrickController.Kickflip();
            return;
        }
        
        _animationController.PlayJumpAnimation();
        _audioSource.PlayOneShot(jumpSFX, SFXVolume);
        rigidBody.velocity = new Vector2(0.0f, jumpHeight);
    }

    public void ButtonReleased(ButtonInfo releasedInfo)
    {

    }

    // ReSharper disable Unity.PerformanceAnalysis
    public bool IsGrounded()
    {
        RaycastHit2D boxCast = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
        if (boxCast)
        {
            if (_playerTrickController.WillBail())
            {
                //KillPlayer();
                //return false;
            }
            
            if (!boxCast.collider.GetComponent<GrindSurface>()) 
                return true;
            
            if (!_playerTrickController.IsGrinding)
            {
                StartCoroutine(_playerTrickController.StartGrind());
            }
            
            return true;
        }
        StopCoroutine(_playerTrickController.StartGrind());
        _playerTrickController.StopGrind();
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void KillPlayer()
    {
        Debug.Log("Killed Player");
        _gameManager.DeathSequence();
        deathEffect.Play();
        
        //audio jank
        _audioSource.PlayOneShot(deathCrunch, SFXVolume);
        int rand = UnityEngine.Random.Range(0, deathSFXs.Length);
        _deathSFX = deathSFXs[rand];
        _audioSource.PlayOneShot(_deathSFX, SFXVolume);

        _isDead = true;
    }
}
