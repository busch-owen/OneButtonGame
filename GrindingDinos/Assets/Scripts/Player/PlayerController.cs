using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IButtonListener
{
    
    [Header("Player Physics Attributes")]
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float jumpHeight = 10.0f;
    //raycast for grounded
    
    [Space(10f)]
    [Header("Ground Detection Attributes")]
    [SerializeField] Vector2 boxSize;
    [SerializeField] float castDistance;
    [SerializeField] LayerMask groundLayer;

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
        isGrounded();
        _animationController.ChangeGroundState(isGrounded());
    }
    
    public void ButtonHeld(ButtonInfo heldInfo)
    {
        if (isGrounded())
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
            _gameManager.DeathSequence();
            Debug.Log("I ded");
            _isDead = true;
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
        if(!isGrounded())
        {
            _playerTrickController.Kickflip();
            return;
        }
        
        _animationController.PlayJumpAnimation();
        rigidBody.velocity = new Vector2(0.0f, jumpHeight);
    }

    public void ButtonReleased(ButtonInfo releasedInfo)
    {

    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
