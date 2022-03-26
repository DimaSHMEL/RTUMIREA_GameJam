using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    //Movement values
    public float speedMovement;
    public float jumpForce;
    private Rigidbody2D rb;
    private bool facingRight = true;
    //Checkers values
    public LayerMask whatIsGround, whatIsWall;
    public BoxCollider2D groundCheck, wallCheck;
    private bool onGround = false;
    //Control values
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    //Score values
    private int collectables = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 speedXY = playerInputActions.Player.Move.ReadValue<Vector2>();
        if(Math.Abs(speedXY.x) > 0.15f && onGround)
            rb.velocity = new Vector2(speedXY.x *  speedMovement, rb.velocity.y);
        else if(Math.Abs(speedXY.x) == 0.0f && rb.velocity.y == 0.0f )
            rb.velocity = new Vector2(0, rb.velocity.y);

        
    }
    IEnumerator holdJump()
    {
        float jumpPower = 0;
        float numberOfSeconds = 0;
        while (playerInputActions.Player.Jump.IsPressed())
        {
            if (jumpPower < 1.2f)
            {
                jumpPower += 0.1f;
                numberOfSeconds += 0.1f;
            }
            
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log(jumpPower * jumpForce);
        Debug.Log(numberOfSeconds);
        if(facingRight)
            rb.velocity = new Vector2(speedMovement, jumpPower * jumpForce);
        else
            rb.velocity = new Vector2(-speedMovement, jumpPower * jumpForce);

    }
    private void Update()
    {
        if (playerInputActions.Player.Jump.triggered && onGround)
        {
            StartCoroutine(holdJump());
        }
        checkStatuses();

    }
    private void checkStatuses()
    {
        Vector2 speedXY = playerInputActions.Player.Move.ReadValue<Vector2>();
        onGround = groundCheck.IsTouchingLayers(whatIsGround);
        if (speedXY.x < 0f && facingRight)
            flip();
        else if (speedXY.x > 0f && !facingRight)
            flip();
        if (wallCheck.IsTouchingLayers(whatIsWall))
            rb.AddForce(new Vector2(0, -9.8f));
    }
    private void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
