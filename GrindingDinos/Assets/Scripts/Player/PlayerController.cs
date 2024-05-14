using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IButtonListener
{
    [SerializeField] Rigidbody2D rigidBody;
    float jumpHeight = 10.0f;
    //raycast for grounded
    [SerializeField] Vector2 boxSize;
    [SerializeField] float castDistance;
    [SerializeField] LayerMask groundLayer;

    private PlayerAnimationController _animationController;

    private PlayerTrickController _playerTrickController;
    
    // Start is called before the first frame update
    void Start()
    {
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
    public void ButtonPressed(ButtonInfo pressedInfo)
    {
        //jump
        if(!isGrounded())
        {
            _playerTrickController.Kickflip();
            return;
        }
        
        Debug.Log("boing");
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
            Debug.Log("Grounded");
            return true;
        }

        Debug.Log("Not Grounded");
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }
}
